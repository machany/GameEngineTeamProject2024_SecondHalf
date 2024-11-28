using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LineManager : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] private LineGroup _lineGroup;
    [SerializeField] private LineGroupType _lineGroupType;
    [SerializeField] private LineType _lineType;
    
    public float LineWidth { get; private set; }

    private readonly List<(Transform, Transform)> _connections = new();
    public Transform CenterPoint; // 중심(MainCenter) 포인트 추가

    private void Awake()
    {
        LineWidth = 0.5f;
        LineSetting();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _lineGroupType = LineGroupType.Red;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _lineGroupType = LineGroupType.Blue;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _lineGroupType = LineGroupType.Green;
        }
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            _lineType = LineType.Input;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            _lineType = LineType.Output;
        }
    }

    public void LineSetting()
    {
        _lineGroup.GetLinePosType(LineType.Input, LineGroupType.Red).Add(CenterPoint);
        _lineGroup.GetLinePosType(LineType.Input, LineGroupType.Blue).Add(CenterPoint);
        _lineGroup.GetLinePosType(LineType.Input, LineGroupType.Green).Add(CenterPoint);
        _lineGroup.GetLinePosType(LineType.Output, LineGroupType.Red).Add(CenterPoint);
        _lineGroup.GetLinePosType(LineType.Output, LineGroupType.Blue).Add(CenterPoint);
        _lineGroup.GetLinePosType(LineType.Output, LineGroupType.Green).Add(CenterPoint);
    }
    
    
    
    //라인연결조건검사 메쏘드
    public void CheckLineConnectionConditions(Transform startPoint, Transform endPoint)
    {
        if (startPoint == CenterPoint || endPoint == CenterPoint)
        {
            // 중심(MainCenter)과 연결되는 경우 중복 확인 없이 바로 라인 생성
            LineConnection(startPoint, endPoint);
        }
        else
        {
            // 중심(MainCenter)이 아닐 경우 중복 연결 확인
            if (!LineDuplicationTest(startPoint, endPoint))
            {
                LineConnection(startPoint, endPoint); 
            }
        }
    }

    //라인 연결 메쏘드
    private void LineConnection(Transform startPoint, Transform endPoint)
    {
        GameObject line = Instantiate(_lineGroup.GetLineGroupType(_lineType, _lineGroupType), (startPoint.position + endPoint.position) / 2, Quaternion.identity);
        _lineGroup.GetLineType(_lineType, _lineGroupType).Add(line);
        Vector2 dir = endPoint.position - startPoint.position;
        line.transform.localScale = new Vector2(dir.magnitude, LineWidth);
        line.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }
    
    //라인 중복 검사 메쏘드
    public bool LineDuplicationTest(Transform startPoint, Transform endPoint)
    {
        //return _lineGroup.GetLinePosType(_lineType, _lineGroupType).Contains(startPoint) ||
        //    _lineGroup.GetLinePosType(_lineType, _lineGroupType).Contains(endPoint);
        return _connections.Contains((startPoint, endPoint)) || _connections.Contains((endPoint, startPoint));
    }

    //선로 삭제 메쏘드
    public void RemoveList(int index, GameObject lineObj1)
    {
        var posList = _lineGroup.GetLinePosType(_lineType, _lineGroupType);
        var lineList = _lineGroup.GetLineType(_lineType, _lineGroupType);
        
        GameObject lineObj2 = (index + 1 < lineList.Count) ? lineList[index + 1] : null;
        
        int currentIndex = index; 
        int nextIndex = index + 2;   

        if (currentIndex >= 0 && nextIndex < posList.Count)
        {
            LineConnection(posList[currentIndex], posList[nextIndex]);
        }
        if (lineObj1 != null)
        {
            lineList.Remove(lineObj1);
            Destroy(lineObj1);
        }
        if (lineObj2 != null)
        {
            lineList.Remove(lineObj2);
            Destroy(lineObj2);
        }

        Debug.Log($"위치 {currentIndex}와 {nextIndex} 사이의 선을 제거gka, 다시연결함.");
    }



    
    public void LineReSetAlpha(LineGroupType lineGroupType, LineType lineType)
    {
        // 선택된 라인 타입에 해당하는 라인만 true로 설정
        foreach (var item in _lineGroup.GetLineType(lineType, lineGroupType))
        {
            item.GetComponent<Line>().LineSetAlpha(true);
        }

        // 나머지 라인들 false로 설정
        foreach (var groupType in Enum.GetValues(typeof(LineGroupType)).Cast<LineGroupType>())
        {
            foreach (var type in Enum.GetValues(typeof(LineType)).Cast<LineType>())
            {
                // 현재 선택된 타입은 건너뜀
                if (groupType == lineGroupType && type == lineType) continue;

                // 나머지 타입의 라인들을 false로 설정
                foreach (var item in _lineGroup.GetLineType(type, groupType))
                {
                    item.GetComponent<Line>().LineSetAlpha(true);
                }
            }
        }
    }

    public void LineSetAlpha(LineGroupType lineGroupType, LineType lineType)
    {
        foreach (var item in _lineGroup.GetLineType(lineType, lineGroupType))
        {
            item.GetComponent<Line>().LineSetAlpha(true);
        }

        // 나머지 라인들 false로 설정
        foreach (var groupType in Enum.GetValues(typeof(LineGroupType)).Cast<LineGroupType>())
        {
            foreach (var type in Enum.GetValues(typeof(LineType)).Cast<LineType>())
            {
                // 현재 선택된 타입은 건너뜀
                if (groupType == lineGroupType && type == lineType) continue;

                // 나머지 타입의 라인들을 false로 설정
                foreach (var item in _lineGroup.GetLineType(type, groupType))
                {
                    item.GetComponent<Line>().LineSetAlpha(false);
                }
            }
        }
    }
}
