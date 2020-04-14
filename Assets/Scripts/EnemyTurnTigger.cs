using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnTigger : MonoBehaviour
{
    public GameObject gm;
    public GameManager gmCode;
    private float nextActionTime = 0.0f;
    public float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController");
        gmCode = gm.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        print("hi");

        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            gmCode.TestEnemy();
        }
        
    }
}
