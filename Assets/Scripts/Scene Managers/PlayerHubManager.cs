﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHubManager : MonoBehaviour
{
    public GameObject optionSelectPanel;
    public GameObject forageSelectPanel;

    public GameObject storyPanel;
    public GameObject finishRead;

    private PlayerData playerData;
    public GameObject plainsUnlocked;
    public GameObject storyEventButton;
    private GameObject audio;

    // Start is called before the first frame update
    void Start()
    {
        setOnOffMenu(optionSelectPanel, true);
        setOnOffMenu(forageSelectPanel, false);
        setOnOffMenu(plainsUnlocked, false);
        setOnOffMenu(storyEventButton, false);
        setOnOffMenu(storyPanel, false);

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

        if (GameObject.FindGameObjectsWithTag("Audio") != null)
        {
            audio = GameObject.FindGameObjectWithTag("Audio");
            audio.GetComponent<AudioController>().PlayBGMStartScene(0.4f);
        }
        else
        {
            print("audio manager missing");
        }
    }

    public void StoryEventInitiate()
    {
        setOnOffMenu(storyPanel, true);
        setOnOffMenu(optionSelectPanel, false);
        setOnOffMenu(forageSelectPanel, false);
    }

    public void FinishRead()
    {
        setOnOffMenu(storyPanel, false);
        setOnOffMenu(optionSelectPanel, false);
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
