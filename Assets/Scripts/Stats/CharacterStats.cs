using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat damage;
    public Stat moveSpeed;
    public Stat attackRange;
    public int weaponNumber; //must assign
    public bool checkGotAttack;

    public int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth.GetValue();
        checkGotAttack = false;
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
        Debug.Log(transform.name + " is destroyed.");
    }

    private IEnumerator GotAttacked()
    {

        checkGotAttack = true;

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1.5f);

        //After we have waited 5 seconds print the time again.
        checkGotAttack = false;
    }
}
