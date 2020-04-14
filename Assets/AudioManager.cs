using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Static Instance
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    #endregion

    #region Fields
    private AudioSource musicSouce;
    private AudioSource musicSouce2;
    private AudioSource sfxSource;

    private bool firstMusicSourceIsPlaying;
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        musicSouce = this.gameObject.AddComponent<AudioSource>();
        musicSouce2 = this.gameObject.AddComponent<AudioSource>(); ;
        sfxSource = this.gameObject.AddComponent<AudioSource>(); ;

        //loop the music track
        musicSouce.loop = true;
        musicSouce2.loop = true;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSouce : musicSouce2;

        activeSource.clip = musicClip;
        activeSource.volume = 1;
        activeSource.Play();
    }

    public void PlayMusicWithFade(AudioClip newClip, float transTime = 1f)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSouce : musicSouce2;
        StartCoroutine(UpdateMusicWithFade(activeSource,newClip,transTime));
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transTime = 1f)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSouce : musicSouce2;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSouce2 : musicSouce;

        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transTime));
    }

    public IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip,float transTime)
    {
        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;

        //fade out
        for (t = 0; t < transTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transTime));
            yield return null;

        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();
        //fade in
        for (t = 0; t < transTime; t += Time.deltaTime)
        {
            activeSource.volume =  (t / transTime);
            yield return null;

        }
    }
    public IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transTime)
    {
        float t = 0.0f;

       
        for (t = 0; t < transTime; t += Time.deltaTime)
        {
            original.volume = (1 - (t / transTime));
            newSource.volume =  (t / transTime);
            yield return null;

        }
        original.Stop();
        
    }



    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSouce.volume = volume;
        musicSouce2.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
