using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CompanyManager : MonoSingleton<CompanyManager>
{
    /// <summary>회사의 모양에 따른 생산될 자원을 나타냅니다.</summary>
    public Dictionary<CompanyShapeType, ResourceType> productShape = new Dictionary<CompanyShapeType, ResourceType>();
    ///<summary>모양의 중복을 피하기위한 변수입니다.</summary>
    public NotOverlapEnum<CompanyShapeType> companyShape = new NotOverlapEnum<CompanyShapeType>();
    ///<summary>색의 중복을 피하기 위한 변수입니다.</summary>
    public NotOverlapEnum<ResourceType> companyResource = new NotOverlapEnum<ResourceType>();

    /// <summary>회사에서 자원을 생산하게 합니다.</summary>
    public Action<int> OnCompanyProduct;
    /// <summary>회사에서 자원을 필요하게 합니다.</summary>
    public Action<int> OnCompanyRequest;
    
    [Header("Resources")]
    // 생산된 자원이 최대로 보관할 수 있는 양
    public int maxProductCost = 5;
    // 카운트 다운이 시작되는 자원의 양
    public int maxRequestCost = 5;
    
    [Header("Company")]
    // 자원 생산을 위한 최대/소 시간 딜레이
    public float minDelayTime;
    // 자원 생산을 위한 최대/소 시간 딜레이
    public float maxDelayTime;

    /// 생성 주기입니다.
    [SerializeField] private float productTime, requestTime;
    private float _lastProductTime, _lastRequestTime;
    
    private void Awake()
    {
        Initialize();
    }

    private void LateUpdate()
    {
        AskProductAndRequest();
    }    
    /// <summary>
    /// 초기화 함수 입니다.
    /// </summary>
    private void Initialize()
    {
        ResetProductShape();
    }
    
    /// <summary>
    /// 모양별 생산 자원을 리셋해주는 함수입니다.
    /// </summary>
    private void ResetProductShape()
    {
        productShape.Clear();
        for (int i = 0; i < 5; i++)
            productShape.Add(companyShape.Get(), companyResource.Get());
    }

    private void AskProductAndRequest()
    {
        if (Time.time > _lastProductTime + productTime)
        {
            _lastProductTime = Time.time;
            OnCompanyProduct?.Invoke(Random.Range(0, 2));
        }
        else if (Time.time > _lastRequestTime + requestTime)
        {
            _lastRequestTime = Time.time;
            OnCompanyRequest?.Invoke(Random.Range(0, 2));
        }
    }
}
