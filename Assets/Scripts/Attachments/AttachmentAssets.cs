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

    //sprites for the attachments to display
    public Sprite machineGunSprite;
    public Sprite pivotHammerSprite;
    public Sprite eaterBladesSprite;
    public Sprite sniperSprite;
    public Sprite spikeBombSprite;

    public Sprite armorSprite;
    public Sprite wheelSprite;
    public Sprite boosterSprite;
}
