 using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;


 //선로의 색깔
public enum LineGroupType
{
    Red,
    Blue,
    Green
}
 
 //선로가 들오는 것인지 나가는 것 인지 구분하는거
public enum LineType
{
    Input,
    Output
}
public class LineGroup : MonoBehaviour
{

    
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
    public List<GameObject> outputBlueObjects= new();
    public List<GameObject> outputGreenObjects = new();
    
    public GameObject ir, ib, ig, or, ob, og;

    //선로의 색깔을 그룹에 따라 지정해주는 메쏘드
    public GameObject GetLineGroupType(LineType lineType, LineGroupType lineGroupType) => lineGroupType switch
    {
        LineGroupType.Red when lineType == LineType.Input => ir,
        LineGroupType.Red => or,
        LineGroupType.Blue when lineType == LineType.Input => ib,
        LineGroupType.Blue => ob,
        LineGroupType.Green when lineType == LineType.Input => ig,
        LineGroupType.Green => og,
        _ => throw new Exception("wow, you seem like 57")
    };

    //선로를 따라 이동할 운송수단을 위한 운송수단이 이동해야할 위치값을 지정해주는 메쏘드
    public List<Transform> GetLinePosType(LineType lineType, LineGroupType lineGroupType) => lineGroupType switch
    {
        LineGroupType.Red when lineType == LineType.Input => inputRedPos,
        LineGroupType.Red => outputRedPos,
        LineGroupType.Blue when lineType == LineType.Input => inputBluePos,
        LineGroupType.Blue => outputBluePos,
        LineGroupType.Green when lineType == LineType.Input => inputGreenPos,
        LineGroupType.Green => outputGreenPos,
        _ => throw new Exception("wow, you seem like 5.7")
    };

    //현재 연결된 선로의 그룹을 지정해주는 메쏘드
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




