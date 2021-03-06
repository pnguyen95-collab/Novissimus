﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagement : MonoBehaviour
{
    public int speed;
    public Camera m_OrthographicCamera;

    //Moving the camera around with the WASD keys & scrolling

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (this.transform.position.x < 30)
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            if(this.transform.position.x>0)
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (this.transform.position.z > 0)
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (this.transform.position.z < 30)
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if(m_OrthographicCamera.orthographicSize>4)
            m_OrthographicCamera.orthographicSize--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (m_OrthographicCamera.orthographicSize < 11)
                m_OrthographicCamera.orthographicSize++;
        }

    }
}
