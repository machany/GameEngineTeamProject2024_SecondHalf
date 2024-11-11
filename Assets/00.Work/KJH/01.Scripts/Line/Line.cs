using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public float lineWidth {get; private set;}
    
    public LineGroupType _lineGroupType;
    public LineType _lineType;
    
    
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

    public void LineSetAlpha( bool alpha)
    {
        Color currentColor = _spriteRenderer.color;
        if (alpha)
        {
            _spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
            Debug.Log(0);

        }
        else
        {
            _spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);

            Debug.Log(2);
        }
    }
}
