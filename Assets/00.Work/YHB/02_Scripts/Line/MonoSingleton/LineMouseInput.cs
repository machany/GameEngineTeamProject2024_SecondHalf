using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VHierarchy.Libs;

public class LineMouseInput : MonoSingleton<LineMouseInput>, IInitialize
{
    public Action<Transform> OnClickCompany;

    [SerializeField] private InputReader inputReader;
    [SerializeField] private CompanyDragChecker dragChecker;
    // [SerializeField] private PoolItemSO checker;

    private Camera _mainCam;

    // �巡�� ������
    private bool _dragMode;
    // ���� ���� ���
    private bool _destroyMode;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        _mainCam = Camera.main;
    }

    public void Initialize()
    {
        // inputReader.OnMouseClickEvent += HandleMouseClick;
        inputReader.OnMouseClickEvent += HandleMouseClickDragMode;
        inputReader.OnMouseClickRealseEvent += HandleMouseRealseDragMode;

        if (dragChecker is null)
        {
            dragChecker = new GameObject("CompanyDragChecker").AddComponent<CompanyDragChecker>();
            Debug.LogWarning("NullRef");
        }
        dragChecker.transform.parent = transform;
    }

    public void Disable()
    {
        // inputReader.OnMouseClickEvent -= HandleMouseClick;
        inputReader.OnMouseClickEvent -= HandleMouseClickDragMode;
        inputReader.OnMouseClickRealseEvent -= HandleMouseRealseDragMode;

        dragChecker.Destroy();
    }

    private void Update()
    {
        if (_dragMode)
            dragChecker.CheckBuilding(_mainCam.ScreenToWorldPoint(inputReader.MousePositionValue));
    }

    private void HandleMouseClickDragMode()
    {
        _dragMode = true;
        dragChecker.gameObject.SetActive(true);
        dragChecker.SetMouseClick();
        dragChecker.OnBuilding += HandleBuling;
    }

    private void HandleMouseRealseDragMode()
    {
        _dragMode = false;
        dragChecker.OnBuilding -= HandleBuling;
        dragChecker.gameObject.SetActive(false);
    }

    // ������
    private void HandleBuling(GameObject go)
    {
        OnClickCompany?.Invoke(go.transform);
    }

    /*#region 

    // (��) ���콺 Ŭ��
    private void HandleMouseClick()
    {
        CompanyChecker gameObject = PoolManager.Instance.Pop(checker).GetComponent<CompanyChecker>();
        gameObject.transform.position = inputReader.MousePositionValue;
        gameObject.OnBuilding += HandleBuling;
        gameObject.OnDestroy += HandleCheckerDestroy;
    }

    // ���� ���
    private void HandleCheckerDestroy(CompanyChecker go)
    {
        PoolManager.Instance.Push(checker, go.gameObject);

        CompanyChecker gameObject = go.GetComponent<CompanyChecker>();
        gameObject.OnBuilding -= HandleBuling;
        gameObject.OnDestroy -= HandleCheckerDestroy;
    }

    #endregion*/
}
