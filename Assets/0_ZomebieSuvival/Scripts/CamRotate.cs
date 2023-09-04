using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 200f;
    float mx = 0;
    float my = 0;

    void Update()
    {
        MoveMouse();
    }
    void MoveMouse()
    {
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");
        
        //거리 = 속 * 시간
        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;
        
        //Mathf 함수  Clamp 제한
        my = Mathf.Clamp(my, -90f, 90f);

        //회전
        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
    
}
