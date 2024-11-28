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
    public Dictionary<LineGroupType, bool> lineGroup {  get; private set; }
    private Dictionary<VehicleType, sbyte> useVehicle;
    private Dictionary<VehicleType, sbyte> maxUsedVehicle;

    [Header("Bridge")]
    [Tooltip("���۽� ���޵Ǵ� �ٸ�")]
    public sbyte startBridge = 3;
    [Tooltip("�ִ�� ������ �� �ִ� �ٸ�")]
    public sbyte maxBridge = 10;

    [Header("Line")]
    [Tooltip("���۽� ����� �� �ִ� ������ ����")]
    [Range(1, 5)]
    [SerializeField] private sbyte startLineCount = 1;
    [Tooltip("�ִ�� ����� �� �ִ� ������ ����")]
    [Range(1, 5)]
    [SerializeField] private sbyte maxLineCount = 1;
    private sbyte _curLineCount = 0;

    [Header("Vehicle")]
    [Tooltip("���۽� �����ϴ� ���� ��")]
    [Range(0, 10)]
    [SerializeField] private sbyte startCar = 1;
    [Tooltip("���۽� �����ϴ� Ʈ���� ��")]
    [Range(0, 10)]
    [SerializeField] private sbyte startTruck = 1;
    [Tooltip("���۽� �����ϴ� Ʈ���Ϸ��� ��")]
    [Range(0, 10)]
    [SerializeField] private sbyte startTrailer = 1;
    [Tooltip("�ִ�� ����ϴ� ���� ��")]
    [Range(1, 100)]
    [SerializeField] private sbyte maxVehicle = 100;

    private void Awake()
    {
        _curLineCount = startLineCount;
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

    public void AddVehicle(VehicleType type, sbyte value)
        => maxUsedVehicle[type] = (sbyte)Mathf.Clamp(maxUsedVehicle[type] + value, 1, maxVehicle);

    public void UnlockLine()
    {        Debug.Log(lineGroup[(LineGroupType)_curLineCount]);

        if (_curLineCount < maxLineCount)
            lineGroup[(LineGroupType)_curLineCount++] = true;
    }
}
