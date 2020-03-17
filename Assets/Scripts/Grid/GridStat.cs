using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridStat : MonoBehaviour
{
    //This class is for each block of the grid map

    public int visit = -1;
    public int MoveOrAttack = 0; // 0 = move and 1 =attack
    public int x = 0;
    public int y = 0;
    public int stepLimit; //store step limit of the player that active
    
    public bool standable = true; //permanent value. determine if it is an obstacle
    public bool occupied = false; //changeable based on playable characters
    public bool pathActive = false; //showing the path that the player takes
    public bool mouseOver = false; //check if rn Mouse is over the object
    public bool interactable = false; //use this to check if the object's pos is walkable for the player
    public bool inAttackRange;
    public bool resourceNode = false; //turn on if is a resource node
    
    public GridBehavior gridBehaviorCode;
    public GameObject gc;
    public GameManager gm;


    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
        gm = gc.GetComponent<GameManager>();
        gridBehaviorCode = gc.GetComponent<GridBehavior>();

        inAttackRange = false;
        stepLimit = 0;

        this.GetComponent<Renderer>().material.SetFloat("_SecondOutlineWidth", 0.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        NewVisitColour();
        //VisitedColor();
        checkOccupied();

        if (occupied == true)
        {
            if (mouseOver == true)
            {
                SetGridColor(Color.blue);
            }
            else
            SetGridColor(Color.gray);
        }
        if (standable == false)
        {
            SetGridColor(Color.red);
        }


        if (pathActive == true)
        {
            SetGridColor(Color.yellow);
            
        }

        if (standable == true && occupied==false && visit==-1)
        {
            SetGridColor(Color.white);
        }

        if (resourceNode == true)
        {
            SetGridColor(Color.gray);
        }


    }

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

       this.GetComponent<Renderer>().material.SetFloat("_SecondOutlineWidth", 0.05f);
       // SetGridColor(Color.gray);
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        //SetGridColor(Color.white);
        this.GetComponent<Renderer>().material.SetFloat("_SecondOutlineWidth", 0.0f);
        mouseOver = false;
    }

    //associate with the color of the grid based on player's speed limit
    void VisitedColor()
    {
        if (gm.currentPlayer != null)
        {
            this.MoveOrAttack = gm.currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack;
        }

        if (visit < stepLimit && visit > 0)
        {
            if (mouseOver == true)
            {
                SetGridColor(Color.blue);
                interactable = true;
            }
            else
            {
                if (MoveOrAttack == 0)
                {
                    SetGridColor(Color.cyan);
                    interactable = true;
                }
                else
                {
                    SetGridColor(Color.yellow);
                    inAttackRange = true;
                }
            }
        }
        else if (visit == -1)
        {
            SetGridColor(Color.white);
            interactable = false;
            inAttackRange = false;
        }
    }

    public void NewVisitColour()
    {
        if (gm.currentPlayer != null)
        {
            this.MoveOrAttack = gm.currentPlayer.GetComponent<PlayerBehavior>().moveOrAttack;
        }

        if (gridBehaviorCode.tempOfInteractableBlocks.Count > 0)
        {
            if (gridBehaviorCode.tempOfInteractableBlocks.Contains(this.gameObject))
            {
                if (mouseOver == true)
                {
                    SetGridColor(Color.blue);
                    interactable = true;
                }
                else
                {
                    if (MoveOrAttack == 0)
                    {
                        SetGridColor(Color.cyan);
                        interactable = true;
                    }
                    else
                    {
                        SetGridColor(Color.yellow);
                        inAttackRange = true;
                    }
                }
            }
            else
            {
                SetGridColor(Color.white);
                interactable = false;
                inAttackRange = false;
            }
        }
        else
        {
            SetGridColor(Color.white);
            interactable = false;
            inAttackRange = false;
        }
        
    }

    //return x and y of this object
    public int[] GetXYPoints()
    {
        int[] points = new int[2];

        points[0] = x;
        points[1] = y;

        return points;
    }

    public void checkOccupied()
    {
        if (this.transform.childCount > 0)
        {
            occupied = true;
        }
        else
        {
            occupied = false;
        }
    }

    //just function to change color
    void SetGridColor(Color x)
    {
        this.GetComponent<Renderer>().material.color = x;
       
    }

    
}
