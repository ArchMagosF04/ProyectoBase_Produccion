using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotateToCursor : MonoBehaviour
{
    Vector3 MousePos;
    Camera can;
    Rigidbody2D Rb;

    void Start()
    {
        Rb = this.GetComponent<Rigidbody2D>();
        can = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        rotaToCamera();
    }

    void rotaToCamera() 
    {
        MousePos = can.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z-can.transform.position.z));
        Rb.transform.eulerAngles = new Vector3(0, 0, math.atan2((MousePos.y - transform.position.y), (MousePos.x - transform.position.x)) *Mathf.Rad2Deg);
    }
}
