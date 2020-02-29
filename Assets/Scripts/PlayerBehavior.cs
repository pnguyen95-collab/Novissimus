using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerBehavior : MonoBehaviour
{
    
    public GridBehavior gridBehaviorCode;
    public GameManager gmCode;
    public GameObject gm;
    public GameObject parent;

    public int limitNum; //Limit of block that player can move
    public float speedOfBallMoving; //just for speed of the object movement

    public Vector3[] positions; //Positions store the Points of blocks that player will walk through

    public bool triggerMoving;
    public bool playerIsActive;
    
    
   
    void Start()
    {
        
        gm = GameObject.FindGameObjectWithTag("GameController");
        gridBehaviorCode = gm.GetComponent<GridBehavior>();
        gmCode = gm.GetComponent<GameManager>();

        
        //set limit num
        limitNum = 5;

        triggerMoving = false;
        speedOfBallMoving = 1;

        playerIsActive = false;

    }

    // Update is called once per frame
    void Update()
    {
        //finding parent's block of the player
        parent = this.transform.parent.gameObject;
        parent.GetComponent<GridStat>().occupied = true;

        //trigger player to move (must be used in Update function in order to use Lerp)
        if(triggerMoving==true)
        {
            StartCoroutine(MultipleLerp(positions, speedOfBallMoving));
        }
    }

    void OnMouseOver()
    {
        //click
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            gmCode.setOnOffMenu(true);



            int x = parent.GetComponent<GridStat>().x;
            int y = parent.GetComponent<GridStat>().y;
            
            gridBehaviorCode.FindSelectableBlock(x,y,limitNum);
            playerIsActive = true;
            gmCode.setCurrentPlayer(this.gameObject);

        }
        else if (Input.GetMouseButtonDown(1))
        {

            gmCode.setOnOffMenu(false);
            gridBehaviorCode.resetVisit();
            //act as click exit
            
           
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

        
        bool walkable = gridBehaviorCode.RunThePath(currentX, currentY, endX, endY, limitNum);


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

                gridBehaviorCode.gridArray[currentX, currentY].GetComponent<GridStat>().occupied = false;
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
        triggerMoving = false;
        gridBehaviorCode.resetVisit();
        playerIsActive = false;
        gmCode.setCurrentPlayer(null);
        yield return false;
    }







}
