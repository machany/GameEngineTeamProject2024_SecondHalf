using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyChecker : MonoBehaviour
{
    public Action<GameObject> OnBuilding;
    public Action<GameObject> OnDestroy;

    [SerializeField] private float radius = 0.1f;
    [SerializeField] private LayerMask buildingLayer;

    private void OnEnable()
    {
        StartCoroutine(DestroyDelay());
    }

    private IEnumerator DestroyDelay()
    {
        yield return null; // ���� ���

        Collider2D collision = Physics2D.OverlapCircle(transform.position, radius, buildingLayer);
        if (collision is not null)
            OnBuilding?.Invoke(collision.gameObject);

        yield return null; // ����

        OnDestroy?.Invoke(gameObject);
    }
}
