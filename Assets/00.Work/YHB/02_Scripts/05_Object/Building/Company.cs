using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public enum ResourceType
{
    None,
    Red,
    Yellow,
    Green,
    Blue,
    Purple
}

public enum CompanyShapeType // 회사의 모양을 나타냅니다.
{
    Circle,
    Triangle,
    InvertedTriangle,
    Square,
    Rhombus
}

public class Company : Building
{
    /// <summary>회사의 모양</summary>
    public CompanyShapeType shapeType;
    /// <summary>회사의 필요로 하는 자원, 색</summary>
    public ResourceType requestType;

    private GameOverCount _countDown = new GameOverCount();

    private int _requestCost;
    /// <summary>회사가 필요로하는 자원 갯수</summary>
    public int RequestCost
    {
        get => _requestCost;
        set
        {
            if (value > CompanyManager.Instance.maxRequestCost)
                _countDown.RequestOverCountDown();
            
            _requestCost = value;
        }
    }
    
    private int _productCost;
    /// <summary>회사가 생산한 자원</summary>
    public int ProductCost
    {
        get => _productCost;
        set => _productCost = value < 0 ? 0 :
            value > CompanyManager.Instance.maxProductCost ? CompanyManager.Instance.maxProductCost : value;
    }

    private void OnEnable()
    {
        Initialize();
    }

    /// <summary>초기화 함수 입니다.</summary>
    private void Initialize()
    {
        buildingType = BuildingType.Company;
        requestType = CompanyManager.Instance.companyResource.Get();
        shapeType = CompanyManager.Instance.companyShape.Get();
        
        RequestCost = 0;
        ProductCost = 0;
        
        CompanyManager.Instance.OnCompanyProduct += HandleProduct;
        CompanyManager.Instance.OnCompanyRequest += HandleRequest;

        #region test

        transform.GetComponent<SpriteRenderer>().color = CompanyInfo.Instance.GetResourceColor(requestType);
        transform.GetComponent<SpriteRenderer>().sprite = CompanyInfo.Instance.GetShapeSprite(shapeType);

        #endregion
    }
    
    public ResourceType GetCompanyResourceType() => CompanyManager.Instance.productShape[shapeType];

    /// <summary>자원을 생산합니다.</summary>
    private void HandleProduct(int n)
    {
        if (n != 0) 
            StartCoroutine(ProductCoe(n < 0 ? 0 : 1));
    }
    
    /// <summary>자원을 필요로 합니다.</summary>
    private void HandleRequest(int n)
    {
        if (n != 0) 
            StartCoroutine(RequestCoe(n > 0 ? 1 : 0));
    }

    /// <summary>회사가 자원을 n개 더 생산합니다.</summary>
    /// <param name="n">추가로 생산할 자원의 개수 기본값 : 1</param>
    private IEnumerator ProductCoe(int n)
    {
        yield return new WaitForSeconds(Random.Range(CompanyManager.Instance.minDelayTime, CompanyManager.Instance.maxDelayTime));
        ProductCost += n;
        Debug.Log("ProductCost : " + ProductCost);
    }

    /// <summary>회사가 자원을 n개 더 필요로합니다.</summary>
    /// <param name="n">추가할 필요 자원의 개수 기본값 : 1</param>
    private IEnumerator RequestCoe(int n)
    {
        yield return new WaitForSeconds(Random.Range(CompanyManager.Instance.minDelayTime, CompanyManager.Instance.maxDelayTime));
        RequestCost += n;
        Debug.Log("RequestCost : " + RequestCost);
    }
    
    /// <summary>회사를 비활성화 할 때 사용합니다.</summary>
    private void DisableCompany()
    {
        CompanyManager.Instance.OnCompanyProduct -= HandleProduct;
        CompanyManager.Instance.OnCompanyRequest -= HandleRequest;
    }

    private void OnDisable()
    {
        DisableCompany();
    }

}