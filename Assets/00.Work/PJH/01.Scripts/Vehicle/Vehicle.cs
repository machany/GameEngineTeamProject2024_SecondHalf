using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public Action<ResourceType, int> OnResourceChanged;

    public Action OnCompanyReached;
    public Action OnCenterReached;

    public Action OnResourceReceive;
    public Action OnResourceSend;

    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxStorageResource;
    [SerializeField] private VehicleSO vehicleSO;

    private ResourceType _storageResourceType;
    private int _storageResource;

    private void OnEnable()
    {
        OnResourceChanged += ResourceChange;
        OnCompanyReached += CompanyReach;
        OnCenterReached += CenterReach;
        //OnResourceReceive += ResourceReceive;
        OnResourceSend += ResourceSend;
        
        Initialize();
    }

    private void OnDisable()
    {
        OnResourceChanged -= ResourceChange;
        OnCompanyReached -= CompanyReach;
        OnCenterReached -= CenterReach;
        //OnResourceReceive -= ResourceReceive;
        OnResourceSend -= ResourceSend;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Company"))
        {
            Company company = collision.GetComponent<Company>();
            OnCompanyReached?.Invoke();
        }
        else if (collision.CompareTag("Center"))
        {
            Center center = collision.GetComponent<Center>();
            OnCenterReached?.Invoke();
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

    private void CompanyReach()
    {
        if (_storageResourceType == ResourceType.None)
            OnResourceReceive?.Invoke();
        else
            OnResourceSend?.Invoke();
    }

    private void CenterReach()
    {
        if (_storageResourceType == ResourceType.None)
            OnResourceReceive?.Invoke();
        else
            OnResourceSend?.Invoke();
    }

    private void ResourceReceive<T>(T building) where T : Building
    {
        if (building is Company company)
        {
        }
    }

    private void ResourceSend()
    {
    }
}