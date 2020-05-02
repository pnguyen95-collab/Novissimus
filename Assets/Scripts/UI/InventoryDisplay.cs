using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public Inventory inventory;

    //text for each resource
    public Text ironAmount;
    public Text steelAmount;
    public Text brassAmount;
    public Text rubberAmount;
    public Text plasticAmount;
    public Text nylonAmount;
    public Text wireAmount;
    public Text circuitAmount;
    public Text chipAmount;
    public Text scrapAmount;

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
    }

    // Update is called once per frame
    void Update()
    {
        //resource display
        if (inventory != null)
        {
            //checks if you have any of each of the resources and displays the amount you have
            int tempIron = inventory.CheckItem(new Item { type = Item.Type.Iron });
            int tempSteel = inventory.CheckItem(new Item { type = Item.Type.Steel });
            int tempBrass = inventory.CheckItem(new Item { type = Item.Type.Brass });
            int tempRubber = inventory.CheckItem(new Item { type = Item.Type.Rubber });
            int tempPlastic = inventory.CheckItem(new Item { type = Item.Type.Plastic });
            int tempNylon = inventory.CheckItem(new Item { type = Item.Type.Nylon });
            int tempWires = inventory.CheckItem(new Item { type = Item.Type.Wires });
            int tempCircuit = inventory.CheckItem(new Item { type = Item.Type.CircuitBoard });
            int tempChip = inventory.CheckItem(new Item { type = Item.Type.ElectronicChip });
            int tempScrap = inventory.CheckItem(new Item { type = Item.Type.Scrap });

            ironAmount.text = "Iron - " + tempIron;
            steelAmount.text = "Steel - " + tempSteel;
            brassAmount.text = "Brass - " + tempBrass;
            rubberAmount.text = "Rubber - " + tempRubber;
            plasticAmount.text = "Plastic - " + tempPlastic;
            nylonAmount.text = "Nylon - " + tempNylon;
            wireAmount.text = "Wires - " + tempWires;
            circuitAmount.text = "Circuit Board - " + tempCircuit;
            chipAmount.text = "Electronic Chip - " + tempChip;
            scrapAmount.text = "Scrap - " + tempScrap;
        }
    }
}
