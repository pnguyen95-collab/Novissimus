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

    public List<Attachments> craftList;

    public GameObject requirementPanel;
    public GameObject displayName;
    public GameObject requirementList;
    public GameObject displaySprite;

    public Attachments selected;
    public GameObject craftButton;

    public GameObject alertText;

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

        RefreshTable();
    }

    //function to refresh the crafting table
    public void RefreshTable()
    {
        //clears out all instantiated buttons
        foreach (Transform child in GameObject.Find("Content").transform)
        {
            Destroy(child.gameObject);
        }

        craftList = new List<Attachments>();

        //for loop to iterate over all attachments and add craftables to the crafting list
        foreach (Attachments.Name name in System.Enum.GetValues(typeof(Attachments.Name)))
        {
            Attachments checkAttach = new Attachments { attachmentName = name };

            if (checkAttach.CheckCraftable() == true)
            {
                craftList.Add(new Attachments { attachmentName = name });
            }
        }

        //for loop to create the list of craftables buttons
        for (int i = 0; i < craftList.Count; i++)
        {
            bool alreadyHave = false;

            //checks to see if you already have the attachment crafted
            foreach (Attachments attach in inventory.attachmentList)
            {
                //if you already have crafted
                if (attach.attachmentName == craftList[i].attachmentName)
                {
                    InstanceCrafted(new Attachments { attachmentName = craftList[i].attachmentName });
                    alreadyHave = true;
                }
            }

            if (!alreadyHave)
            {
                //creates a craftable button for the attachment
                InstanceCraftable(new Attachments { attachmentName = craftList[i].attachmentName });
            }
        }
    }

    public void InstanceCraftable(Attachments attach)
    {
        GameObject craftable = Instantiate(craftablePrefab, craftingPanel.transform);

        craftable.GetComponentInChildren<Text>().text = attach.GetName();

        //when you click on the button it parses through the attachment's info
        craftable.GetComponent<Button>().onClick.AddListener(() => ClickCraftable(attach));
    }

    public void InstanceCrafted(Attachments attach)
    {
        GameObject crafted = Instantiate(craftedPrefab, craftingPanel.transform);

        //when you click on an already crafted attachment
        crafted.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(PopupText("You have already crafted the " + attach.GetName() + "!")));
    }

    //function called when you click on a craftable/crafted attachment's button
    public void ClickCraftable(Attachments attach)
    {
        //show the panel and updates all the name and sprites
        requirementPanel.SetActive(true);
        selected = attach;
        displayName.GetComponent<Text>().text = attach.GetName();
        displaySprite.GetComponent<Image>().sprite = attach.GetSprite();

        //gets the list of requirements of said attachment
        List<Item> req = attach.GetCraftRequirement();
        string displayRequirements = "";

        foreach (Item i in req)
        {
            displayRequirements += i.amount + "x " + i.type + "\n";
        }

        requirementList.GetComponent<Text>().text = displayRequirements;
    }
         
    public void CheckCraft()
    {
        //checks to see if you have a selected attachment first
        if (selected != null)
        {
            bool craftable = false;
            int hasItem = 0;

            //for loop to check each item in the requirement list and if you have the item
            for (int i = 0; i < selected.GetCraftRequirement().Count; i++)
            {
                foreach (Item itemCheck in inventory.inventoryList)
                {
                    if (itemCheck.type == selected.GetCraftRequirement()[i].type)
                    {
                        //checks to see if you have enough of the resource
                        if (itemCheck.amount >= selected.GetCraftRequirement()[i].amount)
                        {
                            hasItem++;

                            //final check to make sure you have every component
                            if (hasItem == selected.GetCraftRequirement().Count)
                            {
                                CraftAttachment(selected);
                                StartCoroutine(PopupText("You have successfully crafted " + selected.GetName() + "!"));
                                craftable = true;
                            }
                        }
                    }
                }
            }

            //if the item is not craftable
            if (!craftable)
            {
                StartCoroutine(PopupText("You are missing the resources required to craft this attachment!"));
            }
        }
        else
        {
            StartCoroutine(PopupText("You must select an attachment first!"));
        }
    }

    public void CraftAttachment(Attachments attach)
    {
        //removes components from player's inventory
        for (int i = 0; i < attach.GetCraftRequirement().Count; i++)
        {
            foreach (Item itemCheck in inventory.inventoryList)
            {
                if (itemCheck.type == selected.GetCraftRequirement()[i].type)
                {
                    inventory.RemoveItem(selected.GetCraftRequirement()[i]);
                }
            }
        }

        inventory.attachmentList.Add(attach);

        RefreshTable();
    }

    public IEnumerator PopupText(string x)
    {
        print("doing pop up text");
        alertText.SetActive(true);
        alertText.GetComponent<Text>().text = x;

        alertText.GetComponent<Text>().canvasRenderer.SetAlpha(1);
        alertText.GetComponent<Text>().CrossFadeAlpha(0.0f, 2.5f, false);
        yield return new WaitForSeconds(2f);
        alertText.SetActive(false);
        alertText.GetComponent<Text>().canvasRenderer.SetAlpha(1);
    }
}
