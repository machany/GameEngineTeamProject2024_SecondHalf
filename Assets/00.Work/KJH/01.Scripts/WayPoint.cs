using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField]
    private Vector3[] _points;

    public Vector3[] Points
    {
        get { return _points; }
        private set { _points = value; }
    }

    public Vector3 CurrentPos { get { return _currentPos; } }
    public bool GameStarted { get { return _gameStarted; } }

    private Vector3 _currentPos;
    private bool _gameStarted;

    private void Start()
    {
        _currentPos = transform.position;
        _gameStarted = true;
    }

    private void OnDrawGizmos()
    {
        if (!GameStarted && transform.hasChanged)
        {
            _currentPos = transform.position;
        }

        if (Points == null)
            return;

        for (int i = 0; i < Points.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Points[i] + CurrentPos, 0.5f);
            if (i < Points.Length - 1)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(Points[i] + CurrentPos, Points[i + 1] + CurrentPos);
            }
        }
    }

    public Vector3 GetWaypointPosition(int index)
    {
        return Points[index] + _currentPos;
    }
}
