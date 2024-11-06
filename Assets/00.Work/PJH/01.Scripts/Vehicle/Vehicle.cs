using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle
{
    protected ResourceType storageResourceType;
    protected int storageResource;
    protected int maxStorageResource;
    
    protected float moveSpeed;
}
