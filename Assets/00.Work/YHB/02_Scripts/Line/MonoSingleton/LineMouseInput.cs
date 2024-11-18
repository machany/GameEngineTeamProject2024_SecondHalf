using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMouseInput : MonoSingleton<LineMouseInput>
{
    public Action<Transform> OnClickCompany;

    [SerializeField] private InputReader inputReader;
    [SerializeField] private PoolItemSO checker;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        //inputReader.OnMouseClick += HandleMouseClick;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void HandleMouseClick(Vector2 inputPos)
    {
        CompanyChecker gameObject = PoolManager.Instance.Pop(checker).GetComponent<CompanyChecker>();
        gameObject.transform.position = inputPos;
        gameObject.OnBuilding += HandleBuling;
        gameObject.OnDestroy += HandleCheckerDestroy;
    }

    // ���� ������ �ѱ�
    private void HandleBuling(GameObject go)
    {
        OnClickCompany?.Invoke(go.transform);
    }

    // ���� ���
    private void HandleCheckerDestroy(GameObject go)
    {
        PoolManager.Instance.Push(checker, go);

        CompanyChecker gameObject = go.GetComponent<CompanyChecker>();
        gameObject.OnBuilding -= HandleBuling;
        gameObject.OnDestroy -= HandleCheckerDestroy;
    }
}
