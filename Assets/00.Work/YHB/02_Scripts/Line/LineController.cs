using System;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoSingleton<LineController>
{
    public Action OnLineChanged;
    // 현재 라인이 감지한 모든 강의 갯수
    public Action<int> OnBridgeChanged;
    public Action<LineType> OnLineTypeChanged;
    // 현재 라인, 변경이 된 인덱스, 추가로 인한 변경 시 true
    public Action<LineSO, int, bool> OnLineInfoChanged;

    public LineType CurrentLineType { get; private set; }
    public LineGroupType CurrentGroupType { get; private set; }

    [Header("Obstacle")]
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Line")]
    [SerializeField] private PoolItemSO linerender;
    public float invisibleValue = 0.3f;

    private LineSO _curLine;
    private Transform _currentTrm;
    [HideInInspector] public List<LineSO> lines = new List<LineSO>();

    // Test
    [Header("Test")]
    [SerializeField] private PoolItemSO vehile;

    // Test
    private void Update()
    {
        LineType TLT = CurrentLineType;
        LineGroupType TGT = CurrentGroupType;

        if (Input.GetKeyDown(KeyCode.O))
        {
            TLT = LineType.Input;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            TLT = LineType.Output;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            TGT = LineGroupType.Red;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            TGT = LineGroupType.Blue;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            TGT = LineGroupType.Green;
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vehicle vehicle = PoolManager.Instance.Pop(vehile).GetComponent<Vehicle>();
            vehicle.SetLine(CurrentLineType, CurrentGroupType);
        }

        HandleChangedLineType(TLT, TGT);
    }

    public LineType GetLineType() => CurrentLineType;
    public LineGroupType GetGroupType() => CurrentGroupType;
    public LineSO GetLine(LineType lineType, LineGroupType lineGroupType)
    {
        foreach (LineSO line in lines)
            if (EqualityComparer<LineType>.Default.Equals(line.type, lineType) && EqualityComparer<LineGroupType>.Default.Equals(line.group, lineGroupType))
                return line;
        return null;
    }
    private int GetAllBridgeCount()
    {
        int i = 0;
        foreach (LineSO line in lines)
            i += line.usedBridgeCount;
        return i;
    }

    private void Awake()
    {
        //LineInitialize();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        LineMouseInput.Instance.OnClickCompany += HandleClickCompany;

        foreach (LineType type in Enum.GetValues(typeof(LineType)))
            foreach (LineGroupType group in Enum.GetValues(typeof(LineGroupType)))
            {
                LineSO line = new LineSO();
                line.type = type;
                line.group = group;
                line.render = PoolManager.Instance.Pop(linerender).GetComponent<LineRender>();
                line.render.Initialize(line);
                line.usedBridgeCount = 0;
                lines.Add(line);
            }

        _curLine = lines[0];
        SetLineColor();
    }

    private void HandleChangedLineType(LineType lineType, LineGroupType lineGroupType)
    {
        if (EqualityComparer<LineType>.Default.Equals(CurrentLineType, lineType) && EqualityComparer<LineGroupType>.Default.Equals(CurrentGroupType, lineGroupType))
            return;

        SetLineType(lineType, lineGroupType);
        OnLineTypeChanged?.Invoke(CurrentLineType);

        ShotRay();
    }

    // 회사 클릭시
    private void HandleClickCompany(Transform companyTrm)
    {
        if (_curLine.lineInfo.Contains(companyTrm))
        {
            if (_currentTrm is not null && EqualityComparer<Transform>.Default.Equals(companyTrm, _currentTrm))
            {
                int removeBefore = _curLine.lineInfo.FindValueLocation(companyTrm);

                _curLine.lineInfo.Remove(companyTrm);

                if (_curLine.lineInfo.Count <= 1)
                {
                    _curLine.lineInfo.Clear();
                    OnBridgeChanged?.Invoke(0);
                    OnLineInfoChanged?.Invoke(_curLine, -1, false);

                    goto ClearSkip;
                }

                OnLineInfoChanged?.Invoke(_curLine, removeBefore, false);
                _currentTrm = null;

            ClearSkip:
                goto EndProces;
            }

            _currentTrm = companyTrm;
            return;
        }

        if (_currentTrm is not null)
        {
            int location = _curLine.lineInfo.FindValueLocation(_currentTrm) - 1;
            if (location <= 0)
            {
                _curLine.lineInfo.AddAt(companyTrm, 0);
            }
            else if (location == _curLine.lineInfo.Count - 1)
            {
                _curLine.lineInfo.Add(companyTrm);
            }
            else
            {
                // 클릭한 건물과 연결된 가장 가까운 건물과 연결
                _curLine.lineInfo.AddAt(companyTrm,
                    (_curLine.lineInfo[location - 1].position - companyTrm.position).magnitude
                    < (_curLine.lineInfo[location + 1].position - companyTrm.position).magnitude ? location : location + 1);
            }
        }
        else
            _curLine.lineInfo.Add(companyTrm);

        OnLineInfoChanged?.Invoke(_curLine, _curLine.lineInfo.FindValueLocation(companyTrm), true);

        _currentTrm = companyTrm;

    EndProces:
        ShotRay();
        _curLine.render.DrawLine();
    }

    // 라인을 설정
    public void SetLineType(LineType lineValue, LineGroupType groupValue)
    {
        CurrentLineType = lineValue;
        CurrentGroupType = groupValue;

        foreach (LineSO lineInfo in lines)
        {
            if (!EqualityComparer<LineSO>.Default.Equals(_curLine, lineInfo) && EqualityComparer<LineGroupType>.Default.Equals(lineInfo.group, groupValue) && EqualityComparer<LineType>.Default.Equals(lineInfo.type, lineValue))
            {
                _curLine = lineInfo;
                OnLineChanged?.Invoke();
                break;
            }
        }

        SetLineColor();
        _curLine.render.DrawLine();
    }

    private void SetLineColor()
    {
        foreach (LineSO lineInfo in lines)
        {
            Color color = GetLineGroupColor(lineInfo.group);

            if (EqualityComparer<LineType>.Default.Equals(lineInfo.type, CurrentLineType))
            {
                color.a = 0.9f;
                lineInfo.render.SetSortOder(-(int)lineInfo.group);
            }
            else
            {
                color.a = Mathf.Clamp(invisibleValue, 0f, 1f);
                lineInfo.render.SetSortOder(-(int)lineInfo.group - Enum.GetValues(typeof(LineGroupType)).Length);
            }

            lineInfo.render.SetColor(color);
            _curLine.render.SetSortOder(1);
        }
    }

    private void ShotRay()
    {
        int usedBridge = 0;
        int i = 0;

        if (_curLine.lineInfo.Count <= 1) return;

        do
        {
            Vector3 direction = _curLine.lineInfo[i + 1].position - _curLine.lineInfo[i].position;
            usedBridge += Physics2D.Raycast(_curLine.lineInfo[i++].position, direction.normalized, direction.magnitude + 1, obstacleLayer) ? 1 : 0;
        } while (i < _curLine.lineInfo.Count - 1);

        _curLine.usedBridgeCount = usedBridge;

        OnBridgeChanged?.Invoke(GetAllBridgeCount());
        Debug.Log("all bridge count : " + GetAllBridgeCount());
    }

    private void OnDestroy()
    {
        LineMouseInput.Instance.OnClickCompany -= HandleClickCompany;
    }

    // 색 얻음
    private Color GetLineGroupColor(LineGroupType type) => type switch
    {
        LineGroupType.Red => Color.red,
        LineGroupType.Green => Color.green,
        LineGroupType.Blue => Color.blue,
        _ => Color.black
    };
}
