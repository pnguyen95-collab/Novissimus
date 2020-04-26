using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIButtonManager : MonoBehaviour, IPointerEnterHandler
{
    private GameObject a;
    private Button thisButton; 

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Audio") != null)
        {
            a = GameObject.FindGameObjectWithTag("Audio");
            
        }
        thisButton = this.GetComponent<Button>();    
    }

    // Update is called once per frame
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        a.GetComponent<AudioController>().PlayButtonHover();
    }
    public void Click()
    {
        a.GetComponent<AudioController>().PlayButtonClick();
    }
}
