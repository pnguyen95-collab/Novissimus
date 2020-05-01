using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneControl : MonoBehaviour
{
    
    public int level;
    private GameObject playerData;
    // Start is called before the first frame update
    void Start()
    {
       
        //INVENTORY
        if (GameObject.Find("PlayerInventory") != null)
        {
            playerData = GameObject.Find("PlayerInventory");
            level = playerData.GetComponent<PlayerData>().levelNum;
            
        }
        else
        {
            print("Missing Inventory object");
        }
    }

    

    public void SceneTo(string x)
    {
        
        SceneManager.LoadScene(x);
    }

    public void SceneToDesert()
    {
        //random levelnum 1-4
        //5 for event 
        int temp = Random.Range(1,5);

        playerData.GetComponent<PlayerData>().levelNum = temp;
        string x;
        x = "Desert_1";
        SceneManager.LoadScene(x);
    }

    public void ExitGame()
    {
        
        Application.Quit();
    }
}
