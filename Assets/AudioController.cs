using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip gunShots;
    [SerializeField] private AudioClip gotHitCrash;

    [SerializeField] private AudioClip buttonClick;


    public void PlayBGM()
    {
        AudioManager.Instance.PlayMusic(bgm);
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
}
