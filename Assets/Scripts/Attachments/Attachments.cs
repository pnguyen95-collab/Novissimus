using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachments
{
    //name of the attachment
    public enum Name
    {
        MachineGun,
        PivotHammer,
        EaterBlades,
        Sniper,
        SpikeBomb,
        NoCoating,
        MetalCoating,
        SpikeCoating,
        GlassCoating,
        RegularTyres,
        RubberTyres,
        OffRoadTyres,
        CrystalTyres,
        BladedWeaponry,
        MaggotFarm,
        Lightweight,
        HerbMaster,
        AttackMeSign
    }

    //the equipable slot of the attachment
    public enum Slot
    {
        Weapon,
        Armour,
        Wheels,
        Boosters
    }

    //attachment information
    public Name attachmentName;
    public Slot equipSlot;
    public int weaponNumber;

    //sprite to display in the crafting menu
    public Sprite GetSprite()
    {
        switch(attachmentName)
        {
            default:
            case Name.MachineGun:        return AttachmentAssets.Instance.machineGunSprite;
            case Name.PivotHammer:       return AttachmentAssets.Instance.pivotHammerSprite;
            case Name.EaterBlades:       return AttachmentAssets.Instance.eaterBladesSprite;
            case Name.Sniper:            return AttachmentAssets.Instance.sniperSprite;
            case Name.SpikeBomb:         return AttachmentAssets.Instance.spikeBombSprite;

            case Name.MetalCoating:      return AttachmentAssets.Instance.armorSprite;
            case Name.SpikeCoating:      return AttachmentAssets.Instance.armorSprite;
            case Name.GlassCoating:      return AttachmentAssets.Instance.armorSprite;

            case Name.RubberTyres:       return AttachmentAssets.Instance.wheelSprite;
            case Name.OffRoadTyres:      return AttachmentAssets.Instance.wheelSprite;
            case Name.CrystalTyres:      return AttachmentAssets.Instance.wheelSprite;

            case Name.BladedWeaponry:    return AttachmentAssets.Instance.boosterSprite;
            case Name.MaggotFarm:        return AttachmentAssets.Instance.boosterSprite;
            case Name.Lightweight:       return AttachmentAssets.Instance.boosterSprite;
            case Name.HerbMaster:        return AttachmentAssets.Instance.boosterSprite;
            case Name.AttackMeSign:      return AttachmentAssets.Instance.boosterSprite;
        }
    }

    //list of resources required to craft each attachment
    public List<Item> CraftRequirement()
    {
        switch(attachmentName)
        {
            default:
            case Name.PivotHammer:      return CraftingReq.Instance.pivotHammerReq;
            case Name.EaterBlades:      return CraftingReq.Instance.eaterBladesReq;
            case Name.Sniper:           return CraftingReq.Instance.sniperReq;
            case Name.SpikeBomb:        return CraftingReq.Instance.spikeBombReq;
        }
    }
    
}
