using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CompanySpawn : MonoBehaviour
{
    [SerializeField] private LayerMask _companyLayer;
    
    [SerializeField] private GameObject _companyPrefab;
    [SerializeField] private float _companyOverlapSize;

    [SerializeField] private float _spawnTime;
    public Vector2 _spawnAreaSize;
    [SerializeField] private float maxCompanyCount;
    
    [SerializeField] List<GameObject> _spawnedCompanies = new List<GameObject>();
    
    private float _lastSpawnTime = 0f;
    private int _companyCount = 0;

    private GameObject companyGroup;


    private void Awake()
    {
        companyGroup = new GameObject("CompanyGroup");
    }
    
    private void Update()
    {
        if (maxCompanyCount > _companyCount && _lastSpawnTime + _spawnTime < Time.time)
        {
            InstantiateCompany();
        }
    }

    private void InstantiateCompany()
    {
        Vector2 spawnPosition;

        int i = 0;
        
        do
        {
            spawnPosition = new Vector2(Random.Range(-_spawnAreaSize.x / 2, _spawnAreaSize.x / 2),
                Random.Range(-_spawnAreaSize.y / 2, _spawnAreaSize.y / 2));
            
            if(++i>=500) return;

        } while (Physics2D.OverlapCircle(spawnPosition, _companyOverlapSize, _companyLayer));
        
        GameObject newCompany=Instantiate(_companyPrefab, spawnPosition, Quaternion.identity, companyGroup.transform);
        _spawnedCompanies.Add(newCompany);
        
        ++_companyCount;
        _lastSpawnTime = Time.time;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _spawnAreaSize);
        for (int i = 0; i < _spawnedCompanies.Count; i++)
        {
            Gizmos.DrawWireSphere(_spawnedCompanies[i].transform.position, _companyOverlapSize);   
        }
        Gizmos.color = Color.white;
    }
}
