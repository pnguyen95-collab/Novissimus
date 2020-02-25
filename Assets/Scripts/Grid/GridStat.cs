using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStat : MonoBehaviour
{
    public int visit = -1;
    public int x = 0;
    public int y = 0;
    public bool standable = true;

    public int status;
    // Start is called before the first frame update
    void Start()
    {
        status = 0;
    }

    // Update is called once per frame
    void Update()
    {
        StandableStatus();


        switch (status)
        {
            case 0:
                this.GetComponent<Renderer>().material.color = Color.white;
                break;
            case 1:
                this.GetComponent<Renderer>().material.color = Color.yellow;
                StartCoroutine(ChangeColorBack());
                break;
            case 2:
                this.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 3:
                this.GetComponent<Renderer>().material.color = Color.gray;
                break;
            case 4:
                this.GetComponent<Renderer>().material.color = Color.red;
                break;

        }             
    }

    private void OnMouseOver()
    {
        this.status = 3;
    }


    void OnMouseExit()
    {
         this.status = 0;
    }

    IEnumerator ChangeColorBack()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        this.status = 0;

        //yield return false;
    }

    void StandableStatus()
    {
        if (standable == false)
        {
            this.status = 4;
        }
            
    }
}
