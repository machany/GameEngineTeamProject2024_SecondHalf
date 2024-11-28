using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

public enum ResourceType
{
    None = 9,
    Red = 0,
    Yellow = 1,
    Green = 2,
    Blue = 3,
    Purple = 4
}

public enum CompanyShapeType // 회사의 모양을 나타냅니다.
{
    Circle = 0,
    Triangle = 1,
    InvertedTriangle = 2,
    Square = 3,
    Rhombus = 4
}

public class Company : Building, IInitialize
{
    [SerializeField] private SortMark sortMark;
    
    /// <summary>회사의 모양</summary>
    public CompanyShapeType shapeType;
    /// <summary>회사의 필요로 하는 자원, 색</summary>
    public ResourceType requestType;

    private GameOverCount _countDown;

    public Action<int> OnRequestCostChanged, OnProductCostChanged;

    private int _requestCost;
    /// <summary>회사가 필요로하는 자원 갯수</summary>
    public int RequestCost
    {
        get => _requestCost;
        set
        {
            _requestCost = Mathf.Clamp(value, 0, CompanyManager.Instance.companyInfo.maxRequestCost);

            if (_requestCost >= CompanyManager.Instance.companyInfo.limitRequestCost && !_countDown.countDown)
                _countDown.RequestOverCountDown();
            else if (_countDown.countDown && _requestCost < CompanyManager.Instance.companyInfo.limitRequestCost)
                _countDown.RequestCancelCountDown();

            OnRequestCostChanged?.Invoke(_requestCost);
        }
    }

    private int _productCost;
    /// <summary>회사가 생산한 자원</summary>
    public int ProductCost
    {
        get => _productCost;
        set
        {
            _productCost = Mathf.Clamp(value, 0, CompanyManager.Instance.companyInfo.maxProductCost);
            OnProductCostChanged?.Invoke(_productCost);
        }
    }

    private void OnEnable()
    {
        Initialize();
    }

    /// <summary>초기화 함수 입니다.</summary>
    public void Initialize()
    {
        buildingType = BuildingType.Company;
        requestType = CompanyManager.Instance.companyResource.GetValue();
        shapeType = CompanyManager.Instance.companyShape.GetValue();

        CompanyManager.Instance.OnCompanyProduct += HandleProduct;
        CompanyManager.Instance.OnCompanyRequest += HandleRequest;
        _countDown = transform.AddComponent<GameOverCount>();

        RequestCost = 0;
        ProductCost = 0;

        #region test

        transform.GetComponent<SpriteRenderer>().color = CompanyManager.Instance.companyInfo.GetResourceColor(requestType);
        transform.GetComponent<SpriteRenderer>().sprite = CompanyManager.Instance.companyInfo.GetShapeSprite(shapeType);

        #endregion
    }

    public ResourceType GetProductResourceType() => CompanyManager.Instance.productShape[shapeType];
    public ResourceType GetRequestResource() => requestType;

    // 남는 값 반환
    public int ReceiveRequestResource(int n)
    {
        int save = n > RequestCost ? n - RequestCost : 0;
        RequestCost -= n > RequestCost ? RequestCost : n;
        return save;
    }

    // 주는 값 반환
    public int SendProductResource(int n)
    {
        int save = n >= ProductCost ? ProductCost : n;
        ProductCost -= save;
        return save;
    }


    /// <summary>자원을 생산합니다.</summary>
    private void HandleProduct()
    {
        sbyte n = (sbyte)Random.Range(0, 2);
        StartCoroutine(ProductCoe(n));
    }

    /// <summary>자원을 필요로 합니다.</summary>
    private void HandleRequest()
    {
        sbyte n = (sbyte)Random.Range(0, 2);
        StartCoroutine(RequestCoe(n));
    }

    /// <summary>회사가 자원을 n개 더 생산합니다.</summary>
    /// <param name="n">추가로 생산할 자원의 개수 기본값 : 1</param>
    private IEnumerator ProductCoe(int n)
    {
        if (_countDown.countDown)
            LineController.Instance.MakeCompanyEffect(transform.position, CompanyManager.Instance.companyInfo.GetResourceColor(requestType));

        yield return new WaitForSeconds(Random.Range(CompanyManager.Instance.companyInfo.minDelayTime, CompanyManager.Instance.companyInfo.maxDelayTime));

        if (_countDown.countDown)
            LineController.Instance.MakeCompanyEffect(transform.position, CompanyManager.Instance.companyInfo.GetResourceColor(requestType));
        ProductCost += n;
    }

    /// <summary>회사가 자원을 n개 더 필요로합니다.</summary>
    /// <param name="n">추가할 필요 자원의 개수 기본값 : 1</param>
    private IEnumerator RequestCoe(int n)
    {
        if (_countDown.countDown)
            LineController.Instance.MakeCompanyEffect(transform.position, CompanyManager.Instance.companyInfo.GetResourceColor(requestType));

        yield return new WaitForSeconds(Random.Range(CompanyManager.Instance.companyInfo.minDelayTime, CompanyManager.Instance.companyInfo.maxDelayTime));

        if (_countDown.countDown)
            LineController.Instance.MakeCompanyEffect(transform.position, CompanyManager.Instance.companyInfo.GetResourceColor(requestType));
        RequestCost += n;
    }

    /// <summary>회사를 비활성화 할 때 사용합니다.</summary>
    public void Disable()
    {
        CompanyManager.Instance.OnCompanyProduct -= HandleProduct;
        CompanyManager.Instance.OnCompanyRequest -= HandleRequest;
    }

    private void OnDisable()
    {
        sortMark.Disable(this);
        Disable();
    }

}
