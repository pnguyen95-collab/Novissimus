using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingReq : MonoBehaviour
{
    public static CraftingReq Instance { get; set; }

    //lists for resource requirements each of the craftables
    public List<Item> pivotHammerReq = new List<Item>();
    public List<Item> eaterBladesReq = new List<Item>();
    public List<Item> sniperReq = new List<Item>();
    public List<Item> spikeBombReq = new List<Item>();

    public List<Item> metalCoatingReq = new List<Item>();
    public List<Item> spikesCoatingReq = new List<Item>();
    public List<Item> glassCoatingReq = new List<Item>();

    public List<Item> rubberTyresReq = new List<Item>();
    public List<Item> offRoadTyresReq = new List<Item>();
    public List<Item> crystalTyresReq = new List<Item>();

    public List<Item> bladedWeaponryReq = new List<Item>();
    public List<Item> maggotFarmReq = new List<Item>();
    public List<Item> lightweightReq = new List<Item>();
    public List<Item> herbMasterReq = new List<Item>();
    public List<Item> attackMeSignReq = new List<Item>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //Pivot Hammer requirements
        pivotHammerReq.Add(new Item { type = Item.Type.Iron, amount = 5 });
        pivotHammerReq.Add(new Item { type = Item.Type.Steel, amount = 2 });
        pivotHammerReq.Add(new Item { type = Item.Type.Rubber, amount = 2 });

        //Eater Blades requirements
        eaterBladesReq.Add(new Item { type = Item.Type.Iron, amount = 6 });
        eaterBladesReq.Add(new Item { type = Item.Type.Steel, amount = 3 });
        eaterBladesReq.Add(new Item { type = Item.Type.Brass, amount = 1 });

        //Sniper requirements
        sniperReq.Add(new Item { type = Item.Type.Plastic, amount = 6 });
        sniperReq.Add(new Item { type = Item.Type.Steel, amount = 1 });
        sniperReq.Add(new Item { type = Item.Type.Nylon, amount = 1 });

        //Spike Bomb requirements
        spikeBombReq.Add(new Item { type = Item.Type.Steel, amount = 5 });
        spikeBombReq.Add(new Item { type = Item.Type.Plastic, amount = 3 });
        spikeBombReq.Add(new Item { type = Item.Type.ElectronicChip, amount = 1 });

        //Metal Coating requirements
        metalCoatingReq.Add(new Item { type = Item.Type.Iron, amount = 7 });
        metalCoatingReq.Add(new Item { type = Item.Type.Wires, amount = 7 });

        //Spikes Coating requirements
        spikesCoatingReq.Add(new Item { type = Item.Type.Steel, amount = 4 });
        spikesCoatingReq.Add(new Item { type = Item.Type.Wires, amount = 15 });

        //Glass Coating requirements
        glassCoatingReq.Add(new Item { type = Item.Type.Plastic, amount = 7 });
        glassCoatingReq.Add(new Item { type = Item.Type.Nylon, amount = 1 });
        glassCoatingReq.Add(new Item { type = Item.Type.Wires, amount = 15 });

        //Rubber Tyres requirements
        rubberTyresReq.Add(new Item { type = Item.Type.Rubber, amount = 5 });
        rubberTyresReq.Add(new Item { type = Item.Type.Iron, amount = 3 });

        //Off Road Tyres requirements
        offRoadTyresReq.Add(new Item { type = Item.Type.Rubber, amount = 7 });
        offRoadTyresReq.Add(new Item { type = Item.Type.Steel, amount = 2 });

        //Crystal Tyres requirements
        crystalTyresReq.Add(new Item { type = Item.Type.Rubber, amount = 12 });
        crystalTyresReq.Add(new Item { type = Item.Type.Brass, amount = 1 });

        //Bladed Weaponry requirements
        bladedWeaponryReq.Add(new Item { type = Item.Type.Iron, amount = 5 });
        bladedWeaponryReq.Add(new Item { type = Item.Type.Wires, amount = 5 });
        bladedWeaponryReq.Add(new Item { type = Item.Type.Steel, amount = 2 });
        bladedWeaponryReq.Add(new Item { type = Item.Type.Brass, amount = 1 });

        //Maggot Farm requirements
        maggotFarmReq.Add(new Item { type = Item.Type.Rubber, amount = 5 });
        maggotFarmReq.Add(new Item { type = Item.Type.Plastic, amount = 5 });
        maggotFarmReq.Add(new Item { type = Item.Type.Wires, amount = 5 });
        maggotFarmReq.Add(new Item { type = Item.Type.CircuitBoard, amount = 1 });

        //Lightweight requirements
        lightweightReq.Add(new Item { type = Item.Type.Plastic, amount = 8 });
        lightweightReq.Add(new Item { type = Item.Type.Nylon, amount = 1 });
        lightweightReq.Add(new Item { type = Item.Type.CircuitBoard, amount = 1 });

        //Herb Master requirements
        herbMasterReq.Add(new Item { type = Item.Type.Rubber, amount = 5 });
        herbMasterReq.Add(new Item { type = Item.Type.Wires, amount = 10 });
        herbMasterReq.Add(new Item { type = Item.Type.ElectronicChip, amount = 1 });
        herbMasterReq.Add(new Item { type = Item.Type.CircuitBoard, amount = 1 });

        //Attack Me Sign requirements
        attackMeSignReq.Add(new Item { type = Item.Type.Plastic, amount = 8 });
        attackMeSignReq.Add(new Item { type = Item.Type.Steel, amount = 2 });
        attackMeSignReq.Add(new Item { type = Item.Type.Brass, amount = 1 });

        print("To craft the Pivot Hammer you require:");
        foreach ( Item item in pivotHammerReq)
        {
            print(item.amount + " " + item.type);
        }
    }
}
