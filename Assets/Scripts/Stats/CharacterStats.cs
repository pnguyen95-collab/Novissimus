using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour
{
    public int vehicleNumber;
    public string vehicleName;
    public bool isPlayer;

    public List<Attachments> currentlyEquipped = new List<Attachments>();
    public Attachments currentWeapon;
    public Attachments currentArmour;
    public Attachments currentWheels;
    public Attachments currentBooster1;
    public Attachments currentBooster2;

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
        currentHealth = maxHealth.GetValue();

        checkGotAttack = false;

        transform.name = vehicleName;

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
        //checks if booster
        if (newEquip.GetSlot() == Attachments.Slot.Boosters)
        {
            //checks to see if booster slots are empty
            if (currentBooster1 == null && currentBooster2 != null)
            {
                foreach (Attachments equipped in currentlyEquipped)
                {
                    //checks to see if it isn't already equipped
                    if (newEquip != equipped)
                    {
                        //remove current equipped attachment and add the new attachment in
                        currentlyEquipped.Remove(equipped);
                        currentlyEquipped.Add(newEquip);
                    }
                }
            }
            else if (currentBooster2 == null && currentBooster1 != null)
            {
                foreach (Attachments equipped in currentlyEquipped)
                {
                    //checks to see if it isn't already equipped
                    if (newEquip != equipped)
                    {
                        //remove current equipped attachment and add the new attachment in
                        currentlyEquipped.Remove(equipped);
                        currentlyEquipped.Add(newEquip);
                    }
                }
            }
            else
            {
                print("You cannot equip any more boosters!");
            }
        }
        else
        {
            //checks currently equipped for the same type of attachment
            foreach (Attachments equipped in currentlyEquipped)
            {
                if (newEquip.GetSlot() == equipped.GetSlot())
                {
                    //checks to see if it isn't already equipped
                    if (newEquip != equipped)
                    {
                        //remove current equipped attachment and add the new attachment in
                        currentlyEquipped.Remove(equipped);
                        currentlyEquipped.Add(newEquip);
                    }
                }
            }
        }
    }

    public void UpdatePlayerStats()
    {
        //remove all old multipliers and get new multipliers on all equipped items and adjust vehicle values accordingly
        damage.ResetModifier();
        maxHealth.ResetModifier();
        moveSpeed.ResetModifier();

        //weapons
        foreach (Attachments equipped in currentlyEquipped)
        {
            if (equipped.GetSlot() == Attachments.Slot.Weapon)
            {
                damage.AddModifier(equipped.GetModifiers());
                weaponNumber = equipped.GetWeaponNumber();

                currentWeapon = equipped;
            }
        }
        //armour
        foreach (Attachments equipped in currentlyEquipped)
        {
            if (equipped.GetSlot() == Attachments.Slot.Armour)
            {
                maxHealth.AddModifier(equipped.GetModifiers());

                currentArmour = equipped;
            }
        }
        //wheels
        foreach (Attachments equipped in currentlyEquipped)
        {
            if (equipped.GetSlot() == Attachments.Slot.Wheels)
            {
                moveSpeed.AddModifier(equipped.GetModifiers());

                currentWheels = equipped;
            }
        }
        //boosters
        foreach (Attachments equipped in currentlyEquipped)
        {
            if (equipped.GetSlot() == Attachments.Slot.Boosters)
            {
                switch(equipped.attachmentName)
                {
                    default:
                    case Attachments.Name.BladedWeaponry:
                        damage.AddModifier(equipped.GetModifiers());
                        if (currentBooster1 == null)
                        {
                            currentBooster1 = equipped;
                        }
                        else
                        {
                            currentBooster2 = equipped;
                        }
                        break;

                    case Attachments.Name.MaggotFarm:
                        moveSpeed.AddModifier(equipped.GetModifiers());
                        if (currentBooster1 == null)
                        {
                            currentBooster1 = equipped;
                        }
                        else
                        {
                            currentBooster2 = equipped;
                        }
                        break;

                    case Attachments.Name.Lightweight:
                        maxHealth.AddModifier(equipped.GetModifiers());
                        if (currentBooster1 == null)
                        {
                            currentBooster1 = equipped;
                        }
                        else
                        {
                            currentBooster2 = equipped;
                        }
                        break;
                }
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
