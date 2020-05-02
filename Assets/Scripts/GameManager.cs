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
    public GameObject currentPlayerDataToShow;
    public GameObject menuPanel;
    public GameObject menuPanel2;
    public GameObject menuPanel3;
    public GameObject blockClickingPanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject gameOverPanel;
    public GridBehavior gridBehaviorCode;
    public NodeLoot lootResources;
    public GameObject currentEnemy;
    public Button test;
    public GameObject alertText;
    public Text enemyLeftText;
    
    public GameObject textDisplay;
    public GameObject textPanel;
    List<Message> messageList = new List<Message>();

    //maximum number of messages to display
    int maxMessages = 15;

    public GameObject playerDataObject;
    public Inventory inventory;

    public int turnStatus; // 0 = player , 1 = enemy, 2 = none
    public int numOfPlayer;
    public int countNumOfPlayer;
    public int numOfEnemy;
    public int countNumOfEnemy;
    public int turnCountdown;

    public bool runRaycast; //boolean to trigger whether or not it's a resource grid
    public bool resourceGrid = false;
    private bool runEnemy;

    private void Awake()
    {
        //INVENTORY
        if (GameObject.Find("PlayerInventory") != null)
        {
            playerDataObject = GameObject.Find("PlayerInventory");

            inventory = playerDataObject.GetComponent<PlayerData>().inventory;
        }
        else
        {
            print("Missing Inventory object");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        

        currentPlayerDataToShow = null;
        gridBehaviorCode = this.GetComponent<GridBehavior>();

        players = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numOfEnemy = enemies.Length;
        numOfPlayer = players.Length;
        countNumOfEnemy = 0;
        countNumOfPlayer = 0;
        

        for (int i = 0; i < numOfPlayer; i++)
        {
            playersList.Add(players[i]);
        }

        for (int i = 0; i < numOfEnemy; i++)
        {
            enemiesList.Add(enemies[i]);
        }

        
        
        //PANELs

        menuPanel = GameObject.Find("PlayerMenuPanel");
        
        if (resourceGrid == false)
        {
            menuPanel2 = GameObject.Find("PlayerMenuPanel2");
            menuPanel3 = GameObject.Find("PlayerMenuPanel3");
        }

        setOnOffMenu(menuPanel, false);
        setOnOffMenu(menuPanel2, false);
        setOnOffMenu(menuPanel3, false);
        setOnOffMenu(blockClickingPanel, false);
        alertText.SetActive(false);
        
        if (resourceGrid == false)
        {
            setOnOffMenu(menuPanel3, false);
            
            setOnOffMenu(winPanel, false);
            setOnOffMenu(losePanel, false);
            
        }

        turnStatus = 0;
        runRaycast = false;
        runEnemy = true;

       
        
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
        if (resourceGrid == false)
        {
            checkWinLose();enemyLeftText.text = numOfEnemy+ " Enemy left";
        }
        

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
                    StartCoroutine(PopupText("You have " + turnCountdown + " turns remaining!"));

                    //check if turn countdown is 0
                    if (turnCountdown == 0)
                    {

                        //return to base function
                        turnStatus = 2;

                        
                        StartCoroutine(PopupText("You are forced to return to base"));
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
                        
                        
                        StartCoroutine(PopupText("You have " + turnCountdown + " turns remaining!"));

                        //check if turn countdown is 0
                        if (turnCountdown == 0)
                        {

                            //return to base function
                            turnStatus = 2;

                            
                            StartCoroutine(PopupText("You are forced to return to base"));
                        }

                        setCurrentPlayer(null);
                    }

                    runRaycast = false;  //FIX
                }
                else
                {
                    
                    StartCoroutine(PopupText("Please choose in-range Block"));

                }
                

            }
        }
        else if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 1)
        {

            if (temp.GetComponent<GridStat>().interactable == true && temp.GetComponent<GridStat>().occupied == false)
            {
                
                StartCoroutine(PopupText("Nothing to attack there"));
                runRaycast = false;
                gridBehaviorCode.resetVisit();
                if (resourceGrid == false)
                {
                    setOnOffMenu(menuPanel2, true);
                }
                

            }
            else
            {
                StartCoroutine(PopupText("Cannot attack there"));
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
        //check first if the player's block is interactable
        if (temp.transform.parent.GetComponent<GridStat>().interactable == false)
        {
            StartCoroutine(PopupText("Please choose in-range Block"));
        }
        else
        {

            if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 0)
            {
                StartCoroutine(PopupText("You cannot move there."));
                //reset to the start
                runRaycast = false;
                gridBehaviorCode.resetVisit();
                setOnOffMenu(menuPanel, true);


            }
            else if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 1)
            {
                StartCoroutine(PopupText("You cannot attack another player"));
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
        if (temp.transform.parent.GetComponent<GridStat>().interactable == false|| temp.transform.parent.GetComponent<GridStat>().inAttackRange == false)
        {
            StartCoroutine(PopupText("Please choose in-range Block"));
        }
        else
        {
            if (currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack == 0)
            {
                
                StartCoroutine(PopupText("You cannot move there."));
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
                        List<GameObject> d = new List<GameObject>();
                        d.Add(temp);
                        currentPlayer.GetComponent<PlayerBehavior>().AttackEnemy(d);
                        runRaycast = false;
                    }
                    else
                    {



                        StartCoroutine(PopupText("Enemy not in range"));
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
            foreach (GameObject p in playersList)
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
                StartCoroutine(DoingEnemyTurn());
                
            }
           else if (countNumOfEnemy == numOfEnemy)
            {
               
                turnStatus = 0;
                countNumOfEnemy = 0;
                runEnemy = true;

                //check all active enemies/players
                players = GameObject.FindGameObjectsWithTag("Player");
                enemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject p in playersList)
                {
                    p.GetComponent<PlayerBehavior>().playerIsPlayable = true;
                    p.GetComponent<PlayerBehavior>().thisActionPoint = p.GetComponent<PlayerBehavior>().playerStats.actionPoint;
                }
                
            }
        }

        //neither player or enemy turn
        if (turnStatus == 2)
        {
            foreach (GameObject p in playersList)
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

        //adds display text to the message list
        messageList.Add(displayMessage);
        GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    public void TestEnemy()
    {
        
        enemies[countNumOfEnemy].GetComponent<EnemyBehavior>().triggerEnemyToFunction = true;
        countNumOfEnemy++;
        gridBehaviorCode.resetVisit();
       
    }
    public void checkWinLose()
    {
        if (enemiesList.Count == 0)
        {
            setOnOffMenu(winPanel, true);
            WinReward();
        }
        if (playersList.Count == 0)
        {
            bool gameOVer = LoseReward();
            if (gameOVer == false)
            {
                setOnOffMenu(losePanel, true);
            }
            else
            {
                setOnOffMenu(gameOverPanel, true);
            }
            
        }
    }
    public void ReCountEnemies()
    {
        numOfEnemy = enemies.Length;
        countNumOfEnemy = 0;

        
    }
    public void ReCountPlayers()
    {
        numOfPlayer = players.Length;
        countNumOfPlayer = 0;
      
    }

    public IEnumerator PopupText(string x)
    {
        print("doing pop up text");
        alertText.SetActive(true);
        alertText.GetComponent<Text>().text = x;

        alertText.GetComponent<Text>().canvasRenderer.SetAlpha(1);
        alertText.GetComponent<Text>().CrossFadeAlpha(0.0f, 2.5f, false);
        yield return new WaitForSeconds(4f);
        alertText.SetActive(false);
        alertText.GetComponent<Text>().canvasRenderer.SetAlpha(1);
    }

    public void CallPopupTextOutsideGm(string x)
    {
        StartCoroutine(PopupText(x));
    }

    public IEnumerator DoingEnemyTurn()
    {
        if (runEnemy == true)
        {
            runEnemy = false;
            print("enemy coubt" + enemiesList.Count);

            for (int i = 0; i < enemiesList.Count; i++)
            {
                print("dddd");
                yield return new WaitForSeconds(1.2f);
                TestEnemy();
            }

        }
    }

    public void WinReward()
    {
        int temp = playerDataObject.GetComponent<PlayerData>().levelNum;

        switch (temp)
        {
            case 1:

                
                inventory.AddItem(new Item { type = Item.Type.Scrap, amount = 96 });
                inventory.AddItem(new Item { type = Item.Type.Fuel, amount = 18 });
                inventory.AddItem(new Item { type = Item.Type.Iron, amount = 3 });

               
                break;
            case 2:

                inventory.AddItem(new Item { type = Item.Type.Scrap, amount = 112 });
                inventory.AddItem(new Item { type = Item.Type.Fuel, amount = 20 });
                inventory.AddItem(new Item { type = Item.Type.Iron, amount = 3 });
                inventory.AddItem(new Item { type = Item.Type.Rubber, amount = 1 });
                
                break;
            case 3:
                inventory.AddItem(new Item { type = Item.Type.Scrap, amount = 32+56 });
                inventory.AddItem(new Item { type = Item.Type.Fuel, amount = 6+9 });
                inventory.AddItem(new Item { type = Item.Type.Iron, amount = 1 });
                inventory.AddItem(new Item { type = Item.Type.Rubber, amount = 1 });
                inventory.AddItem(new Item { type = Item.Type.Plastic, amount = 1 });
                
                break;
            case 4:

                inventory.AddItem(new Item { type = Item.Type.Scrap, amount = 56 + 48 });
                inventory.AddItem(new Item { type = Item.Type.Fuel, amount = 9 + 8 });
                inventory.AddItem(new Item { type = Item.Type.Iron, amount = 1 });
                inventory.AddItem(new Item { type = Item.Type.Rubber, amount =  2});
                inventory.AddItem(new Item { type = Item.Type.Plastic, amount = 1 });
                
                break;
            case 5:

                inventory.AddItem(new Item { type = Item.Type.Scrap, amount = 82+82+48+48});
                inventory.AddItem(new Item { type = Item.Type.Fuel, amount =11 + 11+8+8 });
                inventory.AddItem(new Item { type = Item.Type.Iron, amount = 2 });
                inventory.AddItem(new Item { type = Item.Type.Rubber, amount = 2 });
                inventory.AddItem(new Item { type = Item.Type.Steel, amount = 1 });
               
                break;

        }

    }
    public bool LoseReward()
    {
        bool gameOver=false;

        int temp = playerDataObject.GetComponent<PlayerData>().levelNum;
        
        int num = inventory.CheckItem(new Item { type = Item.Type.Scrap, amount = 1 });
        if (num > 0)
        {
            num = Mathf.RoundToInt(num/2);
            inventory.RemoveItem(new Item { type = Item.Type.Fuel, amount = num });

            int num2 = inventory.CheckItem(new Item { type = Item.Type.Scrap, amount = 1 });


            if (num2 < 50 && num2 > 0)
            {
                inventory.RemoveItem(new Item { type = Item.Type.Fuel, amount = num2 });
                return gameOver = true;
                //game over back to start scene (reset playerdata)
            }
            else if (num2==0)
            {
                return gameOver = true;
                //gameover back to start scene (reset playerdata)
            }
            else
            {
                inventory.RemoveItem(new Item { type = Item.Type.Fuel, amount = 50 });
            }
        }
        //lose fuel half +50 if they have
        //lose all
        return gameOver;
    }
}
