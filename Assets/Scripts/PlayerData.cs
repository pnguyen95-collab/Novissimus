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
    //hashset of game progression elements to check
    public HashSet<Progression> gameProgression = new HashSet<Progression>();

    public Inventory inventory;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        inventory = new Inventory();
        gameProgression.Add(Progression.Starting);

        //base attachments added
        inventory.AddAttachment(new Attachments { attachmentName = Attachments.Name.MachineGun});
        inventory.AddAttachment(new Attachments { attachmentName = Attachments.Name.NoCoating});
        inventory.AddAttachment(new Attachments { attachmentName = Attachments.Name.RegularTyres});
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
