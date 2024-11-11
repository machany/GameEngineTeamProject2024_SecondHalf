using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VHierarchy.Libs;
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
    public void RemoveList(int value,GameObject lineobj)
    {
        LineGroupType lineGroupType = lineobj.GetComponent<Line>()._lineGroupType;
        LineType lineType = lineobj.GetComponent<Line>()._lineType;
        
        if (value >= 0 && value < _lineGroup.GetLineType(lineType,lineGroupType).Count)
        {
            for (int i = value; i < _lineGroup.GetLineType(lineType,lineGroupType).Count; i++)
            {
                Destroy(_lineGroup.GetLineType(lineType,lineGroupType)[i]); // 선로삭제
            }
        }
        _lineGroup.GetLineType(lineType,lineGroupType).RemoveRange(value, _lineGroup.GetLineType(lineType,lineGroupType).Count-value);
       
        _lineGroup.GetLinePosType(lineType,lineGroupType)[value].GetComponent<Buil>().Current--; 
        if (value >= 0 && value < _lineGroup.GetLinePosType(lineType,lineGroupType).Count)
        {
            for (int i = value+1; i < _lineGroup.GetLinePosType(lineType,lineGroupType).Count; i++)
            { 
                _lineGroup.GetLinePosType(lineType,lineGroupType)[i].GetComponent<Buil>().Current-=2; 
            }
        }
        value++;
        _lineGroup.GetLinePosType(lineType,lineGroupType).RemoveRange(value, _lineGroup.GetLinePosType(lineType,lineGroupType).Count-value);
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
