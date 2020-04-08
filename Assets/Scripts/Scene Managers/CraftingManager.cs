using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public GameObject craftablePrefab;
    public GameObject craftedPrefab;
    public GameObject craftingPanel;
    public Inventory inventory;

    public int numberOfCraftables;

    // Start is called before the first frame update
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

        //for loop to create the list of craftables buttons
        for (int i = 0; i < numberOfCraftables; i++)
        {
            InstanceCraftable();
        }
    }

    public void InstanceCraftable()
    {
        GameObject craftable = Instantiate(craftablePrefab, craftingPanel.transform);
    }
}
