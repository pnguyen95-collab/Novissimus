using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] players;
    public GameObject[] enemies;
    public bool runRaycast;
    public GameObject currentPlayer;
    public GameObject menuPanel;
    public GameObject menuPanel2;
    public GridBehavior gridBehaviorCode;
    public NodeLoot lootResources;
    public int turnStatus; // 0 = player , 1 = enemy, 2 = none
    public int numOfPlayer;
    public int countNumOfPlayer;
    public int numOfEnemy;
    public int countNumOfEnemy;

    //boolean to trigger whether or not it's a resource grid
    public bool resourceGrid = false;
    public int turnCountdown;

    // Start is called before the first frame update
    void Start()
    {
        gridBehaviorCode = this.GetComponent<GridBehavior>();
        runRaycast = false;
        menuPanel = GameObject.Find("PlayerMenuPanel");
        if (resourceGrid == false)
        {
            menuPanel2 = GameObject.Find("PlayerMenuPanel2");
        }
        setOnOffMenu(menuPanel, false);
        setOnOffMenu(menuPanel2, false);
        turnStatus = 0;

        players = GameObject.FindGameObjectsWithTag("Player");
        numOfPlayer = players.Length;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numOfEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        countNumOfEnemy = 0;
        countNumOfPlayer = 0;

        //set how many turns countdown to countdown until return to base
        if (resourceGrid == true)
        {
            turnCountdown = 10;
            Debug.Log("You have " + turnCountdown + " turns remaining!");
        }
        
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
                    GameObject temp = hit.transform.gameObject;

                    if (hit.transform.tag == "Block")
                    {
                        ClickOnBlock(temp);
                    }
                    else if (hit.transform.tag == "Player")
                    {
                        ClickOnPlayer(temp);
                    }
                    else if (hit.transform.tag == "Enemy")
                    {
                        ClickOnEnemy(temp);   
                    }
                    else
                    { print("click on something unknow"); }
                    
                }
                

            }
        
            else // If Raycast did not return anything
            {
            //Debug.Log("NULL");

            }
        }

    }

    public void setCurrentPlayer(GameObject c)
    {
        currentPlayer = c;

    }
    public void setOnOffMenu(GameObject target,bool x)
    {
        target.SetActive(x);
    }

    private void ClickOnBlock(GameObject temp)
    {
        //print("in raycast block");

        //check if the order is move or attack
        if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 0)
        {

            if (temp.GetComponent<GridStat>().interactable == true && temp.GetComponent<GridStat>().occupied == false)
            {
                    //player move
                    currentPlayer.GetComponent<PlayerBehavior>().PlayerMove(temp.GetComponent<GridStat>().x, temp.GetComponent<GridStat>().y);

                if (resourceGrid == true && turnCountdown > 0)
                {
                    turnCountdown -= 1;
                    Debug.Log("You have " + turnCountdown + " turns remaining!");

                    //check if turn countdown is 0
                    if (turnCountdown == 0)
                    {

                        //return to base function
                        turnStatus = 2;

                        Debug.Log("You are forced to return to base.");
                    }
                }

                runRaycast = false;
            }
            else
            {
                if (temp.GetComponent<GridStat>().resourceNode == true && temp.GetComponent<GridStat>().interactable == true)
                {

                    //player move
                    currentPlayer.GetComponent<PlayerBehavior>().PlayerMove(temp.GetComponent<GridStat>().x, temp.GetComponent<GridStat>().y);

                    CollectResources();

                    if (resourceGrid == true && turnCountdown > 0)
                    {
                        turnCountdown -= 1;
                        Debug.Log("You have " + turnCountdown + " turns remaining!");

                        //check if turn countdown is 0
                        if (turnCountdown == 0)
                        {

                            //return to base function
                            turnStatus = 2;

                            Debug.Log("You are forced to return to base.");
                        }
                    }

                    runRaycast = false;
                }

                print("cannot walk there");
                //reset to the start
                runRaycast = false;
                gridBehaviorCode.resetVisit();

            }
        }
        else if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 1)
        {

            if (temp.GetComponent<GridStat>().interactable == true && temp.GetComponent<GridStat>().occupied == false)
            {
                print("nothing to attck there");
                runRaycast = false;
                gridBehaviorCode.resetVisit();

            }
        }
        else
        {
            print("moveOrAttack error");
        }

        //function to collect resources
        void CollectResources()
        {
            int x = temp.GetComponent<GridStat>().x;
            int y = temp.GetComponent<GridStat>().y;

            // gather metal resources
            if (gridBehaviorCode.gridArray[x, y].transform.GetChild(0).name.Contains("Metal") == true)
            {
                //function to add resources & destroy resource node
                lootResources.MetalSpawn();
                Destroy(gridBehaviorCode.gridArray[x, y].transform.GetChild(0).gameObject);
                temp.GetComponent<GridStat>().resourceNode = false;
            }
            // gather synthetic resources
            if (gridBehaviorCode.gridArray[x, y].transform.GetChild(0).name.Contains("Synthetic") == true)
            {
                //function to add resources & destroy resource node
                lootResources.SyntheticSpawn();
                Destroy(gridBehaviorCode.gridArray[x, y].transform.GetChild(0).gameObject);
                temp.GetComponent<GridStat>().resourceNode = false;
            }
            // gather electronic resources
            if (gridBehaviorCode.gridArray[x, y].transform.GetChild(0).name.Contains("Electronic") == true)
            {
                //function to add resources & destroy resource node
                lootResources.ElectronicSpawn();
                Destroy(gridBehaviorCode.gridArray[x, y].transform.GetChild(0).gameObject);
                temp.GetComponent<GridStat>().resourceNode = false;
            }
        }
    }

    private void ClickOnPlayer(GameObject temp)
    {
        print("clcik on player");
        if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 0)
        {
            print("cannot walk there. Another player on it");
            //reset to the start
            runRaycast = false;
            gridBehaviorCode.resetVisit();


        }
        else if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 1)
        {
                print("cannot attack another player");
                runRaycast = false;
                gridBehaviorCode.resetVisit();
        }
        else
        {
            print("moveOrAttack error");
        }
    }

    private void ClickOnEnemy(GameObject temp)
    {
        if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 0)
        {
            print("cannot walk there. Enemy on it");
            //reset to the start
            runRaycast = false;
            gridBehaviorCode.resetVisit();

        }
        else if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 1)
        {
                //attack
                print("Attacking enemy");
                currentPlayer.GetComponent<PlayerBehavior>().AttackEnemy(temp);
                runRaycast = false;
        }
        else
        {
            print("moveOrAttack error");
        }
    }

    public void checkTurn()
    {
        int tempPlayerNum = 0;
        //int tempEnemyNum = 0;

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
                /*/check if limit turns is true
                if (resourceGrid == true && turnCountdown > 0)
                {
                    turnCountdown -= 1;
                    Debug.Log("You have " + turnCountdown + " turns remaining!");

                    //check if turn countdown is 0
                    if (turnCountdown == 0)
                    {

                        //return to base function
                        turnStatus = 1;
                        numOfPlayer = 0;

                        Debug.Log("You are forced to return to base.");
                    }
                }
                */

                turnStatus = 1;
                //all enemies are active
            }
        }
        else if (turnStatus == 1)
        {
            //enemyai start
            //do one enemy one by one
            if(countNumOfEnemy<numOfEnemy)
            foreach (GameObject e in enemies)
            {
                    e.GetComponent<EnemyBehavior>().triggerMoving = true;
                countNumOfEnemy++;
            }

          else if (countNumOfEnemy == numOfEnemy)
            {
                turnStatus = 0;
                countNumOfEnemy = 0;
                foreach (GameObject p in players)
                {
                    p.GetComponent<PlayerBehavior>().playerIsPlayable = true;
                }
            }
        }

        //neither player or enemy turn
        if (turnStatus == 2)
        {
            foreach (GameObject p in players)
            {
                p.GetComponent<PlayerBehavior>().playerIsPlayable = false;
            }
        }
    }
    
    

}
