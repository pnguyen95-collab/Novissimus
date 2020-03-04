﻿using System.Collections;
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
        if (gmCode.turnStatus == 0)
        {
            turnStatus.text = "Turn: Player";
            turnStatus.color = Color.blue;
        }
        else if (gmCode.turnStatus == 1)
        {
            turnStatus.text = "Turn: Enemy";
            turnStatus.color = Color.red;
        }
        else
        {
            print("turn status text error");
            turnStatus.color = Color.green;
        }
    }

    public void MoveButton()
    {
        gmCode.setOnOffMenu(false);
        gmCode.runRaycast = true;
    }

    public void ExitButton()
    {
        gmCode.setOnOffMenu(false);
        gm.GetComponent<GridBehavior>().resetVisit();
    }

    public void EnemyMove()
    {
        gmCode.countNumOfEnemy++;
    }
}
