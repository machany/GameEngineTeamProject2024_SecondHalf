using DG.Tweening;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;

public class Vehicle : VehicleStorage
{
    [SerializeField] private PoolItemSO _me;
    [SerializeField] private float _moveSpeed;

    // 이동 관련
    private static Ease ease = Ease.InOutQuad;
    private Transform _currentTargetTrm;
    [SerializeField] private float stopTime;

    // 내가 속해있는 선로
    private LineSO _currentLine;

    // 내가 갈 방향
    private sbyte _dir = -1;
    private int _index;
    // _index호줄시 처리해야 할 과정을 위함
    private int index
    {
        get => _index;
        set
        {
            if (value <= 0)
            {
                _index = 0;
                _dir = 1;
            }
            else if (value >= _currentLine.lineInfo.Count)
            {
                _index = _currentLine.lineInfo.Count - 1;
                _dir = -1;
            }
            else
                _index = value;
        }
    }

    protected override void Initialize()
    {
        base.Initialize();
        LineController.Instance.OnLineInfoChanged += HandleLineInfoChanged;

        _moveSpeed = vehicleSO.moveSpeed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ss");

        SetMove();
        base.OnTriggerEnter2D(collision);
    }

    private void HandleLineInfoChanged(LineSO curLine, int value, bool isAdd)
    {
        if (!EqualityComparer<LineSO>.Default.Equals(_currentLine, curLine) || value == -1)
        {
            PoolManager.Instance.Push(_me, gameObject);
            return;
        }

        if (index > value)
            index += isAdd ? 1 : -1;
    }

    // 라인 설정
    public void SetLine(LineType lineType, LineGroupType lineGroupType, Vector3? startPos = null)
    {
        _currentLine = LineController.Instance.GetLine(lineType, lineGroupType);

        if (_currentLine.lineInfo.Count <= 0)
            return;
        else if (startPos is null)
            transform.position = _currentLine.lineInfo[0].transform.position;
        else
            transform.position = (Vector3)startPos;

        index = 0;
        _dir = 1;

        _currentTargetTrm = _currentLine.lineInfo[index];
        index += _dir;
        SetMove(_currentLine.lineInfo[index]);
        index += _dir;
    }

    private void SetMove()
    {
        try
        {
            if (!_currentLine.lineInfo.Contains(_currentTargetTrm))
                throw new Exception("의도된 예외입니다. 현재 라인에 도착지점이 없음");

            SetMove(_currentLine.lineInfo[index]);
            index += _dir;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            PoolManager.Instance.Push(_me, gameObject);
            return;
        }
    }

    // 움직이는 방향 설정
    private void SetMove(Transform targetTrm)
    {
        Debug.Log(targetTrm.position);

        Vector3 dir = targetTrm.position - _currentTargetTrm.position;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(targetTrm.position, dir.magnitude / _moveSpeed * 10).SetEase(ease)).SetDelay(stopTime);
        seq.OnComplete(SetMove);

        _currentTargetTrm = targetTrm;
    }

    protected override void OnDisable()
    {
        Disable();
    }

    private void Disable()
    {
        LineController.Instance.OnLineInfoChanged -= HandleLineInfoChanged;
    }
}
