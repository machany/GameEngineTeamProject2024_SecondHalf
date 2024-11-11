using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Vehicle/VehicleSO")]
public class VehicleSO : ScriptableObject
{
    [Header("[ Stat ]")]
    public float moveSpeed;
    public int maxStorageResource;
}
