using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVehicle
{
    public void ResourceChange(ResourceType resourceType, int resource);

    public void CompanyReach(Company company);

    public void CenterReach(DistributionCenter center);
}
