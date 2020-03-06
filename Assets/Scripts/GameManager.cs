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
    public GameObject menuPanel2;
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
        menuPanel2 = GameObject.Find("PlayerMenuPanel2");
        setOnOffMenu(menuPanel,false);
        setOnOffMenu(menuPanel2, false);
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
            Debug.Log("NULL");

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
        print("in raycast block");

        //check if the order is move or attack
        if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 0)
        {

            if (temp.GetComponent<GridStat>().interactable == true && temp.GetComponent<GridStat>().occupied == false)
            {
                //player move
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
