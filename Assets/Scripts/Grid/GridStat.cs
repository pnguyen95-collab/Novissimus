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
        switch (status)
        {
            case 0:
                this.GetComponent<Renderer>().material.color = Color.white;
                break;
            case 1:
                this.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 2:
                this.GetComponent<Renderer>().material.color = Color.green;
                break;

        }             
    }
}
