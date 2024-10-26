using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackConnection : MonoBehaviour
{
    private LineRenderer lr;

    public GameObject Starttarget;
    public GameObject Endtarget;
    
    private void Start()
    {
        lr = GetComponent<LineRenderer>();

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Line(Starttarget.transform,Endtarget.transform);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DisconnectBuildings();
        }
    }

    //두개의 Transform값을 가져와서 선을 연결하는 메쏘드
    public void Line(Transform start, Transform end)
    {
        lr.enabled = true;

        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        
        lr.SetPosition(0, start.transform.position);
        lr.SetPosition(1, end.transform.position);
    }
    
    //연결한 선을 비활성화하는 메쏘드
    public void DisconnectBuildings()
    {
        if (lr != null)
        {
            lr.enabled = false;
        }
    }
}
