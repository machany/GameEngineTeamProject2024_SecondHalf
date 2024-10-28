using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public static Action<Car> OnEnemyReached;

    [SerializeField] private float moveSpeed = 3.0f;
    public int _currentWaypointIndex;
    
    public WayPoint WayPoint { get; set;}

    public float MoveSpeed { get; set; }

    public Vector3 CurrentPos => WayPoint.GetWaypointPosition(_currentWaypointIndex);

    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        MoveSpeed = moveSpeed;
        _currentWaypointIndex = 0;
        
        if (WayPoint == null)
        {
            WayPoint = FindObjectOfType<WayPoint>();
        }
    }

    private void Update()
    {
        
        Move();
        if(CurrentPointReached())
        {
            UpdatePointIndex(); 
        }
        Ming();
    }
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPos, MoveSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        MoveSpeed = 0f;
    }

    public void ResumeMovement()
    {
        MoveSpeed = moveSpeed;
    }

    private bool CurrentPointReached()
    {
        float disToNextPos = (transform.position - CurrentPos).magnitude;
        if (disToNextPos < 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdatePointIndex()
    {
        int lastIndex = WayPoint.Points.Length - 1;
        if(_currentWaypointIndex < lastIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            //�������� ����
            //Ǯ���� �ǵ���
            EndPointReached(); 
        }
    }

    private void EndPointReached()
    {
        OnEnemyReached?.Invoke(this); 
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }

    private void Ming()
    {
        if (CurrentPos.x > transform.position.x)
            _spriteRenderer.flipX = false;
        else
            _spriteRenderer.flipX = true;  
    }
}
