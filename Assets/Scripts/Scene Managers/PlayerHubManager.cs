using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHubManager : MonoBehaviour
{
    public GameObject optionSelectPanel;
    public GameObject forageSelectPanel;

    private PlayerData playerData;
    public GameObject plainsUnlocked;
    public GameObject storyEventButton;

    // Start is called before the first frame update
    void Start()
    {
        setOnOffMenu(optionSelectPanel, true);
        setOnOffMenu(forageSelectPanel, false);
        setOnOffMenu(plainsUnlocked, false);
        setOnOffMenu(storyEventButton, false);

        //finds player data
        if (GameObject.Find("PlayerInventory") != null)
        {
            GameObject temp = GameObject.Find("PlayerInventory");

            playerData = temp.GetComponent<PlayerData>();
        }
        else
        {
            print("Missing player data");
        }
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
        //checks player's current narrative progression and unlocks areas
        if (playerData.gameProgression.Contains(Progression.PlainsUnlocked))
        {
            setOnOffMenu(plainsUnlocked, true);
        }

        if (playerData.gameProgression.Contains(Progression.StoryEvent))
        {
            setOnOffMenu(storyEventButton, true);
        }
        else
        {
            setOnOffMenu(storyEventButton, false);
        }

        setOnOffMenu(optionSelectPanel, false);
        setOnOffMenu(forageSelectPanel, true);
    }

    public void exitButton()
    {
        setOnOffMenu(optionSelectPanel, true);
        setOnOffMenu(forageSelectPanel, false);
    }
}
