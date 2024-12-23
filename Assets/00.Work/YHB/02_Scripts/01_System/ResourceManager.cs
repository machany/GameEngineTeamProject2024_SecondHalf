using System.Collections.Generic;
using UnityEngine;

public enum VehicleType
{
    car,
    truck,
    trailer
}

public class ResourceManager : MonoSingleton<ResourceManager>
{
    public Dictionary<LineGroupType, bool> lineGroup { get; private set; }
    private Dictionary<VehicleType, sbyte> useVehicle;
    private Dictionary<VehicleType, sbyte> maxUsedVehicle;

    [Header("Bridge")]
    [Tooltip("시작시 지급되는 다리")]
    public sbyte startBridge = 3;
    [Tooltip("최대로 소유할 수 있는 다리")]
    public sbyte maxBridge = 10;

    [Header("Line")]
    [Tooltip("시작시 사용할 수 있는 선로의 갯수")]
    [Range(1, 5)]
    [SerializeField] private sbyte startLineCount = 1;
    [Tooltip("최대로 사용할 수 있는 선로의 갯수")]
    [Range(1, 5)]
    [SerializeField] private sbyte maxLineCount = 1;

    [Header("Vehicle")]
    [Tooltip("시작시 지급하는 차의 수")]
    [Range(0, 10)]
    [SerializeField] private sbyte startCar = 1;
    [Tooltip("시작시 지급하는 트럭의 수")]
    [Range(0, 10)]
    [SerializeField] private sbyte startTruck = 1;
    [Tooltip("시작시 지급하는 트레일러의 수")]
    [Range(0, 10)]
    [SerializeField] private sbyte startTrailer = 1;
    [Tooltip("최대로 사용하는 차의 수")]
    [Range(1, 100)]
    [SerializeField] private sbyte maxVehicle = 100;

    private void Awake()
    {
        lineGroup = new Dictionary<LineGroupType, bool>()
        {
            { LineGroupType.Red, false},
            { LineGroupType.Yellow, false},
            { LineGroupType.Green, false},
            { LineGroupType.Blue, false},
            { LineGroupType.Purple, false},
        };

        for (int i = 0; i < startLineCount; i++)
            UnlockLine();

        maxUsedVehicle = new Dictionary<VehicleType, sbyte>()
        {
            {VehicleType.car, startCar},
            {VehicleType.truck, startTruck},
            {VehicleType.trailer, startTrailer},
        };

        useVehicle = new Dictionary<VehicleType, sbyte>()
        {
            {VehicleType.car, 0},
            {VehicleType.truck, 0},
            {VehicleType.trailer, 0},
        };
    }

    public bool TryUseLineGroup(LineGroupType type)
        => lineGroup[type];

    public bool TryUseVehicle(VehicleType type)
    {
        bool save = useVehicle[type] < maxUsedVehicle[type];

        if (save) useVehicle[type]++;

        return save;
    }

    public void ReturnUseVehicle(VehicleType type)
    {
        useVehicle[type]--;
    }

    public int GetVehicleValue(VehicleType type)
        => maxUsedVehicle[type] - useVehicle[type];

    public bool UnlockLine()
    {
        foreach (LineGroupType type in lineGroup.Keys)
        {
            if (!lineGroup[type])
            {
                lineGroup[type] = true;
                return true;
            }
        }
        return false;
    }

    public bool TryAddVehicle(VehicleType type, sbyte value)
    {
        maxUsedVehicle[type] = (sbyte)Mathf.Clamp(maxUsedVehicle[type] + value, 1, maxVehicle);
        return maxUsedVehicle[type] < maxVehicle;
    }
}
