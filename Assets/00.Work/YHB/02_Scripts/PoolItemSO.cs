using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PoolItem")]
public class PoolItemSO : ScriptableObject
{
    public string key;
    public GameObject prefab;
}
