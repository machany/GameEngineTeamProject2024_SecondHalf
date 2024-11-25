using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyRandomSpawn : MonoBehaviour
{
    [Header("Company")]
    [SerializeField] private PoolItemSO company;

    [Header("Obstacle")]
    [SerializeField] private LayerMask obstacle;
    [SerializeField] private float obstractRadius = 1f;

    [Header("Overlap")]
    [SerializeField] private LayerMask notOverlapObjectLayer;
    [SerializeField] private float overlapRadius = 1f;
    
    private Vector2 spawnRange = Vector2.zero;

    private void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        spawnRange = CameraControl.Instance.worldBounds;
    }

    private void SpawnCompany()
    {
        Vector3 targetPos;
        do
        {
            targetPos = new Vector2(Random.Range(-spawnRange.x / 2, spawnRange.x / 2), Random.Range(-spawnRange.y / 2, spawnRange.y / 2));
            Physics2D.OverlapCircle(targetPos, obstractRadius + overlapRadius, notOverlapObjectLayer);
        } while (true);
    }
}
