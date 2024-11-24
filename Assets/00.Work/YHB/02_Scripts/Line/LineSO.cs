using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��
public enum LineGroupType
{
    Red,
    Yellow,
    Green,
    Blue,
    Purple
}

// ���� Ÿ�� ����
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

    public LineRender render;

    public int usedBridgeCount;
}
