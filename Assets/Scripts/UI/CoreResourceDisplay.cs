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
        //checks to see if an inventory exists
        if (inventory != null)
        {
            //checks if you have any fuel/scrap and displays the amount you have
            int tempFuel = inventory.CheckItem(new Item { type = Item.Type.Fuel });
            int tempScrap = inventory.CheckItem(new Item { type = Item.Type.Scrap });

            fuelAmount.text = "Fuel: " + tempFuel;
            scrapAmount.text = "Scrap: " + tempScrap;
        }
    }
}
