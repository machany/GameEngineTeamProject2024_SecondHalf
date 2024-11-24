using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    public Camera _mainCamera;

    public float _moveSpeed = 10.0f;
    public float _zoomSpeed = 2.0f;
    
    public float _minZoom = 2.0f;
    public float _maxZoom = 10.0f;

    public Vector2 _worldBounds = new(20, 20);

    [SerializeField] private InputReader inputReader;

    private Vector3 _dragOrigin;

    private void Start()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;

            if (_mainCamera == null)
            {
                Debug.LogWarning("main camera not found!");
                return;
            }
        }

        if (_worldBounds == Vector2.zero)
            _worldBounds = new Vector2(20, 20);
    }

    private void Update()
    {
        HandleZoom();
        HandleKeyboardMovement(); // WASD 이동 처리
        ClampCameraPosition();
    }

    private void HandleZoom()
    {
        if (inputReader.ScrollValue.y != 0)
        {
            float newZoomValue = _mainCamera.orthographicSize -
                                 inputReader.ScrollValue.y * _zoomSpeed * Time.unscaledDeltaTime;
            _mainCamera.orthographicSize = Mathf.Clamp(newZoomValue, _minZoom, _maxZoom);
        }
    }

    private void HandleKeyboardMovement() =>
        _mainCamera.transform.position += (Vector3)inputReader.InputVector * (_moveSpeed * Time.unscaledDeltaTime);

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