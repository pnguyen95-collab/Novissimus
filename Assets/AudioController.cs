using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioClip bgmCombat;
    [SerializeField] private AudioClip bgmMenu;
    [SerializeField] private AudioClip bgmCraftandCustom;

    [SerializeField] private AudioClip gunShots;
    [SerializeField] private AudioClip BladeSound;
    [SerializeField] private AudioClip sniper;
    [SerializeField] private AudioClip hammer;
    [SerializeField] private AudioClip gotHitCrash;
    [SerializeField] private AudioClip carMoving;

    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip buttonHover;

   

    public void PlayBGMStartScene(float volume)
    {
        AudioManager.Instance.PlayMusic(bgmMenu);
        AudioManager.Instance.SetMusicVolume(volume);
    }

    public void PlayBGMCraftandCustimizeScene(float volume)
    {
        AudioManager.Instance.PlayMusic(bgmCraftandCustom);
        AudioManager.Instance.SetMusicVolume(volume);
    }

    public void PlayBGMCombatScene(float volume)
    {
        AudioManager.Instance.PlayMusic(bgmCombat);
        AudioManager.Instance.SetMusicVolume(volume);
    }
    public void PlayGunShot()
    {
        AudioManager.Instance.PlaySFX(gunShots);
    }
    public void PlayBlade()
    {
        AudioManager.Instance.PlaySFX(BladeSound);
    }
    public void PlaySniper()
    {
        AudioManager.Instance.PlaySFX(sniper);
    }
    public void PlayHammer()
    {
        AudioManager.Instance.PlaySFX(hammer);
    }
    public void PlayGotHit()
    {
        AudioManager.Instance.PlaySFX(gotHitCrash);
    }
    public void PlayMoving()
    {
        AudioManager.Instance.PlaySFX(carMoving);
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
