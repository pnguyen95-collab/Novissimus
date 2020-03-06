using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
