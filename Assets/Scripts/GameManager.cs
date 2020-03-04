using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] players;
    public bool runRaycast;
    public GameObject currentPlayer;
    public GameObject menuPanel;
    public GridBehavior gridBehaviorCode;
    public int turnStatus; // 0 = player , 1 = enemy
    public int numOfPlayer;
    public int countNumOfPlayer;
    public int numOfEnemy;
    public int countNumOfEnemy;

    // Start is called before the first frame update
    void Start()
    {
        gridBehaviorCode = this.GetComponent<GridBehavior>();
        runRaycast = false;
        menuPanel = GameObject.Find("PlayerMenuPanel");
        setOnOffMenu(false);
        turnStatus = 0;

        players = GameObject.FindGameObjectsWithTag("Player");
        numOfPlayer = players.Length;
        numOfEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        countNumOfEnemy = 0;
        countNumOfPlayer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        checkTurn(); 

        if (runRaycast == true)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            if (Physics.Raycast(ray, out hit))
            {
                //   Debug.Log("This hit at " + hit.transform.name);
                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                        return;


                    if (hit.transform.tag == "Block")
                    {
                        GameObject temp = hit.transform.gameObject;
                        if (temp.GetComponent<GridStat>().walkAble == true)
                        {

                            currentPlayer.GetComponent<PlayerBehavior>().PlayerMove(temp.GetComponent<GridStat>().x, temp.GetComponent<GridStat>().y);
                           
                            runRaycast = false;
                        }
                        else
                        {
                            print("cannot walk there");
                            //reset to the start
                            runRaycast = false;
                            gridBehaviorCode.resetVisit();

                        }

                    }
                }
                

            }
        
            else // If Raycast did not return anything
            {
            Debug.Log("NULL");

            }
        }

    }

    public void setCurrentPlayer(GameObject c)
    {
        currentPlayer = c;

    }
    public void setOnOffMenu(bool x)
    {
        menuPanel.SetActive(x);
    }

    public void checkTurn()
    {
        int tempPlayerNum = 0;
        int tempEnemyNum = 0;

        if (turnStatus == 0)
        {
            foreach (GameObject p in players)
            {
                if (p.GetComponent<PlayerBehavior>().playerIsPlayable == false)
                {
                    tempPlayerNum++;
                }
            }

            if (tempPlayerNum == numOfPlayer)
            {
                turnStatus = 1;
                //all enemies are active
            }
        }
        else if (turnStatus == 1)
        {
            /*
       foreach (GameObject e in enemies)
       {
           if (p.GetComponent<PlayerBehavior>().playerIsPlayable == false)
           {
               tempEnemyNum++;
           }
       }
       */

        if (countNumOfEnemy == numOfEnemy)
            {
                turnStatus = 0;
                countNumOfEnemy = 0;
                foreach (GameObject p in players)
                {
                    p.GetComponent<PlayerBehavior>().playerIsPlayable = true;
                }
            }
        }






      


       
        
    }
    
}
