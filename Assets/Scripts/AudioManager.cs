using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sFXSource;

    [Header("Music")]
    public AudioClip mainMenu;
    public AudioClip tutorial;
    public AudioClip level1;
    public AudioClip level2;
    public AudioClip tysonBoss;
    private AudioClip selectedAudio;

    [SerializeField] private int musicIndex;

    [Header("SFXs")]
    public AudioClip punch;
    public AudioClip kick;
    public AudioClip select;
    public AudioClip playerHit;
    public AudioClip crowHit;

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sFXSlider;

    private void Start()
    {
        switch(musicIndex)
        {
            case 0:
                selectedAudio = mainMenu;
                break;
            case 1:
                selectedAudio = tutorial;
                break;
            case 2:
                selectedAudio = level1;
                break;
            case 3:
                selectedAudio = level2;
                break;
        }

        LoadVolume();

        musicSource.clip=selectedAudio;
        musicSource.Play();
    }

    public void PlayBoss1Music()
    {
        musicSource.clip = tysonBoss;
        musicSource.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        sFXSource.PlayOneShot(clip);
    }

    public void SetMasterVolume()
    {
        float volume1 = masterSlider.value;
        myMixer.SetFloat("master", Mathf.Log10(volume1)*20);
        PlayerPrefs.SetFloat("masterVolume", volume1);
    }

    public void SetMusicVolume()
    {
        float volume2 = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume2)*20);
        PlayerPrefs.SetFloat("musicVolume", volume2);
    }

    public void SetSFXVolume()
    {
        float volume3 = sFXSlider.value;
        myMixer.SetFloat("sFX", Mathf.Log10(volume3)*20);
        PlayerPrefs.SetFloat("sFXVolume", volume3);
    }

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume",1f);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume",1f);
        sFXSlider.value = PlayerPrefs.GetFloat("sFXVolume",1f);
    }

    public void FadeOutMusic()
    {
        StartCoroutine(FadeOut(musicSource, 1.5f));
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
