using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    public float cameraSpeed;
    public float cameraZoomSpeed;

    private Vector3 desiredPos;

    private void Start() {
        currentZoom = Camera.main.orthographicSize;
    }

    private void Update() 
    {
        Pan();
        Zoom();
    }

    int vertMove;
    int horMove;
    float currentZoom;
    private void Pan ()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            vertMove = 1;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) )
        {
            vertMove = -1;
        }
        else
        {
            vertMove = 0;
        }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horMove = 1;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
        {
            horMove = -1;
        }
        else
        {
            horMove = 0;
        }

        desiredPos =  transform.position + new Vector3 (horMove * cameraSpeed, vertMove * cameraSpeed, 0);

        transform.position = Vector3.Lerp (transform.position, desiredPos, Time.deltaTime);
    }
    
    private void Zoom()
    {
        //Scroll wheel is inverted
        currentZoom -= Input.mouseScrollDelta.y * cameraZoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, 1, 20);
        Camera.main.orthographicSize = currentZoom;
    }

}
