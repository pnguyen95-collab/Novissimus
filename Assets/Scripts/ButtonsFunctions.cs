using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsFunctions : MonoBehaviour
{
    public GameObject gm;
    public GameManager gmCode;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController");
        gmCode = gm.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
