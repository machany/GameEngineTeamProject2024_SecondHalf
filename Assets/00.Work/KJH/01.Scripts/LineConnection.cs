using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnection : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] private LineGroup _line;
    [SerializeField] private LineGroupType _lineGroupType;
    [SerializeField] private LineType _lineType;

    [SerializeField] private GameObject m;
    
    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;
    
    private void Awake()
    {
        _line = GetComponent<LineGroup>();
        m.SetActive(false);
    }
    public List<Transform> _pos = new List<Transform>();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Line(a.transform,b.transform);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Line(c.transform,d.transform);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m.SetActive(true);
        }
    }

    public void Line(Transform startPoint, Transform endPoint)
    {
        _pos.Add(startPoint);
        _pos.Add(endPoint);
        GameObject line = Instantiate(_line.GetLineGroupType(_lineType,_lineGroupType), (startPoint.position + endPoint.position) / 2, Quaternion.identity);
        Vector2 dir = endPoint.position - startPoint.position;
        line.transform.localScale = new Vector2(dir.x, 0.5f);
        line.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }
}
