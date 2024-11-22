using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleStorage : MonoBehaviour, IVehicle
{
    [SerializeField] protected VehicleSO vehicleSO;

    public Action<ResourceType, int> OnResourceChanged;

    public Action<Company> OnCompanyReached;
    public Action<DistributionCenter> OnCenterReached;

    public Action<Building> OnResourceReceive;
    public Action<Building> OnResourceSend;

    private ResourceType _storageResourceType;
    private int _storageResource;
    private int _maxStorageResource;

    protected virtual void OnEnable()
    {
        Initialize();

        OnResourceChanged += ResourceChange;
        OnCompanyReached += CompanyReach;
        OnCenterReached += CenterReach;
        OnResourceReceive += ResourceReceive;
        OnResourceSend += ResourceSend;
    }

    protected virtual void OnDisable()
    {
        OnResourceChanged -= ResourceChange;
        OnCompanyReached -= CompanyReach;
        OnCenterReached -= CenterReach;
        OnResourceReceive -= ResourceReceive;
        OnResourceSend -= ResourceSend;
    }

    protected void ArriveBuilding(Transform buildingTrm)
    {
        if (buildingTrm.TryGetComponent<Company>(out Company company))
        {
            Debug.Log("회사");
            OnCompanyReached?.Invoke(company);
        }
        else if (buildingTrm.TryGetComponent<DistributionCenter>(out DistributionCenter center))
        {
            Debug.Log("센터");
            OnCenterReached?.Invoke(center);
        }
    }
    
    protected virtual void Initialize()
    {
        _maxStorageResource = vehicleSO.maxStorageResource;
        // 저장소 초기화시켜라 ===============================================================================================================================================================================================
    }

    public void ResourceChange(ResourceType resourceType, int resource)
    {
        _storageResourceType = resourceType;
        _storageResource = resource;
    }

    public void CompanyReach(Company company)
    {
        if (_storageResourceType == ResourceType.None)
            OnResourceReceive?.Invoke(company);
        else
            OnResourceSend?.Invoke(company);
    }

    public void CenterReach(DistributionCenter center)
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