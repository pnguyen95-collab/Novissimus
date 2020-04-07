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
    private GameObject currentPlayer;

    // Start is called before the first frame update
    void Start()
    {

        gmCode = gm.GetComponent<GameManager>();

    
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayer = gmCode.currentPlayerDataToShow;

        if (currentPlayer != null)
        {

            if (currentPlayer.GetComponent<PlayerBehavior>().mouseOver == true)
            {
                playerName.text = currentPlayer.name;
                playerHealth.text = "Health: " + currentPlayer.GetComponent<CharacterStats>().currentHealth + "/ " + currentPlayer.GetComponent<CharacterStats>().maxHealth.GetValue();
                playerWeapon.text = "Weapon: "+ gmCode.GetComponent<WeaponStats>().GiveNameOfTheWeapon(currentPlayer.GetComponent<CharacterStats>().weaponNumber); ;
                playerSpeed.text = "Speed: " + currentPlayer.GetComponent<CharacterStats>().moveSpeed.GetValue();
            }
        }
    }
}
