using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHubManager : MonoBehaviour
{
    public GameObject optionSelectPanel;
    public GameObject forageSelectPanel;

    // Start is called before the first frame update
    void Start()
    {
        setOnOffMenu(optionSelectPanel, true);
        setOnOffMenu(forageSelectPanel, false);
    }

    // Update is called once per frame
     void Update()
    {

    }

    public void setOnOffMenu(GameObject target, bool x)
    {
        target.SetActive(x);
    }

    public void choosingLocale()
    {
        //progression check function here

        setOnOffMenu(optionSelectPanel, false);
        setOnOffMenu(forageSelectPanel, true);
    }

    public void exitButton()
    {
        setOnOffMenu(optionSelectPanel, true);
        setOnOffMenu(forageSelectPanel, false);
    }
}
