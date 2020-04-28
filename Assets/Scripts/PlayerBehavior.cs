using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerBehavior : MonoBehaviour
{
    public GridBehavior gridBehaviorCode;
    public CharacterStats playerStats;
    public CharacterStats enemyStats;
    public GameManager gmCode;
    public GameObject gm;
    public GameObject parent;
    private GameObject audio;

    public Vector3[] positions; //Positions store the Points of blocks that player will walk through

    public float speedOfBallMoving; //just for speed of the object movement
    public int moveOrAttack;
    public int thisActionPoint;

    public bool mouseOver;
    public bool triggerMoving;
    public bool playerIsActive; //to check if this player is selected
    public bool playerIsPlayable; //still can perform actions (walk,attack)
    
    void Start()
    {

        
        gm = GameObject.FindGameObjectWithTag("GameController");
        gridBehaviorCode = gm.GetComponent<GridBehavior>();
        gmCode = gm.GetComponent<GameManager>();
        playerStats = this.GetComponent<CharacterStats>();
        
        triggerMoving = false;
        playerIsActive = false;
        playerIsPlayable = true;
        mouseOver = false;
        speedOfBallMoving = 1;
        moveOrAttack = 0;

        SetOutline("_FirstOutlineWidth", 0.0f);
        thisActionPoint = playerStats.actionPoint;

        if (GameObject.FindGameObjectsWithTag("Audio") != null)
        {
            audio = GameObject.FindGameObjectWithTag("Audio");
        }
        else
        {
            print("audio manager missing");
        }
    }
    
    void Update()
    {
        CheckOutline();

        //finding parent's block of the player
        parent = this.transform.parent.gameObject;
        //parent.GetComponent<GridStat>().occupied = true;

        //trigger player to move (must be used in Update function in order to use Lerp)
        if(triggerMoving==true)
        {
            gmCode.setOnOffMenu(gmCode.blockClickingPanel, true);
            audio.GetComponent<AudioController>().PlayMoving();
            StartCoroutine(MultipleLerp(positions, speedOfBallMoving));

            triggerMoving = false;
            gridBehaviorCode.resetVisit();
            

            if (gmCode.resourceGrid == false)
            {
                
                gmCode.setOnOffMenu(gmCode.menuPanel2, true);  //FIX
            }
            else
            {
                gmCode.setCurrentPlayer(null);
            }
            gmCode.setOnOffMenu(gmCode.blockClickingPanel, false);
        }
    }

    void OnMouseOver()
    {
        mouseOver = true;
        gmCode.currentPlayerDataToShow = this.gameObject;
        SetOutline("_FirstOutlineWidth", 0.12f);

        //click
        if (Input.GetMouseButtonDown(0))
        {
            audio.GetComponent<AudioController>().PlayButtonClick();

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (gmCode.currentPlayer == null)
            {
                if (playerIsPlayable == true && playerIsActive == false)
                {
                    //print("player is playable");
                    gmCode.setOnOffMenu(gmCode.menuPanel, true);



                    int x = parent.GetComponent<GridStat>().x;
                    int y = parent.GetComponent<GridStat>().y;

                    //gridBehaviorCode.FindSelectableBlock(x, y, playerStats.limitNum);
                    if (gmCode.resourceGrid == false)
                    {
                        playerIsActive = true;

                    }
                    gmCode.setCurrentPlayer(this.gameObject);
                }
                else if (playerIsActive == true)
                {
                    print("this is your current player");
                }
                else
                {
                    if (gmCode.turnStatus == 0)
                        print("player already moved");

                    else if (gmCode.turnStatus == 2)
                        print("Time's Up!");
                    else
                        print("Enemy Turn");
                }
            }
            else {
                print("current player is" + gmCode.currentPlayer.name); //FIX put debug here
            }

            

        }
        else if (Input.GetMouseButtonDown(1)) //act as click exit
        {
            moveOrAttack = 0;
            gmCode.setOnOffMenu(gmCode.menuPanel,false);
            gridBehaviorCode.resetVisit();
            gmCode.setCurrentPlayer(null); //FIX
            
        }
    }

    
    private void OnMouseExit()
    {
        mouseOver = false;
        SetOutline("_FirstOutlineWidth", 0.0f);
    }

    public void ShowMoveableBlocks() //FIX NAME
    {
        moveOrAttack = 0;
        int x = parent.GetComponent<GridStat>().x;
        int y = parent.GetComponent<GridStat>().y;
        gridBehaviorCode.FindSelectableBlock(x, y, playerStats.moveSpeed.GetValue(),false,false);
        
    }

    public void ShowAttackableBlocks()
    {
        moveOrAttack = 1;
       
        int attackRange = gmCode.GetComponent<WeaponStats>().GiveAttackRange(playerStats.weaponNumber);
        List<int> x = gmCode.GetComponent<WeaponStats>().GiveXValue(parent.GetComponent<GridStat>().x, playerStats.weaponNumber);
        List<int> y = gmCode.GetComponent<WeaponStats>().GiveYValue(parent.GetComponent<GridStat>().y, playerStats.weaponNumber);

        for (int i = 0; i < x.Count; i++)
        {
            print(x.Count+" "+x[0]);

            if(x[i]!=-1 && y[i]!=-1)
            gridBehaviorCode.FindSelectableBlock(x[i], y[i], attackRange, true,false);
            print(attackRange);
        }

        if (playerStats.weaponNumber == 3)
        {
            gmCode.setOnOffMenu(gmCode.menuPanel3, true);
        }

        
    }


    //playerMove receive input from player
    public void PlayerMove(int endX, int endY)
    {
        gridBehaviorCode.findDistance = true;
        int currentX;
        int currentY;
        //findcurrent
        currentX = parent.GetComponent<GridStat>().x;

        currentY = parent.GetComponent<GridStat>().y;

        
        bool walkable = gridBehaviorCode.RunThePath(currentX, currentY, endX, endY, playerStats.moveSpeed.GetValue());


        //check if it over block limit
        if (walkable == true)
        {
            // In TRUE, this will store points to the 'positions[]'
            int pathCount = gridBehaviorCode.path.Count;
            positions = new Vector3[pathCount];

            for (int i = pathCount-1; i >= 0; i--)
            {
                Vector3 temp = gridBehaviorCode.path[i].transform.position;
                
                temp = new Vector3(temp.x, temp.y + 1, temp.z);

                for (int j = 0; j < pathCount; j++)
                {
                    positions[j] = temp;
                }

                this.transform.SetParent(gridBehaviorCode.path[i].transform);

                
                triggerMoving = true;
            }
        }
        else
        {
            print("over block limit");
        }
        
    }

    //function to move player
    IEnumerator MultipleLerp(Vector3[] _pos, float _speed)
    {
        for (int i = 0; i < _pos.Length; i++)
        {
            Vector3 startPos = transform.position;
            float timer = 0f;
            while (timer <= 1f)
            {
                timer += Time.deltaTime * _speed;
                Vector3 newPos = Vector3.Lerp(startPos, _pos[i], timer);
                transform.position = newPos;
                yield return new WaitForEndOfFrame();
            }
            transform.position = _pos[i];
            startPos = _pos[i];
        }

        yield return false;
    }

    //attack
    public void AttackEnemy(List<GameObject> target)
    {
        if (audio != null)
        {
            if (playerStats.weaponNumber == 1)
            {
                audio.GetComponent<AudioController>().PlayGunShot();
            }
            else if (playerStats.weaponNumber == 2)
            {
                //hammer
                audio.GetComponent<AudioController>().PlayHammer();
            }
            else if (playerStats.weaponNumber == 3)
            {
                audio.GetComponent<AudioController>().PlayBlade();
            }
            else
            {
                audio.GetComponent<AudioController>().PlaySniper();
            }
            
        }
        gameObject.GetComponent<Shake>().ShakeObject();
        gmCode.CallPopupTextOutsideGm("Attacking!");




        for (int i = 0; i < target.Count; i++)
        {
            int damageValue, visitValue;

            visitValue = target[i].transform.parent.GetComponent<GridStat>().visit;
            damageValue = gmCode.GetComponent<WeaponStats>().GiveDamageValue(playerStats.weaponNumber, visitValue);

            foreach (int d in playerStats.boosterNumber)
            {
                if (d == 1)
                {
                    damageValue++;
                }
            }

           


            if (target[i].GetComponent<CharacterStats>() != null)
            {
                bool resetEnemies = false;
                //has to check if the target will be destoy or no
                if (target[i].GetComponent<CharacterStats>().currentHealth - damageValue <= 0)
                {
                    resetEnemies = true;
                    gmCode.enemiesList.Remove(target[i]);
                    gmCode.CallPopupTextOutsideGm("You killed " + target[i].name);
                    
                }
                gmCode.AddMessage("Attacked " + target[i].name + " for " + damageValue + " damage.", Color.white);

                target[i].GetComponent<CharacterStats>().TakeDamage(damageValue);

                if (resetEnemies == true)
                {

                    gmCode.ReCountEnemies();
                }
            }


            EndCurrentPlayerAction();
            
        }

    }

    public void AttackAll()
    {
        print("doing attack all");
        List<GameObject> enemyTemp = new List<GameObject>();
        foreach (GameObject obj in gridBehaviorCode.tempOfInteractableBlocks)
        {
            if (obj.transform.childCount > 0)
            {
                // check if has enemy in attackable blocks
                if (obj.transform.GetChild(0).tag == "Enemy")
                {
                    enemyTemp.Add(obj.transform.GetChild(0).gameObject);
                }
            }
        }

        //start attack if has in enemyTemp
        if(enemyTemp.Count>0)
        AttackEnemy(enemyTemp);
        else
        {
            gmCode.CallPopupTextOutsideGm("No enemy in range. Skipped Attack");
           
            DoNothing();
            gridBehaviorCode.resetVisit();
        }

        

        
    }

    //player choose to do nothing for the selected vehicle 
    public void DoNothing()
    {
        CheckIfAbleToSelfHeal();
        EndCurrentPlayerAction();
    }
    

    public void CheckOutline()
    {
        if (playerIsPlayable == true)
        {

            SetOutline("_SecondOutlineWidth", 0.05f);
        }
        else
        {
            SetOutline("_SecondOutlineWidth", 0.0f);
        }
        
    }

    public void SetOutline(string o,float a)
    {
        this.GetComponent<Renderer>().material.SetFloat(o, a);
    }

    public void CheckIfAbleToSelfHeal()
    {
        

        foreach (int d in playerStats.boosterNumber)
        {
            if (d == 2)
            {
                if (playerStats.currentHealth < playerStats.maxHealth.GetValue())
                {
                    playerStats.currentHealth++;
                    gmCode.CallPopupTextOutsideGm(this.name+" is self-healing");
                    gmCode.AddMessage(this.name+"'s health + 1", Color.white);

                }
            }
        }
    }


    public void EndCurrentPlayerAction()
    {
        thisActionPoint--;
        CheckIfAbleToSelfHeal();

        if (thisActionPoint >= 1)
        {
            this.playerIsPlayable = true;
        }
        else
        {
            this.playerIsPlayable = false;
        }
       
        this.playerIsActive = false;
        gmCode.setCurrentPlayer(null);
        gridBehaviorCode.resetVisit();
    }

}
