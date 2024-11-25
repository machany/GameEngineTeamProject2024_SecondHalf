using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraControl : MonoSingleton<CameraControl>
{
    [Header("InputReader")]
    [SerializeField] private InputReader inputReader;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 10.0f;

    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 2.0f;
    [SerializeField] private float minZoom = 2.0f;
    [SerializeField] private float maxZoom = 10.0f;

    [Header("Setting")]
    public Vector2 worldBounds = Vector2.zero;
    [Range(0.1f, 1f)]
    [SerializeField] private float defaultCameraSize = 0.5f;

    private Camera _mainCamera;
    private Vector3 _dragOrigin;

    private void Awake()
    {
        Initialize();
    }
        
    private void Initialize()
    {
        _mainCamera = null;
        _mainCamera = Camera.main;

        try
        {
            _mainCamera.orthographicSize = Mathf.Lerp(minZoom, maxZoom, defaultCameraSize);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);

            GameObject camera = new GameObject();
            camera.name = "Camera";
            camera.transform.parent = transform;
            camera.AddComponent<Camera>();
            camera.AddComponent<UniversalAdditionalCameraData>();
            camera.AddComponent<CinemachineBrain>();
            _mainCamera = Camera.main;
            _mainCamera.orthographicSize = Mathf.Lerp(minZoom, maxZoom, defaultCameraSize);
        }

        if (EqualityComparer<Vector2>.Default.Equals(worldBounds, Vector2.zero))
            worldBounds = new Vector2(10, 10);
    }

    private void LateUpdate()
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
                                 inputReader.ScrollValue.y * zoomSpeed * Time.unscaledDeltaTime;
            _mainCamera.orthographicSize = Mathf.Clamp(newZoomValue, minZoom, maxZoom);
        }
    }

    private void HandleKeyboardMovement() =>
        _mainCamera.transform.position += (Vector3)inputReader.InputVector * (moveSpeed * Time.unscaledDeltaTime);

    private void ClampCameraPosition()
    {
        float camHeight = _mainCamera.orthographicSize;
        float camWidth = camHeight * _mainCamera.aspect;

        Vector3 camPos = _mainCamera.transform.position;

        float minX = -worldBounds.x / 2 + camWidth;
        float maxX = worldBounds.x / 2 - camWidth;
        float minY = -worldBounds.y / 2 + camHeight;
        float maxY = worldBounds.y / 2 - camHeight;

        camPos.x = Mathf.Clamp(camPos.x, minX, maxX);
        camPos.y = Mathf.Clamp(camPos.y, minY, maxY);

        _mainCamera.transform.position = camPos;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(worldBounds.x, worldBounds.y, 0));
    }
#endif
}