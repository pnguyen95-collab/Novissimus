using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneControl : MonoBehaviour
{
    
    public int level;
    // Start is called before the first frame update
    void Start()
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

    

    public void SceneTo(string x)
    {
        
        SceneManager.LoadScene(x);
    }

    public void SceneToGenerateByLevelNum(string map)
    {
        int temp = level;
        string x;
        x = map + "_" + temp;
        SceneManager.LoadScene(x);
    }

    public void ExitGame()
    {
        
        Application.Quit();
    }
}
