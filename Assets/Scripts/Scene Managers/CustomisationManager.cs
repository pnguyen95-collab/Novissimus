using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationManager : MonoBehaviour
{
    public Inventory inventory;
    public GameObject playerData;

    public bool selected;
    public int selectedVehicle;

    public GameObject displayInfo;
    public string vehicleName;
    public InputField nameInput;

    public List<string> weaponList;
    public List<Attachments> weapon = new List<Attachments>();
    public List<string> armourList;
    public List<Attachments> armour = new List<Attachments>();
    public List<string> wheelList;
    public List<Attachments> wheels = new List<Attachments>();
    public List<string> boosterList;
    public List<Attachments> booster = new List<Attachments>();
    public Dropdown tempDropdown;

    public Dropdown weaponSelection;
    public Dropdown armourSelection;
    public Dropdown wheelSelection;
    public Dropdown booster1Selection;
    public Dropdown booster2Selection;

    public GameObject currentWeapon;
    public GameObject currentArmour;
    public GameObject currentWheels;
    public GameObject currentBooster1;
    public GameObject currentBooster2;

    public GameObject displayStats;
    public GameObject attackPattern;
    public GameObject damageDisplay;
    public GameObject healthDisplay;
    public GameObject speedDisplay;
    public GameObject booster1Display;
    public GameObject booster2Display;

    public GameObject alertText;

    private void Start()
    {
        //finds player inventory and vehicle stats
        if (GameObject.Find("PlayerInventory") != null)
        {
            playerData = GameObject.Find("PlayerInventory");

            inventory = playerData.GetComponent<PlayerData>().inventory;
        }
        else
        {
            print("Missing Inventory object");
        }

        PopulateLists();
    }

    private void Update()
    {
        //update player name
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (nameInput.text != "")
            {
                vehicleName = nameInput.text;
                playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].vehicleName = vehicleName;
                UpdateStats();

                StartCoroutine(PopupText("You have changed this vehicle's name to " + vehicleName));

                vehicleName = "";
            }
        }
    }

    //checks what options are available for the player to equip and displays them in the dropboxes
    public void PopulateLists()
    {
        //checking for weapons
        foreach (Attachments attach in inventory.attachmentList)
        {
            if (attach.GetSlot() == Attachments.Slot.Weapon)
            {
                weapon.Add(attach);
                weaponList.Add(attach.GetName());
            }
        }

        //checking for armours
        foreach (Attachments attach in inventory.attachmentList)
        {
            if (attach.GetSlot() == Attachments.Slot.Armour)
            {
                armour.Add(attach);
                armourList.Add(attach.GetName());
            }
        }

        //checking for wheels
        foreach (Attachments attach in inventory.attachmentList)
        {
            if (attach.GetSlot() == Attachments.Slot.Wheels)
            {
                wheels.Add(attach);
                wheelList.Add(attach.GetName());
            }
        }

        //checking for boosters
        foreach (Attachments attach in inventory.attachmentList)
        {
            if (attach.GetSlot() == Attachments.Slot.Boosters)
            {
                booster.Add(attach);
                boosterList.Add(attach.GetName());
            }
        }

        //adds options to the list
        weaponSelection.AddOptions(weaponList);
        armourSelection.AddOptions(armourList);
        wheelSelection.AddOptions(wheelList);
        booster1Selection.AddOptions(boosterList);
        booster2Selection.AddOptions(boosterList);
    }

    //what happens when you change the equipment in the dropdown box
    public void DropdownSelection(int index)
    {
        //finds what dropdown is currently selected
        tempDropdown = GameObject.Find("Dropdown List").transform.parent.gameObject.GetComponent<Dropdown>();

        UpdateStats();
    }

    //updates the current stats of the selected vehicle
    public void UpdateStats()
    {
        //checks to see if you have a vehicle selected
        if (selected == true)
        {
            if (tempDropdown != null)
            {
                if (tempDropdown.name == "WeaponSelection")
                {
                    playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].ChangeEquipment(weapon[tempDropdown.value]);
                }
                if (tempDropdown.name == "ArmourSelection")
                {
                    playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].ChangeEquipment(armour[tempDropdown.value]);
                }
                if (tempDropdown.name == "Wheel")
                {
                    playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].ChangeEquipment(wheels[tempDropdown.value]);
                }
                if (tempDropdown.name == "BoosterSelection1")
                {
                    playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].ChangeEquipment(booster[tempDropdown.value]);
                }
                if (tempDropdown.name == "BoosterSelection2")
                {
                    playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].ChangeEquipment(booster[tempDropdown.value]);
                }
            }

            //changes the current equipment and stat values
            playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].UpdatePlayerStats();

            //displays the updated info
            currentWeapon.GetComponent<Text>().text = "Weapon: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentWeapon.GetName();
            currentArmour.GetComponent<Text>().text = "Armour: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentArmour.GetName();
            currentWheels.GetComponent<Text>().text = "Wheels: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentWheels.GetName();

            if (playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentBooster1 != null)
            {
                currentBooster1.GetComponent<Text>().text = "Booster #1: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentBooster1.GetName();
            }
            else
            {
                currentBooster1.GetComponent<Text>().text = "Booster #1: None";
            }

            if (playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentBooster2 != null)
            {
                currentBooster2.GetComponent<Text>().text = "Booster #2: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentBooster2.GetName();
            }
            else
            {
                currentBooster1.GetComponent<Text>().text = "Booster #2: None";
            }
            

            //attack pattern
            damageDisplay.GetComponent<Text>().text = "Damage Dealt: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].damage.GetValue();
            healthDisplay.GetComponent<Text>().text = "Health: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].maxHealth.GetValue();
            speedDisplay.GetComponent<Text>().text = "Speed: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].moveSpeed.GetValue();
            //boosterdisplay
        }
        else
        {
            StartCoroutine(PopupText("Please select a vehicle first!"));
        }

    }

    public void SelectVehicle(GameObject select)
    {
        //null check
        if (select != null)
        {
            selectedVehicle = int.Parse(select.GetComponentInChildren<Button>().transform.name);
            selected = true;

            //changes the color of the selected tab and resets the other tabs colors
            GameObject[] tabs;
            tabs = GameObject.FindGameObjectsWithTag("Tab");

            foreach (GameObject obj in tabs)
            {
                obj.transform.GetComponent<Image>().color = new Color32(48, 48, 48, 255);
            }

            select.transform.GetComponent<Image>().color = new Color32(54, 63, 74, 255);

            UpdateStats();

            //displays info and wipes the name input
            displayInfo.SetActive(true);
            displayStats.SetActive(true);

            if (playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].vehicleName != "")
            {
                nameInput.text = playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].vehicleName;
            }
            else
            {
                nameInput.text = "";
            }
        }
        else
        {
            Debug.Log("error, not selectable");
        }
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
