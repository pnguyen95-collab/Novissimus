using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    public GameManager gmCode;
    public Text playerName;
    public Text playerHealth;
    public Text playerSpeed;
    public Text playerWeapon;
    public GameObject currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        gmCode = this.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        

        if (currentPlayer != null)
        {
            currentPlayer = gmCode.currentPlayer;

            if (currentPlayer.GetComponent<PlayerBehavior>().playerIsActive == true)
            {
                playerName.text = "Car";
                playerHealth.text = "Health: " + currentPlayer.GetComponent<CharacterStats>().currentHealth + "/ " + currentPlayer.GetComponent<CharacterStats>().maxHealth.GetValue();
                playerWeapon.text = "Weapon: Machine Gun";
                playerSpeed.text = "Speed: " + currentPlayer.GetComponent<CharacterStats>().moveSpeed.GetValue();
            }
        }
    }
}
