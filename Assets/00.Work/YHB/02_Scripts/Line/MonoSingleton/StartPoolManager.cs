using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoolManager : MonoBehaviour
{
    [SerializeField] private List<PoolItemSO> items = new List<PoolItemSO>();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach(PoolItemSO item in items)
            PoolManager.Instance.AddPoolItem(item);
    }
}
