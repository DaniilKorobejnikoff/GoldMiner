using CustomEventBus;
using CustomEventBus.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Dialogs {
    /// <summary>
    /// Окно проигрыша
    /// </summary>
    public class YouLoseDialog : Dialog {
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private TextMeshProUGUI _currentScoreText;


        private EventBus _eventBus;

        private void Start() {
            _retryButton.onClick.AddListener(TryAgain);
            _menuButton.onClick.AddListener(GoToMenu);

            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        public void Init(int currentScore) {
            _currentScoreText.text = currentScore.ToString();
        }

        private void TryAgain() {
            _eventBus.Invoke(new RestartLevelSignal());
            Hide();
        }

        private void GoToMenu() {
            SceneManager.LoadScene(StringConstants.MENU_SCENE_NAME);
        }
    }
}
