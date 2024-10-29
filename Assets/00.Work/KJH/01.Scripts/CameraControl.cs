using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraControl : MonoBehaviour
{
    public Camera mainCamera;        
    public float zoomSpeed = 2.0f;   
    public float moveSpeed = 10.0f;  
    public float minZoom = 2.0f;     
    public float maxZoom = 10.0f;    

    public Vector2 worldBounds;      

    private Vector3 dragOrigin;       
    private bool isDragging = false;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; 
        }
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            ZoomCamera(scroll);
        }
        
        if (Input.GetMouseButtonDown(0)) 
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging) 
        {
            DragCamera();
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            isDragging = false;
        }
        
        ClampCameraPosition();
    }
    
    void ZoomCamera(float increment)
    {
        float newZoom = mainCamera.orthographicSize - increment * zoomSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(newZoom, minZoom, maxZoom);
    }
    
    void DragCamera()
    {
        Vector3 currentMousePos = Input.mousePosition;
        Vector3 difference = dragOrigin - currentMousePos;

        Vector3 moveDirection = new Vector3(difference.x, difference.y, 0) * (Time.deltaTime * moveSpeed);
        mainCamera.transform.Translate(moveDirection);

        dragOrigin = currentMousePos;
    }
    
    void ClampCameraPosition()
    {
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;
        
        Vector3 camPos = mainCamera.transform.position;
        
        float minX = -worldBounds.x / 2 + camWidth;
        float maxX = worldBounds.x / 2 - camWidth;
        float minY = -worldBounds.y / 2 + camHeight;
        float maxY = worldBounds.y / 2 - camHeight;
        
        camPos.x = Mathf.Clamp(camPos.x, minX, maxX);
        camPos.y = Mathf.Clamp(camPos.y, minY, maxY);
        
        mainCamera.transform.position = camPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position,worldBounds);
    }
}
