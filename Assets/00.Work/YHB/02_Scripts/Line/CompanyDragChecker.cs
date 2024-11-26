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
    private bool _destroyMod;
    private GameObject _building;

    public void SetMouseClick(Vector3 position)
    {
        LineController.Instance.ResetCurTrm();
        StopAllCoroutines();
        _equalsBuildingCool = false;

        Collider2D collision = Physics2D.OverlapCircle(position, radius, buildingLayer);
        _destroyMod = collision is not null;
    }

    public void CheckBuilding(Vector3 position)
    {
        Collider2D collision = Physics2D.OverlapCircle(position, radius, buildingLayer);
        if (collision is not null)
        {
            if (EqualityComparer<GameObject>.Default.Equals(_building, collision.gameObject)
                && _equalsBuildingCool && !_destroyMod)
                return;

            StartCoroutine(BuildingCoolTime(collision.gameObject));
            OnBuilding?.Invoke(collision.gameObject);
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
