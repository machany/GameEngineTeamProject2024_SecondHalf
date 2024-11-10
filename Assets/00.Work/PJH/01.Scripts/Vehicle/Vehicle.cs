using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public Action<ResourceType, int> OnResourceChanged;

    public Action<Company> OnCompanyReached;
    public Action<DistributionCenter> OnCenterReached;

    public Action<Building> OnResourceReceive;
    public Action<Building> OnResourceSend;

    [SerializeField] private VehicleSO vehicleSO;
    
    private float moveSpeed;
    private int maxStorageResource;

    private ResourceType _storageResourceType;
    private int _storageResource;

    private void OnEnable()
    {
        OnResourceChanged += ResourceChange;
        OnCompanyReached += CompanyReach;
        OnCenterReached += CenterReach;
        OnResourceReceive += ResourceReceive;
        OnResourceSend += ResourceSend;

        Initialize();
    }

    private void OnDisable()
    {
        OnResourceChanged -= ResourceChange;
        OnCompanyReached -= CompanyReach;
        OnCenterReached -= CenterReach;
        OnResourceReceive -= ResourceReceive;
        OnResourceSend -= ResourceSend;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Company"))
        {
            Company company = collision.GetComponent<Company>();
            OnCompanyReached?.Invoke(company);
        }
        else if (collision.CompareTag("Center"))
        {
            DistributionCenter center = collision.GetComponent<DistributionCenter>();
            OnCenterReached?.Invoke(center);
        }
    }

    private void Initialize()
    {
        moveSpeed = vehicleSO.moveSpeed;
        maxStorageResource = vehicleSO.maxStorageResource;
    }

    private void ResourceChange(ResourceType resourceType, int resource)
    {
        _storageResourceType = resourceType;
        _storageResource = resource;
    }

    private void CompanyReach(Company company)
    {
        if (_storageResourceType == ResourceType.None)
            OnResourceReceive?.Invoke(company);
        else
            OnResourceSend?.Invoke(company);
    }

    private void CenterReach(DistributionCenter center)
    {
        if (_storageResourceType == ResourceType.None)
            OnResourceReceive?.Invoke(center);
        else
            OnResourceSend?.Invoke(center);
    }

    private void ResourceReceive(Building building)
    {
        switch (building)
        {
            case Company company:
                _storageResource = company.ProductCost;
                _storageResourceType = company.GetCompanyResourceType();
                break;

            case DistributionCenter center:
                //if (center.GetCenterResource())
            {
                // 리퀘스트 한 자원의 타입을 알아내서 넣어줘야 함
                //_storageResource = center.Storage[]
                break;
            }
        }
    }

    private void ResourceSend(Building building)
    {
        switch (building)
        {
            case Company company:
                if (company.requestType == _storageResourceType)
                {
                    company.RequestCost = _storageResource;
                    _storageResourceType = ResourceType.None;
                    _storageResource = 0;
                }

                break;

            case DistributionCenter center:
                center.AddCenterResource(_storageResourceType, _storageResource);
                _storageResourceType = ResourceType.None;
                _storageResource = 0;

                break;
        }
    }
}