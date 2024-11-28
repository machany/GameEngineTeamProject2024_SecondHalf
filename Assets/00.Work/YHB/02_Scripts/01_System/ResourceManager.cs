using System.Collections.Generic;

public enum VehicleType
{
    car,
    truck,
    tailer
}

public class ResourceManager : MonoSingleton<ResourceManager>
{
    public Dictionary<LineGroupType, bool> lineGroup = new Dictionary<LineGroupType, bool>();
    public Dictionary<VehicleType, sbyte> useVehicle = new Dictionary<VehicleType, sbyte>();
    public Dictionary<VehicleType, sbyte> maxVehicle = new Dictionary<VehicleType, sbyte>();

    public sbyte startBridge = 3;
    public sbyte maxBridge = 3;

    public bool TryUseLineGroup(LineGroupType type)
        => lineGroup[type];

    public bool TryUseVehicle(VehicleType type)
    {
        bool save = useVehicle[type] < maxVehicle[type];

        if (save) useVehicle[type]++;

        return save;
    }

}
