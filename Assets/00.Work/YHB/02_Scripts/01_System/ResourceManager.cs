using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum VehicleType
{
    car,
    truck,
    trailer
}

public class ResourceManager : MonoSingleton<ResourceManager>
{
    public Dictionary<LineGroupType, bool> lineGroup;
    public Dictionary<VehicleType, sbyte> useVehicle;
    public Dictionary<VehicleType, sbyte> maxVehicle;

    [Tooltip("시작시 지급되는 다리")]
    public sbyte startBridge = 3;
    [Tooltip("최대로 소유할 수 있는 다리")]
    public sbyte maxBridge = 3;

    private void Awake()
    {
        lineGroup = new Dictionary<LineGroupType, bool>()
        {
            { LineGroupType.Red, true},
            { LineGroupType.Yellow, false},
            { LineGroupType.Green, false},
            { LineGroupType.Blue, false},
            { LineGroupType.Purple, false},
        };

        maxVehicle = new Dictionary<VehicleType, sbyte>()
        {
            {VehicleType.car, 1},
            {VehicleType.truck, 1},
            {VehicleType.trailer, 1},
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
        bool save = useVehicle[type] < maxVehicle[type];

        if (save) useVehicle[type]++;

        return save;
    }

    public void AddVehicle(VehicleType type, sbyte value)
        => maxVehicle[type] += value;
}
