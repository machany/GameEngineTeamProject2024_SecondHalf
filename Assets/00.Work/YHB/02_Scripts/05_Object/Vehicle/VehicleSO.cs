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
    [Tooltip("�ڿ� ���� �ִ� ����")]
    public int maxStorageResourceCapacity;
    [Tooltip("�ڿ� �ִ� ����")]
    public int maxStorageCapacity;
    [Tooltip("ȸ�翡�� ��ٸ��� �ּ� �ð�")]
    [Range(0f, 5f)]
    public float minStopTime;
    [Tooltip("ȸ�翡�� ��ٸ��� �ִ� �ð�")]
    [Range(0f, 5f)]
    public float maxStopTime;

#if UNITY_EDITOR
    private void OnValidate()
    {
        maxStopTime = Mathf.Clamp(maxStopTime, minStopTime, 5f);
    }
#endif
}
