using System;
using System.Collections.Generic;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class AudioController : MonoBehaviour, IService {
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private List<AudioSource> _soundEffectSources = new List<AudioSource>();
    [SerializeField, Range(0.1f, 1f)] private float _musicVolumeMultiplier = 0.15f;

    private Dictionary<SoundType, AudioClip> _soundLibrary = new Dictionary<SoundType, AudioClip>();
    private EventBus _eventBus;

    public void Init() {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GameStartedSignal>(StartLevelSoundSettings);
        LoadSoundLibrary();
    }

    private void StartLevelSoundSettings(GameStartedSignal signal) {
        TurnOnSoundEffects();
        TurnOnMusic();
    }

    private void LoadSoundLibrary() {
        foreach (var soundType in Enum.GetValues(typeof(SoundType))) {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/" + soundType.ToString());
            if (clip != null) {
                _soundLibrary.Add((SoundType)soundType, clip);
            }
            else {
                Debug.LogWarning("Sound clip for " + soundType.ToString() + " not found!");
            }
        }
    }

    public void PlayMusic(AudioClip musicClip) {
        if (_musicSource != null && musicClip != null) {
            _musicSource.clip = musicClip;
            _musicSource.Play();
        }
    }

    public void PlaySoundEffect(SoundType soundType) {
        if (_soundLibrary.ContainsKey(soundType)) {
            foreach (var source in _soundEffectSources) {
                if (!source.isPlaying) {
                    source.clip = _soundLibrary[soundType];
                    source.Play();
                    break;
                }
            }
        }
        else {
            Debug.LogWarning("Sound clip for " + soundType.ToString() + " not found in the library!");
        }
    }

    public void SetMusicVolume(float volume) {
        if (_musicSource != null) {
            _musicSource.volume = Mathf.Clamp01(volume);
        }
    }

    public void SetSoundEffectsVolume(float volume) {
        foreach (var source in _soundEffectSources) {
            source.volume = Mathf.Clamp01(volume);
        }
    }

    public void TurnOnSoundEffects() {
        float isSoundEffectsOnValue = PlayerPrefs.GetInt(StringConstants.SOUND_ON);
        foreach (var source in _soundEffectSources) {
            source.volume = isSoundEffectsOnValue;
        }
    }
    public void TurnOnMusic() {
        float isMusicOnValue = PlayerPrefs.GetInt(StringConstants.MUSIC_ON);
        Debug.Log(isMusicOnValue);
        _musicSource.volume = isMusicOnValue * _musicVolumeMultiplier;
    }
}
