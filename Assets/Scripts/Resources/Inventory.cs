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
}
