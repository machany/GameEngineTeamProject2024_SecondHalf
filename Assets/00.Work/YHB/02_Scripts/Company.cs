using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public enum ResourceType
{
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
    private CompanyShapeType _shapeType;
    /// <summary>회사의 필요로 하는 자원, 색</summary>
    public ResourceType requestType;

    private GameOverCount _countDown = new GameOverCount();

    private int _requestCost;
    /// <summary>회사가 필요로하는 자원 갯수</summary>
    private int RequestCost
    {
        get => _requestCost;
        set
        {
            if (value < CompanyManager.Instance.maxRequestCost)
                _countDown.RequestOverCountDown();
            
            _requestCost = value;
        }
    }
    
    private int _productCost;
    /// <summary>회사가 생산한 자원</summary>
    private int ProductCost
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
        _shapeType = CompanyManager.Instance._companyShape.Get();
        requestType = CompanyManager.Instance._companyResource.Get();
        
        RequestCost = 0;
        ProductCost = 0;
        
        CompanyManager.Instance.OnCompanyProduct += HandleProduct;
        CompanyManager.Instance.OnCompanyRequest += HandleRequest;

        #region test

        transform.GetComponent<SpriteRenderer>().color = CompanyManager.Instance.GetResourceColor(requestType);

        #endregion
    }

    /// <summary>자원을 생산합니다.</summary>
    private void HandleProduct()
    {
        StartCoroutine(ProductCoe(Random.Range(0, 1)));
    }
    
    /// <summary>자원을 필요로 합니다.</summary>
    private void HandleRequest()
    {
        StartCoroutine(RequestCoe(Random.Range(0, 1)));
    }

    /// <summary>회사가 자원을 n개 더 생산합니다.</summary>
    /// <param name="n">추가로 생산할 자원의 개수 기본값 : 1</param>
    private IEnumerator ProductCoe(int n = 1)
    {
        yield return new WaitForSeconds(Random.Range(CompanyManager.Instance.minProductTime, CompanyManager.Instance.maxProductTime));
        ProductCost += n;
    }

    /// <summary>회사가 자원을 n개 더 필요로합니다.</summary>
    /// <param name="n">추가할 필요 자원의 개수 기본값 : 1</param>
    private IEnumerator RequestCoe(int n = 1)
    {
        yield return new WaitForSeconds(Random.Range(CompanyManager.Instance.minRequestTime, CompanyManager.Instance.maxRequestTime));
        RequestCost += n;
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
