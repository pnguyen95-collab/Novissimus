using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level=0;

    // Start is called before the first frame update
    void Awake()
    {
        //INVENTORY
        if (GameObject.Find("PlayerInventory") != null)
        {
            GameObject temp = GameObject.Find("PlayerInventory");


            level = temp.GetComponent<PlayerData>().levelNum;
            
        }
        else
        {
            print("Missing Inventory object");
        }

    }

// Update is called once per frame
void Update()
    {
        
    }
}
