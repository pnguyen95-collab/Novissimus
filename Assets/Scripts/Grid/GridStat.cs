using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridStat : MonoBehaviour
{
    //This class is for each block of the grid map

    public int visit = -1;
    public int x = 0;
    public int y = 0;
    public int stepLimit; //store step limit of the player that active
    
    public bool standable = true; //permanent value. determine if it is an obstacle
    public bool occupied = false; //changeable based on playable characters
    public bool pathActive = false; //showing the path that the player takes
    public bool mouseOver = false; //check if rn Mouse is over the object
    public bool walkAble = false; //use this to check if the object's pos is walkable for the player

    
    public GridBehavior gridBehaviorCode;
    public GameObject gc;


    void Start()
    {
        stepLimit = 0;
        gc = GameObject.FindGameObjectWithTag("GameController");
        gridBehaviorCode = gc.GetComponent<GridBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

        VisitedColor();

        if (occupied == true)
        {
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


    }

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        SetGridColor(Color.gray);
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        SetGridColor(Color.white);
        mouseOver = false;
    }

    //associate with the color of the grid based on player's speed limit
    void VisitedColor()
    {
        if (visit < stepLimit && visit > 0)
        {
            if (mouseOver == true)
            {
                SetGridColor(Color.blue);
                walkAble = true;
            }
            else
            {
                SetGridColor(Color.cyan);
            }
        }
        else if (visit == -1)
        {
            SetGridColor(Color.white);
            walkAble = false;
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

    //player click the object
    private void OnMouseDown()
    {
        if (walkAble == true)
        {

           
        }
    }

    //just function to change color
    void SetGridColor(Color x)
    {
        this.GetComponent<Renderer>().material.color = x;
    }

    
}
