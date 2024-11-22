using System;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoSingleton<LineController>
{
    public Action OnLineChanged;
    // 현재 라인, 변경이 된 인덱스, 추가로 인한 변경 시 true
    public Action<LineSO, int, bool> OnLineInfoChanged;

    [field: SerializeField] public LineType CurrentLineType { get; private set; }
    [field: SerializeField] public LineGroupType CurrentGroupType { get; private set; }

    [SerializeField] private PoolItemSO linerender;
    [SerializeField] private float _invisibleValue = 0.3f;

    public List<LineSO> lines = new List<LineSO>();

    private LineSO _curLine;
    private Transform _currentTrm;

    // Test
    [SerializeField] PoolItemSO vehile;

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

        SetLineType(TLT, TGT); // => 라인 설정시 호출시키면됨.
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
                lines.Add(line);
            }

        _curLine = lines[0];
        SetLineColor();
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
                    OnLineInfoChanged?.Invoke(_curLine, -1, false);

                    goto ClearSkip;
                }

                OnLineInfoChanged?.Invoke(_curLine, removeBefore, false);

            ClearSkip:
                goto EndProces;
            }

            _currentTrm = companyTrm;
            return;
        }

        if (_currentTrm is not null)
            _curLine.lineInfo.AddAt(companyTrm, _curLine.lineInfo.FindValueLocation(_currentTrm));
        else
            _curLine.lineInfo.Add(companyTrm);

        OnLineInfoChanged?.Invoke(_curLine, _curLine.lineInfo.FindValueLocation(companyTrm), true);

    EndProces:
        _currentTrm = null;
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
                Debug.Log("변경");
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
                color.a = Mathf.Clamp(_invisibleValue, 0f, 1f);
                lineInfo.render.SetSortOder(-(int)lineInfo.group - Enum.GetValues(typeof(LineGroupType)).Length);
            }

            lineInfo.render.SetColor(color);
            _curLine.render.SetSortOder(1);
        }
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
