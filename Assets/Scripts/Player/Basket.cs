using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class Basket : MonoBehaviour, IService {
    /// <summary>
    /// Логика отображения монет в корзине
    /// </summary>

    private EventBus _eventBus;
    [SerializeField] private GameObject[] _coins;
    [SerializeField] private int numberOfActivatedCoins = 0;

    public void Init() {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GameStartedSignal>(OnGameStarted);
        _eventBus.Subscribe<AddScoreSignal>(OnScoreAdded);
    }

    private void OnGameStarted(GameStartedSignal signal) {
        numberOfActivatedCoins = 0;

        DisplayCoinsOnBusket();
    }

    private void OnScoreAdded(AddScoreSignal signal) {
        numberOfActivatedCoins++;

        DisplayCoinsOnBusket();
    }

    private void DisplayCoinsOnBusket() {
        for (int i = 0; i < _coins.Length; i++) {
            bool isCoinActive = i <= numberOfActivatedCoins - 1;
            _coins[i].gameObject.SetActive(isCoinActive);
        }
    }
}

