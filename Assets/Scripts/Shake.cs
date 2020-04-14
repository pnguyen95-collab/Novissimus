using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private bool shaking = false;
    public float shakeAmt;
    private Vector3 originalPos;
    //if you move   'Vector3 OriginalPos  = transform.position' to a global variable.

//Set OriginalPos in the start function (OriginalPos = transform.position;)

//Then change    Vector3 NewPosition = Random.insideUnitSphere * (Time.deltaTime * ShakeAmount);
    //to
                             // Vector3 NewPosition = OriginalPos + Random.insideUnitSphere* (Time.deltaTime* ShakeAmount);
    void Update()
    {
        originalPos = transform.position;

        if (shaking)
        {
            Vector3 newPos = originalPos + Random.insideUnitSphere * (Time.deltaTime * shakeAmt);
            
            newPos.y = transform.position.y;
            newPos.z = transform.position.z;

            transform.position = newPos;
        }
    }

    public void ShakeObject()
    {

        StartCoroutine(ShakeNow());
    }

    IEnumerator ShakeNow()
    {
        
        if (shaking == false)
        {
            shaking = true;
        }

        yield return new WaitForSeconds(0.5f);
        shaking = false;
        transform.position = originalPos;
    }
}
