using System.Collections.Generic;
using CustomEventBus;
using UI;
using UnityEngine;
using IDisposable = CustomEventBus.IDisposable;

namespace Examples.VerticalScrollerExample {
    /// <summary>
    /// Загрузчик сервисов для сцены с игрой
    /// </summary>
    public class ServiceLocatorLoader_Main : MonoBehaviour {
        [SerializeField] private InteractableController _interactableMover;
        [SerializeField] private BackgroundController _backgroundController;
        [SerializeField] private AudioController _audioController;
        [SerializeField] private Basket _basket;
        [SerializeField] private SignalSpawner _signalSpawner;
        [SerializeField] private InteractablesSpawner _interactablesSpawner;
        [SerializeField] private Player _player;
        [SerializeField] private GUIHolder _guiHolder;

        [SerializeField] private LevelController _levelController;
        [SerializeField] private ScriptableObjectLevelLoader _scriptableObjectLevelLoader;
        private ConfigDataLoader _configDataLoader;

        private EventBus _eventBus;
        private GameController _gameController;
        private GoldController _goldController;
        private ScoreController _scoreController;

        private ILevelLoader _levelLoader;

        private List<IDisposable> _disposables = new List<IDisposable>();

        private void Awake() {
            _eventBus = new EventBus();
            _gameController = new GameController();
            _goldController = new GoldController();
            _scoreController = new ScoreController();

            _levelLoader = _scriptableObjectLevelLoader;
            RegisterServices();
            Init();
            AddDisposables();
        }

        private void RegisterServices() {
            ServiceLocator.Initialize();

            ServiceLocator.Current.Register(_eventBus);
            ServiceLocator.Current.Register<InteractableController>(_interactableMover);
            ServiceLocator.Current.Register<BackgroundController>(_backgroundController);
            ServiceLocator.Current.Register<AudioController>(_audioController);
            ServiceLocator.Current.Register<Basket>(_basket);
            ServiceLocator.Current.Register<SignalSpawner>(_signalSpawner);
            ServiceLocator.Current.Register<InteractablesSpawner>(_interactablesSpawner);
            ServiceLocator.Current.Register<Player>(_player);
            ServiceLocator.Current.Register<GUIHolder>(_guiHolder);
            ServiceLocator.Current.Register(_gameController);
            ServiceLocator.Current.Register(_goldController);
            ServiceLocator.Current.Register(_scoreController);
            ServiceLocator.Current.Register<ILevelLoader>(_levelLoader);
        }

        private void Init() {
            _interactableMover.Init();
            _backgroundController.Init();
            _audioController.Init();
            _basket.Init();
            _signalSpawner.Init();
            _interactablesSpawner.Init();
            _player.Init();
            _gameController.Init();
            _goldController.Init();
            _scoreController.Init();
            _levelController.Init();

            var loaders = new List<ILoader>();
            loaders.Add(_levelLoader);
            _configDataLoader = new ConfigDataLoader();
            _configDataLoader.Init(loaders);
        }

        private void AddDisposables() {
            _disposables.Add(_gameController);
            _disposables.Add(_goldController);
            _disposables.Add(_scoreController);
        }

        private void OnDestroy() {
            foreach (var disposable in _disposables) {
                disposable.Dispose();
            }
        }
    }
}