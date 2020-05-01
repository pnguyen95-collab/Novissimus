using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentAssets : MonoBehaviour
{
    public static AttachmentAssets Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    //sprites for the attachments icons to display
    public Sprite machineGunSprite;
    public Sprite pivotHammerSprite;
    public Sprite eaterBladesSprite;
    public Sprite sniperSprite;
    public Sprite spikeBombSprite;

    public Sprite armorSprite;
    public Sprite wheelSprite;
    public Sprite boosterSprite;

    //sprites for the attachment attack pattern to display
    public Sprite machineGunPattern;
    public Sprite pivotHammerPattern;
    public Sprite eaterBladePattern;
    public Sprite sniperPattern;
}
