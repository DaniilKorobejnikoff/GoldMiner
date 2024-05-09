using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

/// <summary>
/// Логика параметров персонажа
/// </summary>
public class Player : MonoBehaviour, IService {
    [SerializeField] private int _health = 3;
    [SerializeField] private float _speedKoef = 3f;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _explosionAnimator;

    public int Health => _health;
    public float SpeedKoef => _speedKoef;

    private EventBus _eventBus;

    public void Init() {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<PlayerDamagedSignal>(OnPlayerDamaged);
        _eventBus.Subscribe<AddHealthSignal>(OnAddHealth);
        _eventBus.Subscribe<GameStartedSignal>(GameStarted);
    }

    private void GameStarted(GameStartedSignal signal) {
        _health = 3;
        _eventBus.Invoke(new HealthChangedSignal(_health));
    }

    private void OnPlayerDamaged(PlayerDamagedSignal signal) {
        _explosionAnimator.SetTrigger("Explosion_trig");
        int val = signal.Health;

        _health -= val;
        if (_health < 0) {
            _health = 0;
        }

        _eventBus.Invoke(new HealthChangedSignal(_health));

        if (_health == 0) {
            _eventBus.Invoke(new PlayerDeadSignal());
        }
    }

    private void OnAddHealth(AddHealthSignal signal) {
        _health += signal.Value;

        if (_health > 3) {
            _eventBus.Invoke(new AddScoreSignal(50 * (_health - 3)));
            _health = 3;
        }

        _eventBus.Invoke(new HealthChangedSignal(_health));
    }



    private void OnDestroy() {
        _eventBus.Unsubscribe<PlayerDamagedSignal>(OnPlayerDamaged);
        _eventBus.Unsubscribe<AddHealthSignal>(OnAddHealth);

        _eventBus.Unsubscribe<GameStartedSignal>(GameStarted);
    }
}