using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    public GameObject gm;
    public GameManager gmCode;
    public Text playerName;
    public Text playerHealth;
    public Text playerSpeed;
    public Text playerWeapon;

    // Start is called before the first frame update
    void Start()
    {

        gmCode = gm.GetComponent<GameManager>();

    
    }

    // Update is called once per frame
    void Update()
    {
        if (gmCode.currentPlayer != null)
        {

            if (gmCode.currentPlayer.GetComponent<PlayerBehavior>().playerIsActive == true)
            {
                playerName.text = "Car";
                playerHealth.text = "Health: " + gmCode.currentPlayer.GetComponent<CharacterStats>().currentHealth + "/ " + gmCode.currentPlayer.GetComponent<CharacterStats>().maxHealth.GetValue();
                playerWeapon.text = "Weapon: Machine Gun";
                playerSpeed.text = "Speed: " + gmCode.currentPlayer.GetComponent<CharacterStats>().moveSpeed.GetValue();
            }
        }
    }
}
