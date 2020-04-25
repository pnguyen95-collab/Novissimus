using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationManager : MonoBehaviour
{
    public int selectedVehicle;

    public GameObject displayInfo;
    public GameObject displayStats;

    public string vehicleName;
    public GameObject nameInput;

    public GameObject alertText;

    private void Update()
    {
        //update player name
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (nameInput.GetComponent<Text>().text != "")
            {
                vehicleName = nameInput.GetComponent<Text>().text;
                UpdateStats();
            }
        }
    }

    public void UpdateStats()
    {
        //checks to see if you have a vehicle selected
        if (selectedVehicle != 0)
        {

        }
        else
        {
            StartCoroutine(PopupText("Please select a vehicle first!"));
        }

    }

    public void SelectVehicle(GameObject select)
    {
        //null check
        if (select != null)
        {
            selectedVehicle = int.Parse(select.GetComponentInChildren<Button>().transform.name);

            //changes the color of the selected tab and resets the other tabs colors
            GameObject[] tabs;
            tabs = GameObject.FindGameObjectsWithTag("Tab");

            foreach(GameObject obj in tabs)
            {
                obj.transform.GetComponent<Image>().color = new Color32(170, 170, 170, 255);
            }
      
            select.transform.GetComponent<Image>().color = new Color32(135, 135, 135, 255);

            UpdateStats();

            displayInfo.SetActive(true);
            displayStats.SetActive(true);
        }
        else
        {
            Debug.Log("error, not selectable");
        }
    }

    public IEnumerator PopupText(string x)
    {
        print("doing pop up text");
        alertText.SetActive(true);
        alertText.GetComponent<Text>().text = x;

        alertText.GetComponent<Text>().canvasRenderer.SetAlpha(1);
        alertText.GetComponent<Text>().CrossFadeAlpha(0.0f, 2.5f, false);
        yield return new WaitForSeconds(2f);
        alertText.SetActive(false);
        alertText.GetComponent<Text>().canvasRenderer.SetAlpha(1);
    }
}
