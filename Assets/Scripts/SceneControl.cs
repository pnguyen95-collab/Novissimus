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
        //It costs 8 Fuel to traverse to the desert & reset level number
        playerData.GetComponent<PlayerData>().levelNum = 0;
        playerData.GetComponent<PlayerData>().inventory.RemoveItem(new Item { type = Item.Type.Fuel, amount = 8 });

        //mapType determines what you'll spawn on, 1 for combat & 2 for resource
        int mapType = 0;

        //check for if you currently have the story event
        if (playerData.GetComponent<PlayerData>().gameProgression.Contains(Progression.FirstStoryEvent))
        {
            //odds to encounter combat/resource map at 50/50
            float randValue = Random.value;

            //check odds
            if (randValue <= 0.5f) //go to combat
            {
                //random combat map
                int temp = Random.Range(1, 5);
                playerData.GetComponent<PlayerData>().levelNum = temp;

                mapType = 1;
            }
            else //go to resource
            {
                mapType = 2;
            }

        }
        else
        {
            //odds to encounter combat/resource map at 40/40 and story event at 20
            float randValue = Random.value;

            //check odds
            if (randValue <= 0.4f) //go to combat
            {
                //random combat map
                int temp = Random.Range(1, 5);
                playerData.GetComponent<PlayerData>().levelNum = temp;

                mapType = 1;
            }
            else if (randValue <= 0.8f) //go to resource
            {
                mapType = 2;
            }
            else //story event & adds progression
            {
                //blahblah
                playerData.GetComponent<PlayerData>().gameProgression.Add(Progression.FirstStoryEvent);
                playerData.GetComponent<PlayerData>().gameProgression.Add(Progression.StoryEvent);
            }

        }

        if (mapType == 1)
        {
            SceneManager.LoadScene("Desert_1");
        }
        else if (mapType == 2)
        {
            SceneManager.LoadScene("Desert_Resource");
        }
        else
        {
            mapType = 0;
        }

    }

    //currently only one story event
    public void SceneToEvent()
    {
        playerData.GetComponent<PlayerData>().levelNum = 5;

        SceneManager.LoadScene("Desert_1");
    }

    public void ExitGame()
    {
        
        Application.Quit();
    }
}
