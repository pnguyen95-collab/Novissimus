using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDisplay : MonoBehaviour
{
    /* public GameManager gmCode;
    public Text enemyName;
    public Text enemyHealth;
    public Text enemySpeed;
    public Text enemyWeapon;
    public CharacterStats enemyStats;
    public GameObject currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        gmCode = this.GetComponent<GameManager>();
        CharacterStats = this.Getcomponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        currentEnemy = gmCode.GetComponent<GameObject>().currentEnemy;

        if (currentEnemy != null)
        {

            if (currentEnemy.GetComponent<enemyBehavior>().enemyIsActive == true)
            {
                enemyName.text = "Cannibal Grunt";
                enemyHealth.text = "Health: " + currentEnemy.currentHealth + "/ " + currentEnemy.maxHealth.GetValue();
                enemyWeapon.text = "N/A";
                enemySpeed.text = "Speed: " + currentEnemy.moveSpeed.GetValue();
            }
        }
    } */
}
