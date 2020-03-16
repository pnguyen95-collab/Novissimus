using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagement : MonoBehaviour
{
    public int speed;
    public Camera m_OrthographicCamera;

    //private void Start() don't need this
    //{
    //    speed = 5;
    //}


    //Moving the camera around with the arrow keys

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            m_OrthographicCamera.orthographicSize--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            m_OrthographicCamera.orthographicSize++;
        }

    }
}
