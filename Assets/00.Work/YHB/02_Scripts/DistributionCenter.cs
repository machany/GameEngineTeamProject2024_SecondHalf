using System;
using System.Collections.Generic;

public class DistributionCenter : Building
{
    private Dictionary<ResourceType, int> _storage = new Dictionary<ResourceType, int>();
        
    private void OnEnable()
    {
        Initialize();
    }

    /// <summary>초기화 함수 입니다.</summary>
    private void Initialize()
    {
        buildingType = BuildingType.Center;
        StorageInit();
    }

    /// <summary> 창고 초기화 </summary>
    private void StorageInit()
    {
        _storage.Clear();
        foreach (ResourceType item in Enum.GetValues(typeof(ResourceType)))
            _storage.Add(item, 0);
    }

    private void InputResource(ResourceType resource)
    {
        _storage[resource]++;
    }
    
    private bool OutputResource(ResourceType resource)
    {
        if (--_storage[resource] < 0)
        {
            _storage[resource] = 0;
            return false;
        }
        
        return true;
    }
}
