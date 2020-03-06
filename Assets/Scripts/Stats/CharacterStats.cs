using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat damage;
    public Stat moveSpeed;
    public Stat attackRange;

    public int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth.GetValue();
    }

    //Damage Testing
     public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(2);
        }
    }

    //Damage calculation method
    public void TakeDamage (int damage)
    {
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
        Debug.Log(transform.name + " is destroyed.");
    }
}
