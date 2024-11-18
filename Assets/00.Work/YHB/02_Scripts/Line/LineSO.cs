using System.Collections;
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

public class LineSO : ScriptableObject
{
    public LineGroupType group;
    public LineType type;
    public List<Transform> lineInfo = new List<Transform>();
}
