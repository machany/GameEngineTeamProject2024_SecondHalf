using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CarMovement : MonoBehaviour
{
    public LineGroup _lineGroup; // LineGroup 인스턴스를 할당
    public LineGroupType _lineGroupType; // 현재 라인 그룹 타입
    public LineType _LineType; // 현재 라인 그룹 타입
    public float speed = 5f; // 차량 속도

    private List<Transform> _waypoints; // 웨이포인트 리스트
    private int _currentWaypointIndex = 0; // 현재 웨이포인트 인덱스
    private bool _movingToEnd = true; // 이동 방향을 추적하는 변수

    private void Start()
    {
        UpdateWaypoints(); // 웨이포인트 업데이트
    }

    private void Update()
    {
        MoveAlongLine(); // 라인을 따라 이동
    }

    private void UpdateWaypoints()
    {
        _waypoints = _lineGroup.GetLinePosType(_LineType, _lineGroupType);
        
        // 현재 웨이포인트가 범위를 벗어났는지 확인
        if (_waypoints.Count == 0)
        {
            _currentWaypointIndex = 0; // 유효하지 않은 인덱스를 방지
            return; // 업데이트 중지
        }
        if (_currentWaypointIndex >= _waypoints.Count)
        {
            _currentWaypointIndex = 0; // 처음으로 돌아감
        }
    }

    private void MoveAlongLine()
    {
        // 웨이포인트가 없으면 이동하지 않음
        if (_waypoints.Count == 0) return;

        // 현재 웨이포인트까지의 거리 계산
        float step = speed * Time.deltaTime;

        // 현재 위치에서 웨이포인트 방향으로 이동
        Vector3 targetPosition = _waypoints[_currentWaypointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // 현재 웨이포인트에 도달했는지 확인
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (_movingToEnd)
            {
                _currentWaypointIndex++; // 다음 웨이포인트로 이동
                // 끝점에 도달하면 방향을 반전
                if (_currentWaypointIndex >= _waypoints.Count)
                {
                    _currentWaypointIndex = _waypoints.Count - 1; // 끝점으로 설정
                    _movingToEnd = false; // 방향 전환
                }
            }
            else
            {
                _currentWaypointIndex--; // 이전 웨이포인트로 이동
                // 시작점에 도달하면 방향을 반전
                if (_currentWaypointIndex < 0)
                {
                    _currentWaypointIndex = 0; // 시작점으로 설정
                    _movingToEnd = true; // 방향 전환
                }
            }
        }
    }
}
