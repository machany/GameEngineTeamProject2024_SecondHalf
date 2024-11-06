using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public enum BuildingType // 건물의 속성을 나타냅니다.
    {
        Company,
        Center
    }
    
    public BuildingType buildingType;
}
