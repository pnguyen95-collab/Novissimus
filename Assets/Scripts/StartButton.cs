using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button startButton;
    public InputField x;
    
    public InputField y;
    public GameObject player;
    
    void Start()
    {
        
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
