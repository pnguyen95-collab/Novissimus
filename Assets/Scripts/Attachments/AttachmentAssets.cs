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

    public Sprite baseArmorSprite;
    public Sprite armorSprite;
    public Sprite armorSprite2;
    public Sprite armorSprite3;
    public Sprite baseWheelSprite;
    public Sprite wheelSprite;
    public Sprite wheelSprite2;
    public Sprite wheelSprite3;
    public Sprite boosterSprite;
    public Sprite boosterSprite2;
    public Sprite boosterSprite3;

    //sprites for the attachment attack pattern to display
    public Sprite machineGunPattern;
    public Sprite pivotHammerPattern;
    public Sprite eaterBladePattern;
    public Sprite sniperPattern;
}
