using System;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoSingleton<LineController>
{
    public Action OnLineChanged;
    // 현재 라인이 감지한 모든 강의 갯수
    public Action<int> OnBridgeChanged;
    public Action<LineType> OnLineTypeChanged;
    // 현재 라인
    public Action<LineSO> OnLineInfoChanged;
    
    public LineType CurrentLineType { get; private set; }
    public LineGroupType CurrentGroupType { get; private set; }

    [Header("Obstacle")]
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Line")]
    [SerializeField] private int maxLineCount = 1;
    [SerializeField] private PoolItemSO linerender;
    public float invisibleValue = 0.3f;

    private LineSO _curLine;
    private Transform _currentTrm;
    [HideInInspector] public List<LineSO> lines = new List<LineSO>();

    [Header("Vehicle")]
    [SerializeField] private PoolItemSO vehile;
    [SerializeField] private VehicleSO car, truck, trailer;
    private VehicleSO _curVehicle;
    private bool _dropMode;

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

        maxLineCount = Mathf.Clamp(maxLineCount, 1, Enum.GetValues(typeof(LineGroupType)).Length);

        foreach (LineType type in Enum.GetValues(typeof(LineType)))
        {
            int count = 0;
            foreach (LineGroupType group in Enum.GetValues(typeof(LineGroupType)))
            {
                LineSO line = new LineSO();
                line.type = type;
                line.group = group;
                line.render = PoolManager.Instance.Pop(linerender).GetComponent<LineRender>();
                line.render.Initialize(line);
                line.usedBridgeCount = 0;
                lines.Add(line);

                if (count++ >= maxLineCount)
                    break;
            }
        }

        _curLine = lines[0];
        SetLineColor();

        Enable();
    }

    #region UIConnection

    private void Enable()
    {
        // line 관련
        LineUI.OnToggleLineEvent += HandleToggleLineEvent;
        LineUI.OnRedLineEvent += HandleRedLineEvent;
        LineUI.OnGreenLineEvent += HandleGreenLineEvent;
        LineUI.OnYellowLineEvent += HandleYellowLineEvent;
        LineUI.OnBlueLineEvent += HandleBlueLineEvent;
        LineUI.OnPurpleLineEvent += HandlePurpleLineEvent;

        // vehicle 관련
        VehicleUI.OnCarSelected += HandleCarEvent;
        VehicleUI.OnTruckSelected += HandleTruckEvent;
        VehicleUI.OnTrailerSelected += HandleTrailerEvent;
    }

    private void HandleToggleLineEvent()
        => HandleChangedLineType(CurrentLineType == LineType.Input ? LineType.Output : LineType.Input, CurrentGroupType);

    private void HandleRedLineEvent()
        => HandleChangedLineType(CurrentLineType, LineGroupType.Red);

    private void HandleBlueLineEvent()
        => HandleChangedLineType(CurrentLineType, LineGroupType.Blue);

    private void HandleGreenLineEvent()
        => HandleChangedLineType(CurrentLineType, LineGroupType.Green);

    private void HandleYellowLineEvent()
        => HandleChangedLineType(CurrentLineType, LineGroupType.Yellow);

    private void HandlePurpleLineEvent()
        => HandleChangedLineType(CurrentLineType, LineGroupType.Purple);

    private void HandleCarEvent(bool selected)
    {
        _dropMode = true;

        if (EqualityComparer<VehicleSO>.Default.Equals(_curVehicle, car))
            DropVehicle();

        _curVehicle = car;
    }

    private void HandleTruckEvent(bool selected)
    {
        _dropMode = true;

        if (EqualityComparer<VehicleSO>.Default.Equals(_curVehicle, truck))
            DropVehicle();

        _curVehicle = truck;
    }

    private void HandleTrailerEvent(bool selected)
    {
        _dropMode = true;

        if (EqualityComparer<VehicleSO>.Default.Equals(_curVehicle, trailer))
            DropVehicle();

        _curVehicle = trailer;
    }

    private void DropVehicle(int index = 0)
    {
        Vehicle vehicle = PoolManager.Instance.Pop(vehile).GetComponent<Vehicle>();
        vehicle.GetComponent<VehicleStorage>().vehicleSO = _curVehicle;
        vehicle.SetLine(CurrentLineType, CurrentGroupType, index);
        _curVehicle = null;
        _dropMode = false;
        _currentTrm = null;
    }

    private void Disable()
    {
        // line 관련
        LineUI.OnToggleLineEvent -= HandleToggleLineEvent;
        LineUI.OnRedLineEvent -= HandleRedLineEvent;
        LineUI.OnGreenLineEvent -= HandleGreenLineEvent;
        LineUI.OnYellowLineEvent -= HandleYellowLineEvent;
        LineUI.OnBlueLineEvent -= HandleBlueLineEvent;
        LineUI.OnPurpleLineEvent -= HandlePurpleLineEvent;

        // vehicle 관련
        VehicleUI.OnCarSelected -= HandleCarEvent;
        VehicleUI.OnTruckSelected -= HandleTruckEvent;
        VehicleUI.OnTrailerSelected -= HandleTrailerEvent;
    }

    #endregion

    private void HandleChangedLineType(LineType lineType, LineGroupType lineGroupType)
    {
        CurrentLineType = lineType;
        CurrentGroupType = lineGroupType;

        SetLineType(lineType, lineGroupType);
        OnLineTypeChanged?.Invoke(CurrentLineType);

        ShotRay();
    }

    // 회사 클릭시
    private void HandleClickCompany(Transform companyTrm)
    {
        if (_dropMode)
        {
            Debug.Log("회사 위치에 드롭");
            DropVehicle(_curLine.lineInfo.FindValueLocation(companyTrm) - 1);
            _dropMode = false;
            return;
        }

        if (_curLine.lineInfo.Contains(companyTrm))
        {
            if (_currentTrm is not null && EqualityComparer<Transform>.Default.Equals(companyTrm, _currentTrm))
            {
                int removeBefore = _curLine.lineInfo.FindValueLocation(companyTrm);

                _curLine.lineInfo.Remove(companyTrm);

                if (_curLine.lineInfo.Count <= 1)
                {
                    _curLine.lineInfo.Clear();
                    OnBridgeChanged?.Invoke(GetAllBridgeCount());

                    goto ClearSkip;
                }

                _currentTrm = null;

            ClearSkip:
                OnLineInfoChanged?.Invoke(_curLine);
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

        _currentTrm = companyTrm;
        OnLineInfoChanged?.Invoke(_curLine);

    EndProces:
        ShotRay();
        _curLine.render.DrawLine();
    }

    // 라인을 설정
    public void SetLineType(LineType lineValue, LineGroupType groupValue)
    {
        Debug.Log(lineValue + " /lv/gv " + groupValue);

        CurrentLineType = lineValue;
        CurrentGroupType = groupValue;

        foreach (LineSO lineInfo in lines)
        {
            if (EqualityComparer<LineGroupType>.Default.Equals(lineInfo.group, groupValue) && EqualityComparer<LineType>.Default.Equals(lineInfo.type, lineValue))
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
    }

    private void OnDestroy()
    {
        LineMouseInput.Instance.OnClickCompany -= HandleClickCompany;
        Disable();
    }

    // 색 얻음
    private Color GetLineGroupColor(LineGroupType type) => type switch
    {
        LineGroupType.Red => Color.red,
        LineGroupType.Green => Color.green,
        LineGroupType.Blue => Color.blue,
        LineGroupType.Yellow => Color.yellow,
        LineGroupType.Purple => Color.magenta,
        _ => Color.black
    };
}