using System.Collections.Generic;
using CustomEventBus;
using UI;
using UnityEngine;

namespace Examples.VerticalScrollerExample {
    public class ServiceLocatorLoader_Menu : MonoBehaviour {
        [SerializeField] private GUIHolder _guiHolder;

        [SerializeField] private ScriptableObjectLevelLoader _scriptableObjectLevelLoader;
        private ConfigDataLoader _configDataLoader;
        [SerializeField] private bool _loadFromJSON;

        private EventBus _eventBus;
        private GoldController _goldController;
        private ScoreController _scoreController;

        private ILevelLoader _levelLoader;
        private List<IDisposable> _disposables = new List<IDisposable>();

        private void Awake() {
            _eventBus = new EventBus();
            _goldController = new GoldController();
            _scoreController = new ScoreController();

            _levelLoader = _scriptableObjectLevelLoader;

            Register();
            Init();
            AddToDisposables();
        }


        private void Init() {
            _goldController.Init();
            _scoreController.Init();

            var loaders = new List<ILoader>();
            loaders.Add(_levelLoader);
            _configDataLoader = new ConfigDataLoader();
            _configDataLoader.Init(loaders);
        }

        private void Register() {
            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(_goldController);
            ServiceLocator.Current.Register(_scoreController);
            ServiceLocator.Current.Register(_eventBus);
            ServiceLocator.Current.Register<GUIHolder>(_guiHolder);

            ServiceLocator.Current.Register<ILevelLoader>(_levelLoader);
        }

        private void AddToDisposables() {
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