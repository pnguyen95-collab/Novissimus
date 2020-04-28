using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicController : MonoBehaviour
{
    GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (GameObject.FindGameObjectsWithTag("Audio") != null)
        {
            a = GameObject.FindGameObjectWithTag("Audio");

        }

        if (sceneName == "Start_Scene")
        {
            a.GetComponent<AudioController>().PlayBGMStartScene(0.5f);
        }
        else if (sceneName == "Desert_1")
        {
            a.GetComponent<AudioController>().PlayBGMCombatScene(0.3f);
        }
        else if(sceneName == "Player_Hub")
        {
            a.GetComponent<AudioController>().PlayBGMStartScene(0.5f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
