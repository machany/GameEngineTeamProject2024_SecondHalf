using System;
using System.Collections.Generic;
using UnityEngine;

// 선로 색
public enum LineGroupType
{
    Red,
    Blue,
    Green
}

// 선로 타입 구분
public enum LineType
{
    Input,
    Output
}

//임시 코드 지워질 예정↓
[Serializable]
public class LineGroupPosition
{
    public LineGroupType lineGroupType; // Key
    public List<Transform> positions;  // Value
}
public class LineGroup : MonoBehaviour
{
    [SerializeField] private List<LineGroupPosition> inlinePositions = new();
    [SerializeField] private List<LineGroupPosition> outlinePositions = new();

    private Dictionary<LineGroupType, List<Transform>> _inlinePos = new();
    private Dictionary<LineGroupType, List<Transform>> _outlinePos = new();

    public List<Transform> inputRedPos = new();
    public List<Transform> inputBluePos = new(); 
    public List<Transform> inputGreenPos = new();

    public List<Transform> outputRedPos = new();
    public List<Transform> outputBluePos = new();
    public List<Transform> outputGreenPos = new();
    
    public List<GameObject> inputRedObjects = new();
    public List<GameObject> inputBlueObjects = new(); 
    public List<GameObject> inputGreenObjects = new();

    public List<GameObject> outputRedObjects = new();
    public List<GameObject> outputBlueObjects = new();
    public List<GameObject> outputGreenObjects = new();

    public GameObject inputRed, inputBlue, inputGreen;
    public GameObject outputRed, outputBlue, outputGreen;
    
    /// <summary> 선로의 색을 그룹에 따라 지정해주는 메서드 </summary>
    /// <param name="lineType"> 선로 타입 </param>
    /// <param name="lineGroupType"> 선로 색 </param>
    public GameObject GetLineGroupType(LineType lineType, LineGroupType lineGroupType) => lineGroupType switch
    {
        LineGroupType.Red when lineType == LineType.Input => inputRed,
        LineGroupType.Red => outputRed,
        LineGroupType.Blue when lineType == LineType.Input => inputBlue,
        LineGroupType.Blue => outputBlue,
        LineGroupType.Green when lineType == LineType.Input => inputGreen,
        LineGroupType.Green => outputGreen,
        _ => throw new Exception("wow, you seem like 57")
    };

    /// <summary> 운송수단이 이동해야 할 위치 값을 지정해주는 메서드 </summary>
    /// <param name="lineType"> 선로 타입 </param>
    /// <param name="lineGroupType"> 선로 색 </param>
    public List<Transform> GetLinePosType(LineType lineType, LineGroupType lineGroupType) => lineGroupType switch
    {
        LineGroupType.Red when lineType == LineType.Input => inputRedPos,
        LineGroupType.Red => inputBluePos,
        LineGroupType.Blue when lineType == LineType.Input => inputGreenPos,
        LineGroupType.Blue => outputRedPos,
        LineGroupType.Green when lineType == LineType.Input => outputBluePos,
        LineGroupType.Green => outputGreenPos,
        _ => throw new Exception("wow, you seem like 5.7")
    };
    
    /// <summary> 현재 연결된 선로의 그룹을 지정해주는 메서드 </summary>
    /// <param name="lineType"> 선로 타입 </param>
    /// <param name="lineGroupType"> 선로 색 </param>
    public List<GameObject> GetLineType(LineType lineType, LineGroupType lineGroupType) => lineGroupType switch
    {
        LineGroupType.Red when lineType == LineType.Input => inputRedObjects,
        LineGroupType.Red => outputRedObjects,
        LineGroupType.Blue when lineType == LineType.Input => inputBlueObjects,
        LineGroupType.Blue => outputBlueObjects,
        LineGroupType.Green when lineType == LineType.Input => inputGreenObjects,
        LineGroupType.Green => outputGreenObjects,
        _ => throw new Exception("wow, you seem like 570")
    };
}