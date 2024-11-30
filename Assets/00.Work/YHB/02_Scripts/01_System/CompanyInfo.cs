using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CompanyInfo")]
public class CompanyInfoSO : ScriptableObject
{
    [Header("Info")]
    [Tooltip("Circle, Triangle, InvertedTriangle, Square, Rhombus순 입니다.")]
    [SerializeField] private List<Sprite> companySprites;
    
    [Tooltip("Red, Yellow, Green, Blue, Purple순 입니다.")]
    [SerializeField] private List<Color> companyColor;

    [Header("Resources")]
    // 카운트 다운이 시작되는 자원의 양
    public int limitRequestCost = 5;
    // 생산된 자원이 최대로 보관할 수 있는 양
    public int maxProductCost = 5;
    // 필요로 하는 자원이 최대로 보관할 수 있는 양
    public int maxRequestCost = 5;

    [Header("Company")]
    // 자원 생산을 위한 최대/소 시간 딜레이
    public float minDelayTime;
    public float maxDelayTime;
    // 정렬시 걸리는 시간
    public float DuringTime;
    public float startCountDownTime = 5.7f;

    [Header("SortMark")]
    public float interval;
    public float requestPos, productPos;

    [Header("Resource")]
    public float productTime;
    public float requestTime, productAddTime, requestAddTime;

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
