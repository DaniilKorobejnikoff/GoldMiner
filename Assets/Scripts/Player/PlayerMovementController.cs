using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    [SerializeField] private Player _player;
    [SerializeField] private Gyro _gyro;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _basketTransform;
    private const float _basketOffsetX = 0.7f;

    private float _minX;
    private float _maxX;

    private bool _canMove = true;

    private EventBus _eventBus;


    public bool useAxisInput = false;

    private void Start() {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<GameStopSignal>(x => { _canMove = false; });

        _eventBus.Subscribe<GameStartedSignal>(x => { _canMove = true; });

        var spawner = ServiceLocator.Current.Get<InteractablesSpawner>();
        _maxX = spawner.MaxX;
        _minX = spawner.MinX;
    }

    private void Update() {
        if (!_canMove)
            return;

        var playerInput = useAxisInput ? Input.GetAxisRaw("Horizontal") : _gyro.CalculateHorizontalInput();

        if (playerInput == 1.0f) {
            _spriteRenderer.flipX = false;
            _basketTransform.localPosition = new Vector3(_basketOffsetX, _basketTransform.localPosition.y, 0);

            if (_player.transform.position.x > _maxX) {
                return;
            }
        }

        if (playerInput == -1.0f) {
            _spriteRenderer.flipX = true;
            _basketTransform.localPosition = new Vector3(-_basketOffsetX, _basketTransform.localPosition.y, 0);

            if (_player.transform.position.x < _minX) {
                return;
            }
        }

        _player.transform.Translate(Vector3.right * (Time.deltaTime * playerInput * _player.SpeedKoef));
    }

    private void OnDestroy() {
        _eventBus.Unsubscribe<GameStopSignal>(x => { _canMove = false; });
        _eventBus.Unsubscribe<GameStartedSignal>(x => { _canMove = true; });
    }
}
