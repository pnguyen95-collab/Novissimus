using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour
{
    public GameObject playerData;
    public int vehicleNumber;
    public string vehicleName;
    public bool isPlayer;

    //array of equipment (0 = weapon, 1 = armour, 2 = wheels, 3 = booster 1, 4 = booster 2)
    public Attachments[] currentlyEquipped = new Attachments[System.Enum.GetNames(typeof(Attachments.Slot)).Length + 1];

    public Stat maxHealth;
    public Stat damage;
    public Stat moveSpeed;
    public Stat attackRange;
    public int weaponNumber; //must assign
    public List<int> boosterNumber;
    public int actionPoint;
    public bool checkGotAttack;

    public int currentHealth;

    void Awake()
    {
        //finds vehicle stats
        if (GameObject.Find("PlayerInventory") != null)
        {
            playerData = GameObject.Find("PlayerInventory");
        }
        else
        {
            print("Missing player data");
        }

        checkGotAttack = false;

        transform.name = vehicleName;

        if (isPlayer == true)
        {
            this.maxHealth = playerData.GetComponent<PlayerData>().playerVehicle[vehicleNumber].maxHealth;
            this.moveSpeed = playerData.GetComponent<PlayerData>().playerVehicle[vehicleNumber].moveSpeed;
            this.weaponNumber = playerData.GetComponent<PlayerData>().playerVehicle[vehicleNumber].weaponNumber;
            this.boosterNumber = playerData.GetComponent<PlayerData>().playerVehicle[vehicleNumber].boosterNumber;
        }

        currentHealth = maxHealth.GetValue();

        foreach (int d in boosterNumber)
        {
            if (d == 3)
            {
                actionPoint = 2;
            }
            else
            {
                actionPoint = 1;
            }
        }
    }

    //function used whenever equipment gets changed
    public void ChangeEquipment(Attachments newEquip)
    {
        Attachments.Slot slot = newEquip.GetSlot();

        //checks the slot of the new attachment and equips the new equipment to the corresponding slot
        switch(slot)
        {
            default:
            case Attachments.Slot.Weapon:
                currentlyEquipped[0] = newEquip;
                break;

            case Attachments.Slot.Armour:
                currentlyEquipped[1] = newEquip;
                break;

            case Attachments.Slot.Wheels:
                currentlyEquipped[2] = newEquip;
                break;
        }
    }

    public void UpdatePlayerStats()
    {
        //remove all old multipliers and get new multipliers on all equipped items and adjust vehicle values accordingly
        damage.ResetModifier();
        maxHealth.ResetModifier();
        moveSpeed.ResetModifier();
        boosterNumber = new List<int>();

        //weapons
        damage.AddModifier(currentlyEquipped[0].GetModifiers());
        weaponNumber = currentlyEquipped[0].GetWeaponNumber();

        //armour
        maxHealth.AddModifier(currentlyEquipped[1].GetModifiers());

        //wheels
        moveSpeed.AddModifier(currentlyEquipped[2].GetModifiers());
        
        //boosters
        if (currentlyEquipped[3] != null)
        {
            switch(currentlyEquipped[3].attachmentName)
            {
                default:
                case Attachments.Name.BladedWeaponry:
                    damage.AddModifier(currentlyEquipped[3].GetModifiers());
                    boosterNumber.Add(1);
                    break;

                case Attachments.Name.MaggotFarm:
                    moveSpeed.AddModifier(currentlyEquipped[3].GetModifiers());
                    boosterNumber.Add(2);
                    break;

                case Attachments.Name.Lightweight:
                    maxHealth.AddModifier(currentlyEquipped[3].GetModifiers());
                    boosterNumber.Add(3);
                    break;
            }
        }
        
        if (currentlyEquipped[4] != null)
        {
            switch (currentlyEquipped[4].attachmentName)
            {
                default:
                case Attachments.Name.BladedWeaponry:
                    damage.AddModifier(currentlyEquipped[4].GetModifiers());
                    boosterNumber.Add(1);
                    break;

                case Attachments.Name.MaggotFarm:
                    moveSpeed.AddModifier(currentlyEquipped[4].GetModifiers());
                    boosterNumber.Add(2);
                    break;

                case Attachments.Name.Lightweight:
                    maxHealth.AddModifier(currentlyEquipped[4].GetModifiers());
                    boosterNumber.Add(3);
                    break;
            }
        }
    }

    //Damage calculation method
    public void TakeDamage (int damage)
    {
        StartCoroutine(GotAttacked());
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Destroyed();
        }
    }

    //Method called when a vehicle is destroyed
    public virtual void Destroyed()
    {
        // What happens when vehicle is destroyed
        Destroy(this.gameObject);
        GameObject.Find("GameManagement").GetComponent<GameManager>().AddMessage(this.gameObject.name + " is destroyed.", Color.white);

    }

    private IEnumerator GotAttacked()
    {
        if (GameObject.FindGameObjectsWithTag("Audio") != null)
        {
            GameObject a = GameObject.FindGameObjectWithTag("Audio");
            a.GetComponent<AudioController>().PlayGotHit();

        }
        checkGotAttack = true;

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1.5f);

        //After we have waited 5 seconds print the time again.
        checkGotAttack = false;
    }
}
