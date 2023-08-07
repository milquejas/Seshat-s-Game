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
    [SerializeField] private float currentMusicVolume;
    //[SerializeField]
    //private AudioSource playOnClickSource;

    [SerializeField]
    List<AudioClip> audioClips = new();

    public const string MUSIC_KEY = "MusicVolume";
    public const string SFX_KEY = "SFXVolume";



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
        mixer.GetFloat("MusicVolume", out currentMusicVolume);
        currentMusicVolume = Mathf.Pow(10, (currentMusicVolume / 20));
        StartCoroutine(FadeMixerGroup.StartFade(mixer, "MusicVolume", duration, 0));
        yield return new WaitForSeconds(duration);
        _musicSource.clip = song;
        _musicSource.Play();
        StartCoroutine(FadeMixerGroup.StartFade(mixer, "MusicVolume", duration, currentMusicVolume));
    }

    public void ChangeMasterVolume(float value)
    {
        //AudioListener.volume = value;
    }

    public void ToggleEffects()
    {
        //_effectsSource.mute = !_effectsSource.mute;
    }

    public void ToggleMusic()
    {
        //_musicSource.mute = !_musicSource.mute;
    }
}
