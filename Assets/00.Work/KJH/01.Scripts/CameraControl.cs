using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera _mainCamera;        
    public float _zoomSpeed = 2.0f;   
    public float _moveSpeed = 10.0f;  
    public float _minZoom = 2.0f;     
    public float _maxZoom = 10.0f;    
    public Vector2 _worldBounds = new Vector2(20, 20); 
    private Vector3 _dragOrigin;      
    private const int RightMouseButton = 1; 
    private bool _isDragging = false;

    private void Start()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
            if (_mainCamera == null)
            {
                return;
            }
        }
        
        if (_worldBounds == Vector2.zero)
        {
            _worldBounds = new Vector2(20, 20);
        }
    }

    private void Update()
    {
        HandleZoom();
        HandleDrag();
        HandleKeyboardMovement(); // WASD 이동 처리
        ClampCameraPosition();
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Math.Abs(scroll) > 0.01f) 
        {
            float newZoom = _mainCamera.orthographicSize - scroll * _zoomSpeed;
            _mainCamera.orthographicSize = Mathf.Clamp(newZoom, _minZoom, _maxZoom);
        }
    }

    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(RightMouseButton))
        {
            _dragOrigin = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _isDragging = true;
        }

        if (Input.GetMouseButton(RightMouseButton) && _isDragging)
        {
            Vector3 currentMousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = _dragOrigin - currentMousePos;
            
            _mainCamera.transform.position += difference;
        }

        if (Input.GetMouseButtonUp(RightMouseButton))
        {
            _isDragging = false;
        }
    }

    private void HandleKeyboardMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 moveDirection = new Vector3(horizontal, vertical, 0).normalized;

        _mainCamera.transform.position += moveDirection * (_moveSpeed * Time.deltaTime);
    }

    private void ClampCameraPosition()
    {
        float camHeight = _mainCamera.orthographicSize;
        float camWidth = camHeight * _mainCamera.aspect;

        Vector3 camPos = _mainCamera.transform.position;

        float minX = -_worldBounds.x / 2 + camWidth;
        float maxX = _worldBounds.x / 2 - camWidth;
        float minY = -_worldBounds.y / 2 + camHeight;
        float maxY = _worldBounds.y / 2 - camHeight;

        camPos.x = Mathf.Clamp(camPos.x, minX, maxX);
        camPos.y = Mathf.Clamp(camPos.y, minY, maxY);

        _mainCamera.transform.position = camPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(_worldBounds.x, _worldBounds.y, 0));
    }
}
