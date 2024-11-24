using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Vehicle/VehicleSO")]
public class VehicleSO : ScriptableObject
{
    [Header("[ Stat ]")]
    public float moveSpeed;
    [Tooltip("자원 종류 최대 개수")]
    public int maxStorageResourceCapacity;
    [Tooltip("자원 최대 개수")]
    public int maxStorageCapacity;
}
