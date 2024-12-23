using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Vehicle/VehicleSO")]
public class VehicleSO : ScriptableObject
{
    public VehicleType vehicleType;
    public Sprite sprite;

    [Header("[ Stat ]")]
    public float moveSpeed;
    [Tooltip("자원 종류 최대 개수")]
    public int maxStorageResourceCapacity;
    [Tooltip("자원 최대 개수")]
    public int maxStorageCapacity;
    [Tooltip("회사에서 기다리는 최소 시간")]
    [Range(0f, 5f)]
    public float minStopTime;
    [Tooltip("회사에서 기다리는 최대 시간")]
    [Range(0f, 5f)]
    public float maxStopTime;

#if UNITY_EDITOR
    private void OnValidate()
    {
        maxStopTime = Mathf.Clamp(maxStopTime, minStopTime, 5f);
    }
#endif
}
