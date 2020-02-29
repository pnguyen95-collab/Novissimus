using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    //this button for Testing purpose

    public Button startButton;
    public InputField x;
    
    public InputField y;
    public GameObject[] players;
    public GameObject player;
    
    void Start()
    {
        
        
        players = GameObject.FindGameObjectsWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player");

        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        int numX = int.Parse(x.text);
        int numY = int.Parse(y.text);
        player.GetComponent<PlayerBehavior>().PlayerMove(numX,numY);
    }
}
