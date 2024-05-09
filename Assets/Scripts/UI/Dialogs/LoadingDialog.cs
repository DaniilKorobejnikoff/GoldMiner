using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogs {
    /// <summary>
    /// Окно загрузки
    /// при поступлении сигнала AllResourcesLoaded исчезает
    /// </summary>
    public class LoadingDialog : Dialog {
        [SerializeField] private Image[] _loadingSegments;

        private EventBus _eventBus;

        private void Awake() {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<LoadProgressChangedSignal>(LoadProgressChanged);
            _eventBus.Subscribe<AllDataLoadedSignal>(OnAllResourcesLoaded);

            HideAllLoadingSegments();
        }

        private void LoadProgressChanged(LoadProgressChangedSignal signal) {
            DisplayLoadingSegments(signal.Progress);
        }

        private void OnAllResourcesLoaded(AllDataLoadedSignal signal) {
            Hide();
        }

        private void DisplayLoadingSegments(float progress) {
            int segmentsToShow = Mathf.CeilToInt(progress * _loadingSegments.Length);

            for (int i = 0; i < _loadingSegments.Length; i++) {
                if (i < segmentsToShow) {
                    _loadingSegments[i].gameObject.SetActive(true);
                }
                else {
                    _loadingSegments[i].gameObject.SetActive(false);
                }
            }
        }

        private void HideAllLoadingSegments() {
            foreach (Image segment in _loadingSegments) {
                segment.gameObject.SetActive(false);
            }
        }
    }
}
