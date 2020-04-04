using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreResourceDisplay : MonoBehaviour
{
    public Inventory inventory;
    public Text scrapAmount;
    public Text fuelAmount;

    void Start()
    {
        //finds player inventory
        if (GameObject.Find("PlayerInventory") != null)
        {
            GameObject temp = GameObject.Find("PlayerInventory");

            inventory = temp.GetComponent<PlayerData>().inventory;
        }
        else
        {
            print("Missing Inventory object");
        }
    }

    // Update is called once per frame
    void Update()
    {
        print(inventory.inventoryList.Count);

        //checks to see if an inventory exists
        if (inventory != null)
        {
            //finds fuel/scrap in inventory
            for (int i = 0; i < inventory.inventoryList.Count; i++)
            {
                print("Checking Inventory");

                //checks and displays amount of fuel
                if (inventory.inventoryList[i].type == Item.Type.Fuel)
                {
                    fuelAmount.text = "Fuel: " + inventory.inventoryList[i].amount;
                }
                else
                {
                    fuelAmount.text = "Fuel: 0";
                }

                //checks and displays amount of scrap
                if (inventory.inventoryList[i].type == Item.Type.Scrap)
                {
                    scrapAmount.text = "Scrap: " + inventory.inventoryList[i].amount;
                }
                else
                {
                    scrapAmount.text = "Scrap: 0";
                }
            }
        }
    }
}
