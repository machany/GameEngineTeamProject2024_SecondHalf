using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CompanyManager : MonoBehaviour
{
    public static CompanyManager Instance;
    
    /// <summary>회사의 모양에 따른 생산될 자원을 나타냅니다.</summary>
    public Dictionary<Company.CompanyShapeType, ResourceType> productShape = new Dictionary<Company.CompanyShapeType, ResourceType>();
    ///<summary>모양의 중복을 피하기위한 변수입니다.</summary>
    private NotOverlapEnumType<Company.CompanyShapeType> _companyShape = new NotOverlapEnumType<Company.CompanyShapeType>();
    ///<summary>색의 중복을 피하기 위한 변수입니다.</summary>
    private NotOverlapEnumType<ResourceType> _companyResource = new NotOverlapEnumType<ResourceType>();
    
    /// <summary>생산된 자원이 최대로 보관할 수 있는 양</summary>
    public int maxProductCost = 5;
    /// <summary>필요로 할 수 있는 자원의 최대의 양</summary>
    public int maxRequestCost = 5;
    
    /// <summary></summary>
    public float minProductTime, maxProductTime;
    /// <summary></summary>
    public float minRequestTime, maxRequestTime;

    private void Awake()
    {
        Initialize();
    }
    
    /// <summary>
    /// 초기화 함수 입니다.
    /// </summary>
    private void Initialize()
    {
        if (Instance == null)
            Instance = this;
    }
    
    /// <summary>
    /// 모양별 생산 자원을 리셋해주는 함수입니다.
    /// </summary>
    private void ResetProductShape()
    {
        productShape.Clear();
        productShape.Add(_companyShape.Get(), _companyResource.Get());
    }
    
    /// <summary>
    /// 자원의 타입에 맞는 색을 반환해주는 함수입니다.
    /// </summary>
    /// <param name="resourceType">자원의 타입</param>
    /// <returns>자원의 색</returns>
    public Color GetResourceColor(ResourceType resourceType) => resourceType switch
    {
        ResourceType.Red => Color.red,
        ResourceType.Yellow => Color.yellow,
        ResourceType.Green => Color.green,
        ResourceType.Blue => Color.blue,
        ResourceType.Black => Color.black,
        _ => Color.white
    };
}
