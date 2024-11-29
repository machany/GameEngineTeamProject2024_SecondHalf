using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyDragChecker : MonoBehaviour
{
    public Action<GameObject> OnBuilding;

    [SerializeField] private float radius = 0.1f;
    [SerializeField] private float equalsBuildingCoolTime = 1f;
    [SerializeField] private LayerMask buildingLayer;

    private bool _equalsBuildingCool;
    private GameObject _building;

    public void SetMouseClick(Vector3 position)
    {
        LineController.Instance.ResetCurTrm();
        StopAllCoroutines();
        _equalsBuildingCool = false;
        
        Collider2D collision = Physics2D.OverlapCircle(position, radius, buildingLayer);
        if (collision is not null)
            CheckBuilding(collision.gameObject);
    }

    public void CheckBuildingDrag(Vector3 position)
    {
        Collider2D collision = Physics2D.OverlapCircle(position, radius, buildingLayer);
        if (collision is not null)
        {
            CheckBuilding(collision.gameObject);
        }
    }

    private void CheckBuilding(GameObject collision)
    {
        if (EqualityComparer<GameObject>.Default.Equals(_building, collision))
        {
            if (!_equalsBuildingCool)
            {
                StartCoroutine(BuildingCoolTime(collision));
                OnBuilding?.Invoke(collision);
            }
        }
        else
        {
            StartCoroutine(BuildingCoolTime(collision));
            OnBuilding?.Invoke(collision);
        }
    }

    private IEnumerator BuildingCoolTime(GameObject collision)
    {
        _equalsBuildingCool = true;
        _building = collision;
        yield return new WaitForSeconds(equalsBuildingCoolTime);
        _equalsBuildingCool = false;
    }
}