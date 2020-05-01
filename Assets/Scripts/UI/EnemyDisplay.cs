using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDisplay : MonoBehaviour
{
    public GameObject gm;
    public GameManager gmCode;
    public Text enemyName;
    public Text enemyHealth;
    public Text enemySpeed;
    public Text enemyWeapon;
    public GameObject currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        gmCode = gm.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentEnemy = gmCode.currentEnemy;

        if (currentEnemy != null)
        {

            if (currentEnemy.GetComponent<EnemyBehavior>().mouseOver == true)
            {
                enemyName.text = currentEnemy.name;
                enemyHealth.text = "Health: " + currentEnemy.GetComponent<CharacterStats>().currentHealth + "/ " + currentEnemy.GetComponent<CharacterStats>().maxHealth.GetValue();
                enemyWeapon.text = "Attack Type: "+gmCode.GetComponent<WeaponStats>().GiveNameOfTheWeapon(currentEnemy.GetComponent<CharacterStats>().weaponNumber);
                enemySpeed.text = "Speed: " + currentEnemy.GetComponent<CharacterStats>().moveSpeed.GetValue();
            }
        }
    } 
}
