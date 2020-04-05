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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CheckInInventory();
        }

        //testing grant players scrap & fuel
        if (Input.GetKeyDown(KeyCode.T))
        {
            inventory.AddItem(new Item { type = Item.Type.Fuel, amount = 3 });
            inventory.AddItem(new Item { type = Item.Type.Scrap, amount = 5 });
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

        
    }
}
