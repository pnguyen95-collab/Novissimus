using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> inventoryList;
    public List<Attachments> attachmentList;

    public Inventory()
    {
        inventoryList = new List<Item>();
        attachmentList = new List<Attachments>();
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

    //function used to remove items from your inventory
    public void RemoveItem(Item item)
    {
        //checks to see if the item to remove is greater than 0
        if (item.amount > 0)
        {
            //checks your inventory for the item and removes that amount
            foreach (Item itemCheck in inventoryList)
            {
                if (itemCheck.type == item.type)
                {
                    //checks to see if you have enough of the item or else does nothing
                    if (itemCheck.amount >= item.amount)
                    {
                        itemCheck.amount -= item.amount;

                        //if the amount is now equal to 0, remove from inventoryList
                        if (itemCheck.amount == 0)
                        {
                            inventoryList.Remove(itemCheck);
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("You must define a value to remove that's greater than 0");
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
