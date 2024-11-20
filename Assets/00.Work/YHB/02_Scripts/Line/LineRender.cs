using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRender : MonoBehaviour
{
    [SerializeField] private float width = 1f;

    private LineSO _lineInfo;
    private LineRenderer _lineRenderer;

    public void Initialize(LineSO line)
    {
        _lineInfo = line;
        _lineRenderer = transform.GetComponent<LineRenderer>();
        float lineWidth = ((int)line.group + 1) * width / 2;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;
    }

    public void DrawLine()
    {
        _lineRenderer.positionCount = 0;

        _lineRenderer.positionCount = _lineInfo.lineInfo.Count;

        for (int i = 0; i < _lineInfo.lineInfo.Count; i++)
            _lineRenderer.SetPosition(i, _lineInfo.lineInfo[i].position);
    }

    public void SetSortOder(int value = 0)
        => _lineRenderer.sortingOrder = value;

    public void SetColor(Color color)
        => _lineRenderer.SetColors(color, color);
}
