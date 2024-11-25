using System;
using System.Collections.Generic;
using UnityEngine;

public class DistributionCenter : Building, IInitialize
{
    public Action<Dictionary<ResourceType, int>> OnStorageChanged;

    public static Dictionary<ResourceType, int> Storage;
    [SerializeField] private int earlyStartResource;

    private void OnEnable()
    {
        Initialize();
    }

    /// <summary>초기화 함수 입니다.</summary>
    public void Initialize()
    {
        buildingType = BuildingType.Center;
        StorageInit();
    }

    /// <summary> 창고 초기화 </summary>
    private void StorageInit()
    {
        Storage = new Dictionary<ResourceType, int>();
        foreach (ResourceType item in Enum.GetValues(typeof(ResourceType)))
            Storage.Add(item, earlyStartResource);
        Storage.Remove(ResourceType.None);
    }

    /// <summary>
    /// 자원을 넣으면 해당 자원의 값이 증가합니다.
    /// </summary>
    /// <param name="resourceType">넣을 자원 타입</param>
    /// <param name="resource">넣을 자원</param>
    public void AddCenterResource(ResourceType resourceType, int resource)
    {
        Storage[resourceType] += resource;
    }

    /// <summary>
    /// 창고에서 자원을 꺼내올 수 있는지 확인합니다.
    /// </summary>
    /// <param name="resource">꺼내올 자원</param>
    /// <returns>리턴이 true면 자원을 꺼내온 것입니다.</returns>
    public bool GetCenterResource(ResourceType resource)
    {
        if (Storage[resource] < 0)
            return false;

        Storage[resource]--;
        return true;
    }

    private void OnDisable()
    {
        Disable();
    }

    public void Disable()
    {
        Storage.Clear();
    }
}