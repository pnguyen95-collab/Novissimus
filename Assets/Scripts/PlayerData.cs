using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Inventory inventory;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        inventory = new Inventory();
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
