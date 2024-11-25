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

    [Header("Setting")]
    public Vector2 maxWorldBounds = Vector2.zero;
    [HideInInspector] public Vector2 curWorldBounds = Vector2.zero;

    private Camera _mainCamera;

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
            _mainCamera.orthographicSize = minZoom;
        }
        catch (NullReferenceException)
        {
            GameObject camera = new GameObject();
            camera.name = "Camera";
            camera.transform.parent = transform;
            camera.AddComponent<Camera>();
            camera.AddComponent<UniversalAdditionalCameraData>();
            camera.AddComponent<CinemachineBrain>();
            _mainCamera = Camera.main;
            _mainCamera.orthographicSize = minZoom;
        }
        finally
        {
            if (EqualityComparer<Vector2>.Default.Equals(maxWorldBounds, Vector2.zero))
                maxWorldBounds = new Vector2(100, 100);

            curWorldBounds = maxWorldBounds;
        }
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

            // 이동범위보다 작고, 카메라 줌 범위내로 카메라를 설정
            _mainCamera.orthographicSize = Mathf.Clamp(newZoomValue, minZoom, Mathf.Min(curWorldBounds.x / 3 * (Screen.width / Screen.height), curWorldBounds.y / 3)); // 카메라 줌 범위
        }
    }

    private void HandleKeyboardMovement()
        => _mainCamera.transform.position += (Vector3)inputReader.InputVector * (moveSpeed * Time.unscaledDeltaTime);

    private void ClampCameraPosition()
    {
        float camHeight = _mainCamera.orthographicSize;
        float camWidth = camHeight * _mainCamera.aspect;

        Vector3 camPos = _mainCamera.transform.position;

        float minX = -curWorldBounds.x / 2 + camWidth;
        float maxX = curWorldBounds.x / 2 - camWidth;
        float minY = -curWorldBounds.y / 2 + camHeight;
        float maxY = curWorldBounds.y / 2 - camHeight;

        camPos.x = Mathf.Clamp(camPos.x, minX, maxX);
        camPos.y = Mathf.Clamp(camPos.y, minY, maxY);

        _mainCamera.transform.position = camPos;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(maxWorldBounds.x, maxWorldBounds.y, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(curWorldBounds.x, curWorldBounds.y, 0));
    }
#endif
}