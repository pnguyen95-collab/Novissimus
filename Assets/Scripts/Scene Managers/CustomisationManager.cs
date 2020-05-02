using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationManager : MonoBehaviour
{
    private GameObject audio;

    public Inventory inventory;
    public GameObject playerData;

    public bool selected;
    public int selectedVehicle;

    public GameObject displayInfo;
    public string vehicleName;
    public InputField nameInput;

    public List<Attachments> weapon = new List<Attachments>();
    public List<Attachments> armour = new List<Attachments>();
    public List<Attachments> wheels = new List<Attachments>();
    public List<Attachments> booster = new List<Attachments>();

    public GameObject weaponSelection;
    public GameObject armourSelection;
    public GameObject wheelSelection;
    public GameObject booster1Selection;
    public GameObject booster2Selection;
    public GameObject equipIconPrefab;

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
    public GameObject booster1Name;
    public GameObject booster1Description;
    public GameObject booster2Display;
    public GameObject booster2Name;
    public GameObject booster2Description;

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

        if (GameObject.FindGameObjectsWithTag("Audio") != null)
        {
            audio = GameObject.FindGameObjectWithTag("Audio");
        }
        else
        {
            print("audio manager missing");
        }
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

    //checks what options are available for the player to equip and displays them in the selection
    public void PopulateLists()
    {
        //checking for weapons
        foreach (Attachments attach in inventory.attachmentList)
        {
            if (attach.GetSlot() == Attachments.Slot.Weapon)
            {
                weapon.Add(attach);
            }
        }

        //checking for armours
        foreach (Attachments attach in inventory.attachmentList)
        {
            if (attach.GetSlot() == Attachments.Slot.Armour)
            {
                armour.Add(attach);
            }
        }

        //checking for wheels
        foreach (Attachments attach in inventory.attachmentList)
        {
            if (attach.GetSlot() == Attachments.Slot.Wheels)
            {
                wheels.Add(attach);
            }
        }

        //checking for boosters
        foreach (Attachments attach in inventory.attachmentList)
        {
            if (attach.GetSlot() == Attachments.Slot.Boosters)
            {
                booster.Add(attach);
            }
        }

        UpdateEquipIcons(Attachments.Slot.Weapon);
        UpdateEquipIcons(Attachments.Slot.Armour);
        UpdateEquipIcons(Attachments.Slot.Wheels);
        UpdateEquipIcons(Attachments.Slot.Boosters);
    }

    //function to update equippable icons
    public void UpdateEquipIcons(Attachments.Slot slot)
    {
        //switch to find the current selection & attachment type
        GameObject selection;
        List<Attachments> type;
        bool isBooster = false;

        switch(slot)
        {
            default:
            case Attachments.Slot.Weapon:
                selection = weaponSelection;
                type = weapon;
                break;

            case Attachments.Slot.Armour:
                selection = armourSelection;
                type = armour;
                break;

            case Attachments.Slot.Wheels:
                selection = wheelSelection;
                type = wheels;
                break;

            case Attachments.Slot.Boosters:
                selection = booster1Selection;
                type = booster;
                isBooster = true;
                break;
        }

        //clears out all currently instantiated in the selection
        if (isBooster == true)
        {
            foreach (Transform child in booster2Selection.transform)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (Transform child in selection.transform)
        {
            Destroy(child.gameObject);
        }

        //instantiate all equip icons in the selection
        if (isBooster == true)
        {
            foreach (Attachments equippable in type)
            {
                CreateIcon(equippable, booster2Selection);
            }
        }

        foreach (Attachments equippable in type)
        {
            CreateIcon(equippable, selection);
        }
    }

    //function to create equip icons
    public void CreateIcon(Attachments equippable, GameObject selection)
    {
        GameObject icon = Instantiate(equipIconPrefab, selection.transform);

        icon.transform.GetChild(0).GetComponent<Image>().sprite = equippable.GetSprite();

        //when you click on the button you equip it
        icon.GetComponent<Button>().onClick.AddListener(() => ClickEquip(equippable, selection));
    }

    //function when you click on an equip icon
    public void ClickEquip(Attachments equippable, GameObject selection)
    {
        //checks to see if equippable is a booster
        if (equippable.GetSlot() == Attachments.Slot.Boosters)
        {
            //checks to see which booster slot it is
            if (selection == booster1Selection)
            {
                //checks to see if you already have that booster equipped
                if (playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[3] != equippable && playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[4] != equippable)
                {
                    //equips the booster to Slot 1
                    playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[3] = equippable;

                    UpdateStats();
                }
                else
                {
                    StartCoroutine(PopupText("You already have this equipped!"));
                }
            }
            else if (selection == booster2Selection)
            {
                //checks to see if you already have that booster equipped
                if (playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[3] != equippable && playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[4] != equippable)
                {
                    //equips the booster to Slot 2
                    playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[4] = equippable;

                    UpdateStats();
                }
                else
                {
                    StartCoroutine(PopupText("You already have this equipped!"));
                }
            }
        }
        else
        {
            //checks if you already have the attachment equipped
            foreach (Attachments check in playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped)
            {
                if (check == equippable)
                {
                    StartCoroutine(PopupText("You already have this equipped!"));
                }
                else
                {
                    //equip the item
                    playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].ChangeEquipment(equippable);

                    UpdateStats();
                }
            }
        }
    }

    //updates the current stats of the selected vehicle
    public void UpdateStats()
    {
        //checks to see if you have a vehicle selected
        if (selected == true)
        {
            //changes the current equipment and stat values
            playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].UpdatePlayerStats();

            //displays the updated info
            currentWeapon.GetComponent<Text>().text = "Weapon: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[0].GetName();
            currentArmour.GetComponent<Text>().text = "Armour: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[1].GetName();
            currentWheels.GetComponent<Text>().text = "Wheels: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[2].GetName();

            //attack pattern
            attackPattern.GetComponent<Image>().sprite = playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[0].GetPattern();

            //damage display
            if (playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[0].attachmentName == Attachments.Name.PivotHammer)
            {
                damageDisplay.GetComponent<Text>().text = "Damage Dealt: 3 - 6";
            }
            else
            {
                damageDisplay.GetComponent<Text>().text = "Damage Dealt: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].damage.GetValue();
            }

            //health display
            healthDisplay.GetComponent<Text>().text = "Health: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].maxHealth.GetValue();

            //speed display
            speedDisplay.GetComponent<Text>().text = "Speed: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].moveSpeed.GetValue();

            //boosterdisplay

            if (playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[3] != null)
            {
                currentBooster1.GetComponent<Text>().text = "Booster #1: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[3].GetName();

                //displays booster info
                booster1Display.SetActive(true);

                booster1Name.GetComponent<Text>().text = playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[3].GetName();
                booster1Description.GetComponent<Text>().text = playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[3].GetDescription();
            }
            else
            {
                currentBooster1.GetComponent<Text>().text = "Booster #1: None";
                booster1Display.SetActive(false);
            }

            if (playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[4] != null)
            {
                currentBooster2.GetComponent<Text>().text = "Booster #2: " + playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[4].GetName();

                //displays booster info
                booster2Display.SetActive(true);

                booster2Name.GetComponent<Text>().text = playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[4].GetName();
                booster2Description.GetComponent<Text>().text = playerData.GetComponent<PlayerData>().playerVehicle[selectedVehicle].currentlyEquipped[4].GetDescription();
            }
            else
            {
                currentBooster2.GetComponent<Text>().text = "Booster #2: None";
                booster2Display.SetActive(false);
            }

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

            if(audio!=null)
            audio.GetComponent<AudioController>().PlayButtonClick();
            
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
