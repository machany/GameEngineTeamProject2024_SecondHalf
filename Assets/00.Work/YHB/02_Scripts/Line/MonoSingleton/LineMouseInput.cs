using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMouseInput : MonoSingleton<LineMouseInput>, IInitialize
{
    public Action<Transform> OnClickCompany;

    [SerializeField] private InputReader inputReader;
    [SerializeField] private PoolItemSO checker;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        //inputReader.OnMouseClick += HandleMouseClick;
    }

    public void Disable()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("수정 필요");
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

    // 빌딩 정보를 넘김
    private void HandleBuling(GameObject go)
    {
        OnClickCompany?.Invoke(go.transform);
    }

    // 구독 취소
    private void HandleCheckerDestroy(GameObject go)
    {
        PoolManager.Instance.Push(checker, go);

        CompanyChecker gameObject = go.GetComponent<CompanyChecker>();
        gameObject.OnBuilding -= HandleBuling;
        gameObject.OnDestroy -= HandleCheckerDestroy;
    }
}
