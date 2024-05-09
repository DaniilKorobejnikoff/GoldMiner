using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogs {
    /// <summary>
    /// Окно настроек звука и сброс PlayerPrefs
    /// </summary>
    public class SettingsDialog : Dialog {
        [SerializeField] private Toggle _soundOnToggle;
        [SerializeField] private Toggle _musicOnToggle;
        [SerializeField] private Button _resetPlayerPrefsButton;
        [SerializeField] private Button _backButton;

        protected override void Awake() {
            base.Awake();
            _resetPlayerPrefsButton.onClick.AddListener(OnResetBtnClicked);
            _backButton.onClick.AddListener(BackToMenu);


            _soundOnToggle.isOn = PlayerPrefs.GetInt(StringConstants.SOUND_ON) == 1;
            _soundOnToggle.onValueChanged.AddListener(OnSoundOnChanged);

            _musicOnToggle.isOn = PlayerPrefs.GetInt(StringConstants.MUSIC_ON) == 1;
            _musicOnToggle.onValueChanged.AddListener(OnMusicOnChanged);
        }

        private void OnSoundOnChanged(bool isSoundOn) {
            int currentSoundToggleValue = isSoundOn ? 1 : 0;
            PlayerPrefs.SetInt(StringConstants.SOUND_ON, currentSoundToggleValue);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetInt(StringConstants.SOUND_ON));
        }
        private void OnMusicOnChanged(bool isMusicOn) {
            int currentMusicToggleValue = isMusicOn ? 1 : 0;
            PlayerPrefs.SetInt(StringConstants.MUSIC_ON, currentMusicToggleValue);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetInt(StringConstants.MUSIC_ON));
        }

        private void OnResetBtnClicked() {
            PlayerPrefs.DeleteAll();
        }
        private void BackToMenu() {
            DialogManager.ShowDialog<MenuDialog>();
            Hide();
        }
    }
}
