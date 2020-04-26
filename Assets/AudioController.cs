using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip gunShots;
    [SerializeField] private AudioClip gotHitCrash;

    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip buttonHover;


    public void PlayBGM(float volume)
    {
        AudioManager.Instance.PlayMusic(bgm);
        AudioManager.Instance.SetMusicVolume(volume);
    }
    public void PlayGunShot()
    {
        AudioManager.Instance.PlaySFX(gunShots);
    }
    public void PlayGotHit()
    {
        AudioManager.Instance.PlaySFX(gotHitCrash);
    }

    public void PlayButtonClick()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
        
    }
    public void PlayButtonHover()
    {
        AudioManager.Instance.PlaySFX(buttonHover);

    }
}
