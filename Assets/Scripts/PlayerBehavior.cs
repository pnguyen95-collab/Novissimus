using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerBehavior : MonoBehaviour
{
    
    public GridBehavior gridBehaviorCode;
    public CharacterStats playerStats;
    public GameManager gmCode;
    public GameObject gm;
    public GameObject parent;
    public GameObject particleChildForSelected;
    public GameObject particleChildForPlayable;

    public float speedOfBallMoving; //just for speed of the object movement
    public int moveOrAttack;

    public Vector3[] positions; //Positions store the Points of blocks that player will walk through

    public bool triggerMoving;
    public bool playerIsActive; //to check if this player is selected
    public bool playerIsPlayable; //still can perform actions (walk,attack)
    
    
   
    void Start()
    {
        if (this.transform.childCount > 0)
        {
            particleChildForSelected = this.transform.GetChild(0).gameObject;
            particleChildForPlayable = this.transform.GetChild(1).gameObject;

        }
        gm = GameObject.FindGameObjectWithTag("GameController");
        gridBehaviorCode = gm.GetComponent<GridBehavior>();
        gmCode = gm.GetComponent<GameManager>();
        playerStats = this.GetComponent<CharacterStats>();
        
       

        triggerMoving = false;
        speedOfBallMoving = 1;
        moveOrAttack = 0;

        playerIsActive = false;
        playerIsPlayable = true;

    }

    // Update is called once per frame
    void Update()
    {
        CheckParticle();

        //finding parent's block of the player
        parent = this.transform.parent.gameObject;
        //parent.GetComponent<GridStat>().occupied = true;

        //trigger player to move (must be used in Update function in order to use Lerp)
        if(triggerMoving==true)
        {
            StartCoroutine(MultipleLerp(positions, speedOfBallMoving));

            triggerMoving = false;
            gridBehaviorCode.resetVisit();

            gmCode.setOnOffMenu(gmCode.menuPanel2, true);

            //playerIsActive = false;
            //playerIsPlayable = false;
            //gmCode.countNumOfPlayer++;
            //gmCode.setCurrentPlayer(null);
        }
    }

    void OnMouseOver()
    {
        //click
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (playerIsPlayable == true)
            {
                gmCode.setOnOffMenu(gmCode.menuPanel,true);



                int x = parent.GetComponent<GridStat>().x;
                int y = parent.GetComponent<GridStat>().y;

                //gridBehaviorCode.FindSelectableBlock(x, y, playerStats.limitNum);
                playerIsActive = true;
                gmCode.setCurrentPlayer(this.gameObject);
            }
            else 
            {
                if (gmCode.turnStatus == 0)
                    print("player already moved");
                else
                    print("Enemy turn");
            }
            

            

        }
        else if (Input.GetMouseButtonDown(1)) //act as click exit
        {
            moveOrAttack = 0;
            gmCode.setOnOffMenu(gmCode.menuPanel,false);
            gridBehaviorCode.resetVisit();
            
        }
    }

    public void ShowMoveableBlcoks()
    {
        moveOrAttack = 0;
        int x = parent.GetComponent<GridStat>().x;
        int y = parent.GetComponent<GridStat>().y;
        gridBehaviorCode.FindSelectableBlock(x, y, playerStats.moveSpeed.GetValue());
        
    }

    public void ShowAttackableBlcoks()
    {
        moveOrAttack = 1;
        int x = parent.GetComponent<GridStat>().x;
        int y = parent.GetComponent<GridStat>().y;
        gridBehaviorCode.FindSelectableBlock(x, y, playerStats.attackRange.GetValue());
        
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

    public void AttackEnemy(GameObject target)
    {
        if (target.GetComponent<EnemyStats>() != null)
        {
            target.GetComponent<EnemyStats>().health = target.GetComponent<EnemyStats>().health - playerStats.damage.GetValue();
            this.playerIsPlayable = false;
            this.playerIsActive = false;
            gmCode.setCurrentPlayer(null);
            gridBehaviorCode.resetVisit();
        }

    }

    public void DoNothing()
    {
        playerIsActive = false;
        playerIsPlayable = false;
        gmCode.setCurrentPlayer(null);
    }

    public void CheckParticle()
    {
        if (playerIsActive == true)
        {
            this.particleChildForSelected.SetActive(true);
        }
        else
        {
            this.particleChildForSelected.SetActive(false);
        }

        if (playerIsPlayable==true)
        {
            this.particleChildForPlayable.SetActive(true);
        }
        else
        {
            this.particleChildForPlayable.SetActive(false);
        }

    }







}
