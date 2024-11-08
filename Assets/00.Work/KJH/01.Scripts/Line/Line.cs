using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public float lineWidth {get; private set;}
    
    public LineGroupType _lineGroupType;
    public LineType _lineType;
    
    private void Awake()
    {
        lineWidth = 0.5f;
    }

    public void LineWidthReset()
    {
        lineWidth = 0.5f;
        transform.localScale = new Vector2(transform.localScale.magnitude,lineWidth);
        Debug.Log(1);
    }

    public void LineWidthUp()
    {
        lineWidth = 0.8f;
        transform.localScale = new Vector2(transform.localScale.magnitude,lineWidth);
        Debug.Log(1);

    }
}
