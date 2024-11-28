using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PoolItem")]
public class PoolItemSO : ScriptableObject
{
    public int key;
    public GameObject prefab;

    private void OnValidate()
    {
        key = this.name.GetHashCode();
    }
}
