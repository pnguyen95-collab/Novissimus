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

    public Name attachmentName;

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
    public List<Item> GetCraftRequirement()
    {
        switch(attachmentName)
        {
            default:
            case Name.PivotHammer:      return CraftingReq.Instance.pivotHammerReq;
            case Name.EaterBlades:      return CraftingReq.Instance.eaterBladesReq;
            case Name.Sniper:           return CraftingReq.Instance.sniperReq;
            case Name.SpikeBomb:        return CraftingReq.Instance.spikeBombReq;

            case Name.MetalCoating:     return CraftingReq.Instance.metalCoatingReq;
            case Name.SpikeCoating:     return CraftingReq.Instance.spikesCoatingReq;
            case Name.GlassCoating:     return CraftingReq.Instance.glassCoatingReq;

            case Name.RubberTyres:      return CraftingReq.Instance.rubberTyresReq;
            case Name.OffRoadTyres:     return CraftingReq.Instance.offRoadTyresReq;
            case Name.CrystalTyres:     return CraftingReq.Instance.crystalTyresReq;

            case Name.BladedWeaponry:   return CraftingReq.Instance.bladedWeaponryReq;
            case Name.MaggotFarm:       return CraftingReq.Instance.maggotFarmReq;
            case Name.Lightweight:      return CraftingReq.Instance.lightweightReq;
            case Name.HerbMaster:       return CraftingReq.Instance.herbMasterReq;
            case Name.AttackMeSign:     return CraftingReq.Instance.attackMeSignReq;
        }
    }
    
    //what equippable slot the attachment belongs to
    public Slot GetSlot()
    {
        switch(attachmentName)
        {
            default:
            case Name.MachineGun:       return Slot.Weapon;
            case Name.PivotHammer:      return Slot.Weapon;
            case Name.EaterBlades:      return Slot.Weapon;
            case Name.Sniper:           return Slot.Weapon;
            case Name.SpikeBomb:        return Slot.Weapon;

            case Name.NoCoating:        return Slot.Armour;
            case Name.MetalCoating:     return Slot.Armour;
            case Name.SpikeCoating:     return Slot.Armour;
            case Name.GlassCoating:     return Slot.Armour;

            case Name.RegularTyres:     return Slot.Wheels;
            case Name.RubberTyres:      return Slot.Wheels;
            case Name.OffRoadTyres:     return Slot.Wheels;
            case Name.CrystalTyres:     return Slot.Wheels;

            case Name.BladedWeaponry:   return Slot.Boosters;
            case Name.MaggotFarm:       return Slot.Boosters;
            case Name.Lightweight:      return Slot.Boosters;
            case Name.HerbMaster:       return Slot.Boosters;
            case Name.AttackMeSign:     return Slot.Boosters;
        }
    }

    //finds the appropriate weapon number of the weapon
    public int GetWeaponNumber()
    {
        switch(attachmentName)
        {
            default:
            case Name.MachineGun:       return 1;
            case Name.PivotHammer:      return 2;
            case Name.EaterBlades:      return 3;
            case Name.Sniper:           return 4;
            case Name.SpikeBomb:        return 5;
        }
    }
}
