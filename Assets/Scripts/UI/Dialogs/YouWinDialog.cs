using CustomEventBus;
using CustomEventBus.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Dialogs {
    /// <summary>
    /// Окно победы
    /// </summary>
    public class YouWinDialog : Dialog {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _goToMenuButton;
        [SerializeField] private TextMeshProUGUI _currentScoreText;
        /// <summary>
        /// При необходимости можно добавить рекорд и дополнительную статистику
        /// </summary>

        private EventBus _eventBus;

        void Start() {
            _nextLevelButton.onClick.AddListener(NextLevel);
            _goToMenuButton.onClick.AddListener(GoToMenu);

            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        public void Init(int currentScore) {
            _currentScoreText.text = currentScore.ToString();
        }

        private void NextLevel() {
            _eventBus.Invoke(new NextLevelSignal());
            Hide();
        }

        private void GoToMenu() {
            SceneManager.LoadScene(StringConstants.MENU_SCENE_NAME);
            Hide();
        }
    }
}
