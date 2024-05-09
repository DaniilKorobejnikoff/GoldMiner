using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Dialogs {
    /// <summary>
    /// Окно меню
    /// </summary>
    public class MenuDialog : Dialog {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;

        protected void Awake() {
            base.Awake();

            _playButton.onClick.AddListener(OnPlayButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnPlayButtonClick() {
            SceneManager.LoadScene(StringConstants.MAIN_SCENE_NAME);
        }

        private void OnSettingsButtonClick() {
            DialogManager.ShowDialog<SettingsDialog>();
            Hide();
        }
    }
}
