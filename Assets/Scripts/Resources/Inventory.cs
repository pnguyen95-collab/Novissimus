using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> inventoryList;

    public Inventory()
    {
        inventoryList = new List<Item>();
    }

    //function used to add an item to your inventory
    public void AddItem(Item item)
    {
        if (inventoryList.Count > 0)
        {
            bool alreadyHave = false;

            //check to see if you already have the resource
            foreach (Item itemCheck in inventoryList)
            {
                //if you do add the amount to the total amount
                if (itemCheck.type == item.type)
                {
                    itemCheck.amount += item.amount;
                    alreadyHave = true;
                }
            }

            if (!alreadyHave)
            {
                inventoryList.Add(item);
            }
            
        }
        else
        {
            inventoryList.Add(item);
        }
    }

    //function used to check if you have an item and its quantity in your inventory - returns the int number of quantity
    public int CheckItem(Item item)
    {
        foreach (Item itemCheck in inventoryList)
        {
            if (itemCheck.type == item.type)
            {
                return itemCheck.amount;
            }
        }
        return 0;
    }
}
