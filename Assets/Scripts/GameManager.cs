using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//message class
public class Message
{
    public string text;
    public Text textObject;
}

public class GameManager : MonoBehaviour
{

    public GameObject[] players;
    public List<GameObject> playersList;
    public GameObject[] enemies;
    public List<GameObject> enemiesList;
    public GameObject currentPlayer;
    public GameObject menuPanel;
    public GameObject menuPanel2;
    public GameObject menuPanel3;
    public GameObject blockClickingPanel;
    public GridBehavior gridBehaviorCode;
    public NodeLoot lootResources;
    public GameObject currentEnemy;
    public Button test;
    
    public GameObject textDisplay;
    public GameObject textPanel;
    List<Message> messageList = new List<Message>();

    //maximum number of messages to display
    int maxMessages = 15;

    public int turnStatus; // 0 = player , 1 = enemy, 2 = none
    public int numOfPlayer;
    public int countNumOfPlayer;
    public int numOfEnemy;
    public int countNumOfEnemy;
    public int turnCountdown;

    public bool runRaycast; //boolean to trigger whether or not it's a resource grid
    public bool resourceGrid = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gridBehaviorCode = this.GetComponent<GridBehavior>();

        players = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numOfEnemy = enemies.Length;
        numOfPlayer = players.Length;
        countNumOfEnemy = 0;
        countNumOfPlayer = 0;

        for (int i = 0; i < countNumOfPlayer; i++)
        {
            playersList.Add(players[i]);
        }

        for (int i = 0; i < countNumOfEnemy; i++)
        {
            enemiesList.Add(enemies[i]);
        }

        menuPanel = GameObject.Find("PlayerMenuPanel");
        
        if (resourceGrid == false)
        {
            menuPanel2 = GameObject.Find("PlayerMenuPanel2");
        }

        setOnOffMenu(menuPanel, false);
        setOnOffMenu(menuPanel2, false);
        setOnOffMenu(blockClickingPanel, false);

        if (resourceGrid == false)
        {
            setOnOffMenu(menuPanel3, false);
        }

        turnStatus = 0;
        runRaycast = false;
        
        //set how many turns countdown to countdown until return to base
        if (resourceGrid == true)
        {
            turnCountdown = 10;
            AddMessage("Resource harvesting start!", Color.cyan);
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

                        AddMessage("You are forced to return to base", Color.white);
                    }

                    setCurrentPlayer(null);
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
                        
                        AddMessage("You have " + turnCountdown + " turns remaining!", Color.white);
                        //check if turn countdown is 0
                        if (turnCountdown == 0)
                        {

                            //return to base function
                            turnStatus = 2;

                            AddMessage("You are forced to return to base", Color.white);
                        }

                        setCurrentPlayer(null);
                    }

                    runRaycast = false;  //FIX
                }
                else
                {
                    AddMessage("Out of range. Please choose in-range Block",Color.red);

                }
                

            }
        }
        else if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 1)
        {

            if (temp.GetComponent<GridStat>().interactable == true && temp.GetComponent<GridStat>().occupied == false)
            {
                AddMessage("Nothing to attack there", Color.red);
                runRaycast = false;
                gridBehaviorCode.resetVisit();
                if (resourceGrid == false)
                {
                    setOnOffMenu(menuPanel2, true);
                }
                

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
                AddMessage("Harvested metal resources", Color.cyan);
            }
            // gather synthetic resources
            if (gridBehaviorCode.gridArray[x, y].transform.GetChild(0).name.Contains("Synthetic") == true)
            {
                //function to add resources & destroy resource node
                lootResources.SyntheticSpawn();
                Destroy(gridBehaviorCode.gridArray[x, y].transform.GetChild(0).gameObject);
                temp.GetComponent<GridStat>().resourceNode = false;
                AddMessage("Harvested synthetic polymer resources", Color.cyan);
            }
            // gather electronic resources
            if (gridBehaviorCode.gridArray[x, y].transform.GetChild(0).name.Contains("Electronic") == true)
            {
                //function to add resources & destroy resource node
                lootResources.ElectronicSpawn();
                Destroy(gridBehaviorCode.gridArray[x, y].transform.GetChild(0).gameObject);
                temp.GetComponent<GridStat>().resourceNode = false;
                AddMessage("Harvested electronic resources", Color.cyan);
            }
        }
    }

    private void ClickOnPlayer(GameObject temp)
    {
        //check first if the player's block is interactable
        if (temp.transform.parent.GetComponent<GridStat>().interactable == false)
        {
            AddMessage("Not in range. Please choose in-range Block",Color.red);
        }
        else
        {

            if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 0)
            {
                AddMessage("You cannot move there. Another Player is on it", Color.red);
                //reset to the start
                runRaycast = false;
                gridBehaviorCode.resetVisit();
                setOnOffMenu(menuPanel, true);


            }
            else if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 1)
            {
                AddMessage("You cannot attack another player", Color.red);
                runRaycast = false;
                gridBehaviorCode.resetVisit();
                setOnOffMenu(menuPanel2, true);
            }
            else
            {
                print("moveOrAttack error");
            }
        }
    }

    private void ClickOnEnemy(GameObject temp)
    {
        //check first if the player's block is interactable
        if (temp.transform.parent.GetComponent<GridStat>().interactable == false)
        {
            AddMessage("Not in range. Please choose in-range Block", Color.red);
        }
        else
        {
            if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 0)
            {

                AddMessage("You cannot move there. An enemy is on it", Color.red);
                //reset to the start
                runRaycast = false;
                gridBehaviorCode.resetVisit();
                setOnOffMenu(menuPanel, true);


            }
            else if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 1)
            {
                //attack
                GridStat parent;
                if (temp.transform.parent != null)
                {
                    parent = temp.transform.parent.gameObject.GetComponent<GridStat>();
                    if (parent.inAttackRange == true)
                    {

                        AddMessage("Attacked Prototype for " + currentPlayer.GetComponent<CharacterStats>().damage.GetValue() + " damage.", Color.white);
                        currentPlayer.GetComponent<PlayerBehavior>().AttackEnemy(temp);
                        runRaycast = false;
                    }
                    else
                    {


                        AddMessage("Enemy not in range", Color.red);
                        runRaycast = false;
                        gridBehaviorCode.resetVisit();
                        setOnOffMenu(menuPanel2, true);
                    }

                }

            }
            else
            {
                print("moveOrAttack error");
            }
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
                //enemies are active
                turnStatus = 1;

                //check all active enemies/players
                players = GameObject.FindGameObjectsWithTag("Player");
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
            }
        }
        else if (turnStatus == 1)
        {
            numOfEnemy = enemies.Length;
            //enemyai start
            //do one enemy one by one
            if (countNumOfEnemy < numOfEnemy)
            {
                //button appear
                if (countNumOfEnemy == 0)
                {
                    TestEnemy();
                }
                setOnOffMenu(menuPanel3, true);
            }
          /*  
            if (countNumOfEnemy < numOfEnemy)
                foreach (GameObject e in enemies)
            {
                e.GetComponent<EnemyBehavior>().triggerMoving = true;
                countNumOfEnemy++;
                gridBehaviorCode.resetVisit();
            }*/
        
        else if (countNumOfEnemy == numOfEnemy)
        {
            setOnOffMenu(menuPanel3, false);
            turnStatus = 0;
            countNumOfEnemy = 0;

            //check all active enemies/players
            players = GameObject.FindGameObjectsWithTag("Player");
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

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

    //add a new message to the display log
    public void AddMessage(string x, Color y)
    {
        //check to see if message list is at capacity and if so destroy the earliest entry
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message displayMessage = new Message();

        //set the message to display
        displayMessage.text = x;

        //instantiate text prefab
        GameObject newText = Instantiate(textDisplay, textPanel.transform);

        displayMessage.textObject = newText.GetComponent<Text>();

        displayMessage.textObject.text = displayMessage.text;
        displayMessage.textObject.color = y;
        GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;

        //adds display text to the message list
        messageList.Add(displayMessage);
    }

    public void TestEnemy()
    {
        
        enemies[countNumOfEnemy].GetComponent<EnemyBehavior>().triggerMoving = true;
        countNumOfEnemy++;
        gridBehaviorCode.resetVisit();
       
    }

    public void ReCountEnemies()
    {
        numOfEnemy = enemies.Length;
        countNumOfEnemy = 0;
    }



}
