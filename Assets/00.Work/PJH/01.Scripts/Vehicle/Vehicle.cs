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
    }

    private void OnDisable()
    {
        OnResourceChanged -= ResourceChange;
        OnCompanyReached -= CompanyReach;
        OnCenterReached -= CenterReach;
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
}