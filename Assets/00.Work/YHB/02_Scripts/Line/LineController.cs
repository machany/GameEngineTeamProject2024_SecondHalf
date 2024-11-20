using System;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public Action OnLineChanged;

    [field: SerializeField] public LineType CurrentLineType { get; private set; }
    [field: SerializeField] public LineGroupType CurrentGroup { get; private set; }

    [SerializeField] private PoolItemSO linerender;
    [SerializeField] private float _invisibleValue = 0.3f;

    private List<LineSO> lines = new List<LineSO>();
    private LineSO _curLine;
    private Transform _currentTrm;

    // Test
    private void Update()
    {
        LineType TLT = CurrentLineType;
        LineGroupType TGT = CurrentGroup;

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

        SetLineType(TLT, TGT); // => 라인 설정시 호출시키면됨.
    }

    public LineType GetLineType() => CurrentLineType;
    public LineGroupType GetGroupType() => CurrentGroup;

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
                _curLine.lineInfo.Remove(companyTrm);

                if (_curLine.lineInfo.Count <= 1)
                    _curLine.lineInfo.Clear();

                goto EndProces;
            }

            _currentTrm = companyTrm;
            return;
        }

        if (_currentTrm is not null && _curLine.lineInfo.Count / 2 >= _curLine.lineInfo.FindValueLocation(_currentTrm))
            _curLine.lineInfo.AddAt(companyTrm, 0);
        else
            _curLine.lineInfo.Add(companyTrm);

        EndProces:
        _currentTrm = null;
        _curLine.render.DrawLine();
    }

    // 라인을 설정
    public void SetLineType(LineType lineValue, LineGroupType groupValue)
    {
        CurrentLineType = lineValue;
        CurrentGroup = groupValue;

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
