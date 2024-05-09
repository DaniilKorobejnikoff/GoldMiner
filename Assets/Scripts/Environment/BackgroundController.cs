using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

/// <summary>
/// Отвечает за управление фоном в игре
/// </summary>
public class BackgroundController : MonoBehaviour, IService {
    [SerializeField] private Sprite[] _backgroundSpritesRenderer;
    private SpriteRenderer _currentBackgroundSpriteRenderer;

    private EventBus _eventBus;

    public void Init() {
        _currentBackgroundSpriteRenderer = GetComponent<SpriteRenderer>();
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<GameStartedSignal>(ChangeBackground);
    }

    public void Dispose() {
        _eventBus.Unsubscribe<GameStartedSignal>(ChangeBackground);
    }

    private void ChangeBackground(GameStartedSignal signal) {
        _currentBackgroundSpriteRenderer.sprite = _backgroundSpritesRenderer[Random.Range(0, _backgroundSpritesRenderer.Length)];
    }
}
