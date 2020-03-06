using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int limitNum;
    public int attackDamage;
    public int attackRange;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        limitNum = 5;
        attackDamage = 3;
        attackRange = 7;
    }

    
}
