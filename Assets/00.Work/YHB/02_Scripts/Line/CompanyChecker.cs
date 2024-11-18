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
        yield return null; // 구독 대기

        Collider2D collision = Physics2D.OverlapCircle(transform.position, radius, buildingLayer);
        if (collision is not null)
            OnBuilding?.Invoke(collision.gameObject);

        yield return null; // 삭제

        OnDestroy?.Invoke(gameObject);
    }
}
