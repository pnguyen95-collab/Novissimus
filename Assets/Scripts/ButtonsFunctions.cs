using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsFunctions : MonoBehaviour
{
    public GameObject gm;
    public GameManager gmCode;
    public Text turnStatus;
    public GameObject turnStatusImage;
    public GameObject pausePanel;
    public GameObject holdPanel;
    public GameObject holdPanel2;
    public GameObject audioManager;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Audio") != null)
        {
            audioManager= GameObject.FindGameObjectWithTag("Audio");
           

        }
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
                Color color = new Color32(188, 230, 250, 255);
                turnStatus.color = color;

                //set color by RGBA values
                color = new Color32(0, 25, 255, 255);
                turnStatusImage.GetComponent<Image>().color = color;
            }

            if (gmCode.turnStatus == 1)
            {
                turnStatus.text = "Turn: Enemy";
                Color color = new Color32(255, 198, 198, 255);
                turnStatus.color = color;
                
                //set color by RGBA values
                color = new Color32(255, 0, 35, 255);
                turnStatusImage.GetComponent<Image>().color = color;
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
        audioManager.GetComponent<AudioController>().PlayButtonClick();
        gmCode.setOnOffMenu(gmCode.menuPanel,false);
       
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().ShowMoveableBlocks();
        gmCode.runRaycast = true;
    }
    public void Attack()
    {
        audioManager.GetComponent<AudioController>().PlayButtonClick();
        gmCode.setOnOffMenu(gmCode.menuPanel2, false);
        gmCode.setOnOffMenu(gmCode.menuPanel, false);
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().ShowAttackableBlocks();
        gmCode.runRaycast = true;
    }

    public void AttackAll()
    {
        audioManager.GetComponent<AudioController>().PlayButtonClick();
        gmCode.setOnOffMenu(gmCode.menuPanel3, false);
        //gmCode.setOnOffMenu(gmCode.menuPanel, false);
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().AttackAll();
        gmCode.runRaycast = true;
    }

    public void ExitButton() //FIX
    {
        audioManager.GetComponent<AudioController>().PlayButtonClick();
        gmCode.setOnOffMenu(gmCode.menuPanel, false);
        gm.GetComponent<GridBehavior>().resetVisit();
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().playerIsActive = false;
        gmCode.setCurrentPlayer(null);

    }

    public void DoNothing()
    {
        audioManager.GetComponent<AudioController>().PlayButtonClick();
        gmCode.setOnOffMenu(gmCode.menuPanel2, false);
        gmCode.setOnOffMenu(gmCode.menuPanel, false);

        gmCode.setOnOffMenu(gmCode.menuPanel3, false);
        gmCode.currentPlayer.GetComponent<PlayerBehavior>().DoNothing();
        
    }

    public void SkipToAttack()
    {
        audioManager.GetComponent<AudioController>().PlayButtonClick();
        gmCode.setOnOffMenu(gmCode.menuPanel, false);
        gmCode.setOnOffMenu(gmCode.menuPanel2, true);
    }

    public void EnemyMove()
    {
        gmCode.countNumOfEnemy++;
    }

    public void ExitThelevel()
    {
        audioManager.GetComponent<AudioController>().PlayButtonClick();
        pausePanel.SetActive(true);
        holdPanel.SetActive(false);
        holdPanel2.SetActive(true);
    }
    
    public void ResumeTheGame()
    {
        audioManager.GetComponent<AudioController>().PlayButtonClick();
        pausePanel.SetActive(false);
    }

    
    
}
