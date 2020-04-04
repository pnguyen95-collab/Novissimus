﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsFunctions : MonoBehaviour
{
    public GameObject gm;
    public GameManager gmCode;
    public Text turnStatus;
    public GameObject pausePanel;
    public GameObject holdPanel;
    public GameObject holdPanel2;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController");
        gmCode = gm.GetComponent<GameManager>();

        pausePanel.SetActive(false);
        holdPanel.SetActive(false);
        holdPanel2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //pause menu up
            pausePanel.SetActive(true);
            holdPanel.SetActive(true);
            holdPanel2.SetActive(false);
        }


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
       
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().ShowMoveableBlocks();
        gmCode.runRaycast = true;
    }
    public void Attack()
    {
        gmCode.setOnOffMenu(gmCode.menuPanel2, false);
        gmCode.setOnOffMenu(gmCode.menuPanel, false);
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().ShowAttackableBlocks();
        gmCode.runRaycast = true;
    }

    public void AttackAll()
    {
        gmCode.setOnOffMenu(gmCode.menuPanel3, false);
        //gmCode.setOnOffMenu(gmCode.menuPanel, false);
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().AttackAll();
        gmCode.runRaycast = true;
    }

    public void ExitButton() //FIX
    {
        gmCode.setOnOffMenu(gmCode.menuPanel, false);
        gm.GetComponent<GridBehavior>().resetVisit();
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().playerIsActive = false;
        gmCode.setCurrentPlayer(null);

    }

    public void DoNothing()
    {
        gmCode.setOnOffMenu(gmCode.menuPanel2, false);
        gmCode.setOnOffMenu(gmCode.menuPanel, false);

        gmCode.setOnOffMenu(gmCode.menuPanel3, false);
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().DoNothing();
        gm.GetComponent<GridBehavior>().resetVisit();
    }

    public void SkipToAttack()
    {
        gmCode.setOnOffMenu(gmCode.menuPanel, false);
        gmCode.setOnOffMenu(gmCode.menuPanel2, true);
    }

    public void EnemyMove()
    {
        gmCode.countNumOfEnemy++;
    }

    public void ExitThelevel()
    {
        pausePanel.SetActive(true);
        holdPanel.SetActive(false);
        holdPanel2.SetActive(true);
    }
    
    public void YesExit()
    {

    }

    public void ResumeTheGame()
    {
        pausePanel.SetActive(false);
    }
    
}
