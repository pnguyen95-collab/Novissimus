using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsFunctions : MonoBehaviour
{
    public GameObject gm;
    public GameManager gmCode;
    public Text turnStatus;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController");
        gmCode = gm.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if combat/resource grid
        if (gmCode.resourceGrid == false)
        {
            if (gmCode.turnStatus == 0)
            {
                turnStatus.text = "Turn: Player";
                turnStatus.color = Color.blue;
            }

            if (gmCode.turnStatus == 1)
            {
                turnStatus.text = "Turn: Enemy";
                turnStatus.color = Color.red;
            }
        }
        if (gmCode.resourceGrid == true)
        {
            if (gmCode.turnStatus == 0)
            {
                turnStatus.text = "Turns Remaining: " + gmCode.turnCountdown;
                turnStatus.color = Color.blue;
            }
            if (gmCode.turnStatus == 2)
            {
                turnStatus.text = "Time's Up";
                turnStatus.color = Color.green;
            }
        }

    }

    public void MoveButton()
    {
        gmCode.setOnOffMenu(gmCode.menuPanel,false);
       
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().ShowMoveableBlcoks();
        gmCode.runRaycast = true;
    }
    public void Attack()
    {
        gmCode.setOnOffMenu(gmCode.menuPanel2, false);
        gmCode.setOnOffMenu(gmCode.menuPanel, false);
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().ShowAttackableBlcoks();
        gmCode.runRaycast = true;
    }

    public void ExitButton()
    {
        gmCode.setOnOffMenu(gmCode.menuPanel, false);
        gm.GetComponent<GridBehavior>().resetVisit();
    }

    public void DoNothing()
    {
        gmCode.setOnOffMenu(gmCode.menuPanel2, false);
        gmCode.setOnOffMenu(gmCode.menuPanel, false);
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().DoNothing();
        gm.GetComponent<GridBehavior>().resetVisit();
    }

    public void EnemyMove()
    {
        gmCode.countNumOfEnemy++;
    }


    
}
