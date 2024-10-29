using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyInfo : MonoBehaviour
{
    public static CompanyInfo Instance;
    
    [Header("Info")]
    [Tooltip("Circle, Triangle, InvertedTriangle, Square, Rhombus순 입니다.")]
    [SerializeField] private List<Sprite> companySprites;
    
    [Tooltip("Red, Yellow, Green, Blue, Purple순 입니다.")]
    [SerializeField] private List<Color> companyColor;
    
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
    /// 자원의 타입에 맞는 색을 반환해주는 함수입니다.
    /// </summary>
    /// <param name="resourceType">자원의 타입</param>
    /// <returns>자원의 색</returns>
    public Color GetResourceColor(ResourceType resourceType) => resourceType switch
    {
        ResourceType.Red => companyColor[0],
        ResourceType.Yellow => companyColor[1],
        ResourceType.Green => companyColor[2],
        ResourceType.Blue => companyColor[3],
        ResourceType.Purple => companyColor[4],
        _ => Color.black
    };
    
    /// <summary>
    /// 회사의 모양에 맞는 이미지을 반환해주는 함수입니다.
    /// </summary>
    /// <param name="companyShapeT">모양의 타입</param>
    /// <returns>스프라이트</returns>
    public Sprite GetShapeSprite(CompanyShapeType companyShapeT) => companyShapeT switch
    {
        CompanyShapeType.Circle => companySprites[0],
        CompanyShapeType.Triangle => companySprites[1],
        CompanyShapeType.InvertedTriangle => companySprites[2],
        CompanyShapeType.Square => companySprites[3],
        CompanyShapeType.Rhombus => companySprites[4],
        _ => throw new Exception("out index enum")
    };
}
