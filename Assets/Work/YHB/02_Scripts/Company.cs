public enum ResourceType
{
    Red,
    Yellow,
    Green,
    Blue,
    Black
}

public class Company : Building
{
    public enum CompanyShapeType // 회사의 모양을 나타냅니다.
    {
        Circle,
        Triangle,
        InvertedTriangle,
        Square,
        Rhombus
    }

    /// <summary>회사의 모양</summary>
    public CompanyShapeType _shapeType;
    /// <summary>회사의 필요로 하는 자원, 색</summary>
    public ResourceType resourceType;

    /// <summary>회사가 필요로하는 자원 갯수</summary>
    private int _requsetCost;
    private int _productCost;
    /// <summary>회사가 생산한 자원</summary>
    private int ProdutCost
    {
        get { return _productCost; }
        set
        {
            if (value > CompanyManager.Instance.maxProductCost) _productCost = CompanyManager.Instance.maxProductCost;
            else if (value < 0) _productCost = 0;
            else _productCost = value;
        }
    }

    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// 초기화 함수 입니다.
    /// </summary>
    private void Initialize()
    {
        buildingType = BuildingType.Company;
        _needCost = 0;
        ProdutCost = 0;
    }

    
}
