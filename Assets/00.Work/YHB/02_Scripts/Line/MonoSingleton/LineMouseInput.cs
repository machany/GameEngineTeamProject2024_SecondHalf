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

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        // inputReader.OnMouseClickEvent += HandleMouseClick;
        inputReader.OnMouseClickEvent += HandleMouseClickDragMode;
        inputReader.OnMouseClickRealseEvent += HandleMouseRealseDragMode;

        OptionUI.OnOptionPanel += HandleOptionEvent;

        if (dragChecker is null)
        {
            dragChecker = new GameObject("CompanyDragChecker").AddComponent<CompanyDragChecker>();
            Debug.LogWarning("NullRef");
        }
        dragChecker.transform.parent = transform;

        _mainCam = Camera.main;
    }

    public void Disable()
    {
        // inputReader.OnMouseClickEvent -= HandleMouseClick;
        inputReader.OnMouseClickEvent -= HandleMouseClickDragMode;
        inputReader.OnMouseClickRealseEvent -= HandleMouseRealseDragMode;
        
        OptionUI.OnOptionPanel -= HandleOptionEvent;

        dragChecker.Destroy();
    }

    private void Update()
    {
        if (_dragMode)
            dragChecker.CheckBuildingDrag(_mainCam.ScreenToWorldPoint(inputReader.MousePositionValue));
    }

    private void HandleMouseClickDragMode()
    {
        _dragMode = true;
        dragChecker.gameObject.SetActive(true);
        dragChecker.SetMouseClick(inputReader.MousePositionValue);
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
    
    private void HandleOptionEvent(bool isOnPanel)
    {
        if (isOnPanel)
        {
            inputReader.OnMouseClickEvent -= HandleMouseClickDragMode;
            inputReader.OnMouseClickRealseEvent -= HandleMouseRealseDragMode;
        }
        else
        {
            inputReader.OnMouseClickEvent += HandleMouseClickDragMode;
            inputReader.OnMouseClickRealseEvent += HandleMouseRealseDragMode;
        }
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
