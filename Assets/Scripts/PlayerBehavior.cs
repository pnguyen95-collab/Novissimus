using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    GameObject gm;
    GridBehavior gridBehaviorCode;
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController");
       gridBehaviorCode = gm.GetComponent<GridBehavior>();
      
    }

    // Update is called once per frame
    void Update()
    {
        parent = this.transform.parent.gameObject;
        parent.GetComponent<GridStat>().status = 2;
    }

    
    //playerMove receive input from player
    public void  PlayerMove(int endX,int endY)
    {
        gridBehaviorCode.findDistance = true;
        Debug.Log("doing something");
        int currentX;
        int currentY;
        //findcurrent
        currentX = parent.GetComponent<GridStat>().x;

        currentY = parent.GetComponent<GridStat>().y;

        gridBehaviorCode.RunThePath(currentX, currentY, endX, endY);
        
    }
}
