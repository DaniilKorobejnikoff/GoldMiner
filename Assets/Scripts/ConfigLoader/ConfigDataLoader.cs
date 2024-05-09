using System.Collections.Generic;
using System.Linq;
using CustomEventBus;
using CustomEventBus.Signals;
using UI;
using UI.Dialogs;

/// <summary>
/// Этот класс получает все лоадеры в игре
/// Когда все ресурсы загружены создаст AllDataLoadedSignal 
/// Это значит, что игра готова 
/// </summary>
public class ConfigDataLoader : IService {
    private List<ILoader> _loaders;
    private EventBus _eventBus;

    private int _loadedSystems = 0;
    public void Init(List<ILoader> loaders) {
        _loaders = loaders;

        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<DataLoadedSignal>(OnConfigLoaded);

        // Показать экран загрузки если загрузка не мгновенна
        if (_loaders.Any(x => !x.IsLoadingInstant())) {
            DialogManager.ShowDialog<LoadingDialog>();
        }

        LoadAll();
    }

    private void OnConfigLoaded(DataLoadedSignal signal) {
        _loadedSystems++;

        _eventBus.Invoke(new LoadProgressChangedSignal(((float)_loadedSystems / _loaders.Count)));
        if (_loadedSystems == _loaders.Count) {
            _eventBus.Invoke(new AllDataLoadedSignal());
        }
    }

    private void LoadAll() {
        foreach (var loader in _loaders) {
            loader.Load();
        }
    }
}