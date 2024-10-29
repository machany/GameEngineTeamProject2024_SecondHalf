using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WayPoint))]

public class WayPointEditor : Editor
{
    WayPoint WayPoint => target as WayPoint;

    private void OnSceneGUI()
    {
        EditorGUI.BeginChangeCheck();

        for (int i = 0; i < WayPoint.Points.Length; i++)
        {
            Handles.color = Color.black;

            Vector3 currentWaypoint = WayPoint.Points[i] + WayPoint.CurrentPos;
            Vector3 newWaypoint = Handles.FreeMoveHandle(currentWaypoint, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.yellow;

            Vector3 textAlign = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(currentWaypoint + textAlign, $"{i + 1}", textStyle);

            if (EditorGUI.EndChangeCheck()) 
            {
                Undo.RecordObject(target, "Free Move Handle"); 
                WayPoint.Points[i] = newWaypoint - WayPoint.CurrentPos;
                EditorUtility.SetDirty(WayPoint);
            }
        }
    }
}