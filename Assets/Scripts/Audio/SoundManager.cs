using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    AudioMixer mixer;

    [SerializeField]
    private AudioSource _musicSource, _effectsSource;
    //[SerializeField]
    //private AudioSource playOnClickSource;

    [SerializeField]
    List<AudioClip> audioClips = new();

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeBackgroundSong(float duration, int songIndex)
    {
        StartCoroutine(FadeMusic(duration, audioClips[songIndex]));
    }
    private IEnumerator FadeMusic(float duration, AudioClip song)
    {
        StartCoroutine(FadeMixerGroup.StartFade(mixer, "MusicVolume", duration, 0));
        yield return new WaitForSeconds(duration);
        _musicSource.clip = song;
        _musicSource.Play();
        StartCoroutine(FadeMixerGroup.StartFade(mixer, "MusicVolume", duration, 1));
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleEffects()
    {
        _effectsSource.mute = !_effectsSource.mute;
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }
}
