using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//enum of current progression of player in the narrative
public enum Progression
{
    Starting,
    FirstStoryEvent,
    PlainsUnlocked,
    SecondStoryEvent,
    CannibalBossFight,
    StoryEvent
}

public class PlayerData : MonoBehaviour
{
    public int levelNum;
    //hashset of game progression elements to check
    public HashSet<Progression> gameProgression = new HashSet<Progression>();

    //array of player vehicle's stats
    public List<CharacterStats> playerVehicle = new List<CharacterStats>();

    public Inventory inventory;

    private static GameObject instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        levelNum = 1;
        inventory = new Inventory();
        gameProgression.Add(Progression.Starting);

        //base attachments added
        inventory.attachmentList.Add(new Attachments { attachmentName = Attachments.Name.MachineGun});
        inventory.attachmentList.Add(new Attachments { attachmentName = Attachments.Name.NoCoating});
        inventory.attachmentList.Add(new Attachments { attachmentName = Attachments.Name.RegularTyres});
        inventory.attachmentList.Add(new Attachments { attachmentName = Attachments.Name.BladedWeaponry });
        inventory.attachmentList.Add(new Attachments { attachmentName = Attachments.Name.MaggotFarm });
        inventory.attachmentList.Add(new Attachments { attachmentName = Attachments.Name.Lightweight });

        //base resources added
        inventory.AddItem(new Item { type = Item.Type.Fuel, amount = 500 });

        //set up the base equipment for each vehicle
        foreach (CharacterStats vehicle in playerVehicle)
        {
            vehicle.currentlyEquipped[0] = new Attachments { attachmentName = Attachments.Name.MachineGun };
            vehicle.currentlyEquipped[1] = new Attachments { attachmentName = Attachments.Name.NoCoating };
            vehicle.currentlyEquipped[2] = new Attachments { attachmentName = Attachments.Name.RegularTyres };

            vehicle.UpdatePlayerStats();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CheckInInventory();
        }

        //testing grant players resources
        if (Input.GetKeyDown(KeyCode.T))
        {
            inventory.AddItem(new Item { type = Item.Type.Fuel, amount = 3 });
            inventory.AddItem(new Item { type = Item.Type.Scrap, amount = 5 });

            inventory.AddItem(new Item { type = Item.Type.Iron, amount = 3 });
            inventory.AddItem(new Item { type = Item.Type.Steel, amount = 2 });
            inventory.AddItem(new Item { type = Item.Type.Brass, amount = 1 });
            inventory.AddItem(new Item { type = Item.Type.Rubber, amount = 3 });
            inventory.AddItem(new Item { type = Item.Type.Plastic, amount = 3 });
            inventory.AddItem(new Item { type = Item.Type.Nylon, amount = 1 });
            inventory.AddItem(new Item { type = Item.Type.Wires, amount = 5 });
            inventory.AddItem(new Item { type = Item.Type.CircuitBoard, amount = 1 });
            inventory.AddItem(new Item { type = Item.Type.ElectronicChip, amount = 1 });
        }
    }

    public void CheckInInventory()
    {
         if (inventory.inventoryList.Count >= 1)
            {
                for (int i = 0; i < inventory.inventoryList.Count; i++)
                {
                    print("You have " + inventory.inventoryList[i].amount + " " + inventory.inventoryList[i].type + " in your inventory.");
                }
            }
            else
            {
                print("Your inventory is empty :(");
            }

        if (inventory.attachmentList.Count >= 1)
        {
            for (int i = 0; i <inventory.attachmentList.Count; i++)
            {
                print("You have crafted " + inventory.attachmentList[i].attachmentName);
            }
        }
        else
        {
            print("You have not crafted anything yet");
        }
    }
}
