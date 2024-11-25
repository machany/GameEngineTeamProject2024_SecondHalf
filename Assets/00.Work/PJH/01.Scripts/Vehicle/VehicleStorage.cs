using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VehicleStorage : MonoBehaviour
{
    public VehicleSO vehicleSO;

    private Dictionary<ResourceType, int> _storage;
    private int _maxStorageResourceCapacity;
    private int _maxStorageCapacity;
    private int _curStorageCapacity;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Disable();
    }

    private void Initialize()
    {
        _maxStorageCapacity = vehicleSO.maxStorageCapacity;
        _maxStorageResourceCapacity = vehicleSO.maxStorageResourceCapacity;
        _curStorageCapacity = 0;
        _storage = new Dictionary<ResourceType, int>();
    }

    private void Disable()
    {
    }

    public void ArriveBuilding(Transform buildingTrm, LineSO line, int index, int dir)
    {
        // -1등 범위 밖의 index예외 처리
        if (index < 0 || index > line.lineInfo.Count - 1) return;
        if (buildingTrm.TryGetComponent<Company>(out Company company))
        {
            if (EqualityComparer<LineType>.Default.Equals(line.type, LineType.Input))
                ReceiveResource(company);
            else
                SendResource(company);
        }
        else if (buildingTrm.TryGetComponent<DistributionCenter>(out DistributionCenter center))
        {
            SendResource(center);
            if (EqualityComparer<LineType>.Default.Equals(line.type, LineType.Output))
                ReceiveResource(center, line, index, dir);
        }
    }

    // 자원을 보냄 (출품)
    private void SendResource(Company company)
    {
        ResourceType resourceType = company.GetRequestResource();
        if (!_storage.ContainsKey(resourceType)) return;

        _curStorageCapacity -= _storage[resourceType];

        _storage[resourceType] = company.ReceiveRequestResource(_storage[resourceType]);

        _curStorageCapacity += _storage[resourceType];
    }

    // 자원을 받음 (입품)
    private void ReceiveResource(Company company)
    {
        ResourceType resourceType = company.GetProductResourceType();
        // 키가 없고 딕셔너리에 키를 추가하는데에 실패했으면 반환
        if (!_storage.ContainsKey(resourceType) && !_storage.TryAddMaxCount(resourceType, 0, _maxStorageResourceCapacity)) return;

        _curStorageCapacity -= _storage[resourceType];

        // 자원이 현재 수용 가능 용량보다 작으면 전부 가져옴
        int value = company.ProductCost < _maxStorageCapacity - _curStorageCapacity ? company.ProductCost : _maxStorageCapacity - _curStorageCapacity;
        _storage[resourceType] = company.SendProductResource(value);

        _curStorageCapacity += _storage[resourceType];

        // 비어있으면 해당 자원 공간 제거 (위에서 추가를 했지만, 받은게 없는 경우도 있음.)
        if (_storage.Count <= 0)
            _storage.Remove(resourceType);
    }

    // 자원을 물류 센터에 넣음 (입품)
    private void SendResource(DistributionCenter center)
    {
        _storage.Keys.ToList().ForEach(key => { center.Storage[key] += _storage[key]; });
        _storage.Clear();
    }

    // 자원을 물류 센터에서 받음 (출품)
    private void ReceiveResource(DistributionCenter center, LineSO line, int index, int dir)
    {
        if (dir == 1)
        {
            for (int i = index; i < line.lineInfo.Count; i++)
            {
                ReceiveResource(center, line, i);

                if (_storage.Count >= _maxStorageResourceCapacity)
                    break;
            }
        }
        else
        {
            for (int i = index; i >= 0; i--)
            {
                ReceiveResource(center, line, i);

                if (_storage.Count >= _maxStorageResourceCapacity)
                    break;
            }
        }

        center.OnStorageChanged?.Invoke(center.Storage);
    }

    // 자원을 물류 센터에서 받음 (출품)
    private void ReceiveResource(DistributionCenter center, LineSO line, int i)
    {
        if (line.lineInfo[i].transform.TryGetComponent(out Company company))
        {
            ResourceType resource = company.GetRequestResource();
            _storage.TryAddMaxCount(resource, 0, _maxStorageResourceCapacity);

            int requestCost = Mathf.Clamp(company.RequestCost, 0, center.Storage[resource]);

            _storage[resource] += requestCost;
            center.Storage[resource] -= requestCost;

            if (_storage[resource] <= 0)
                _storage.Remove(resource);
        }
    }
}