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

    private bool equalsBuildingCool;
    private GameObject building;

    public void SetMouseClick()
    {
        StopAllCoroutines();
        equalsBuildingCool = false;
    }

    public void CheckBuilding(Vector3 position)
    {
        Collider2D collision = Physics2D.OverlapCircle(position, radius, buildingLayer);
        if (collision is not null)
        {
            if (EqualityComparer<GameObject>.Default.Equals(building, collision.gameObject))
            {
                if (!equalsBuildingCool)
                {
                    StartCoroutine(BuildingCoolTime(collision.gameObject));
                    OnBuilding?.Invoke(collision.gameObject);
                }
            }
            else
            {
                StartCoroutine(BuildingCoolTime(collision.gameObject));
                OnBuilding?.Invoke(collision.gameObject);
            }
        }
    }

    private IEnumerator BuildingCoolTime(GameObject collision)
    {
        equalsBuildingCool = true;
        building = collision;
        yield return new WaitForSeconds(equalsBuildingCoolTime);
        equalsBuildingCool = false;
    }
}
