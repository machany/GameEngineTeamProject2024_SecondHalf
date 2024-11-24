using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    // 반투명화를 위함
    private SpriteRenderer _spriteRenderer;
    private VehicleStorage _vehicleStorage;

    [SerializeField] private PoolItemSO _me;
    [SerializeField] private float _moveSpeed;

    // 이동 관련
    private static Ease ease = Ease.InOutCubic;
    private Transform _currentTargetTrm;
    [SerializeField] private float stopTime;

    // 내가 속해있는 선로
    private LineSO _currentLine;

    // 내가 갈 방향
    public sbyte _dir { get; private set; } = -1;
    private int _index;
    // _index호줄시 처리해야 할 과정을 위함
    public int index
    {
        get => _index;
        private set
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

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        _vehicleStorage = transform.GetComponent<VehicleStorage>();

        LineController.Instance.OnLineInfoChanged += HandleLineInfoChanged;
        LineController.Instance.OnLineTypeChanged += HandleLineTypeChange;

        _moveSpeed = _vehicleStorage.vehicleSO.moveSpeed;
    }

    // 반투명화 등 처리
    private void HandleLineTypeChange(LineType curLineType)
    {
        Color color = _spriteRenderer.color;
        color.a = EqualityComparer<LineType>.Default.Equals(_currentLine.type, curLineType) ? 1f : LineController.Instance.invisibleValue;
        _spriteRenderer.color = color;
    }

    private void HandleLineInfoChanged(LineSO curLine, int value, bool isAdd)
    {
        if (value > 0) return;

        if (EqualityComparer<LineSO>.Default.Equals(_currentLine, curLine))
        {
            // 변경 후 도착하기 전 다시 연결 시를 위한 인덱스 변경
            // index = _currentLine.lineInfo.FindValueLocation(_currentTargetTrm);

            if (index > value)
                index += isAdd ? 1 : -1;
        }
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

            _vehicleStorage.ArriveBuilding(_currentTargetTrm);

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

        LookAt(targetTrm.position);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(targetTrm.position, dir.magnitude / _moveSpeed).SetEase(ease)).SetDelay(stopTime);
        seq.OnComplete(SetMove);

        _currentTargetTrm = targetTrm;
    }

    private void LookAt(Vector3 vector)
    {
        Vector3 direction = vector - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    private void OnDisable()
    {
        Disable();
    }

    private void Disable()
    {
        LineController.Instance.OnLineInfoChanged -= HandleLineInfoChanged;
        LineController.Instance.OnLineTypeChanged -= HandleLineTypeChange;
    }
}
