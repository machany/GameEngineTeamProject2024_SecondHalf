using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CompanyEffect : MonoBehaviour
{
    [SerializeField] private Material _material;

    [Header("set")]
    [SerializeField] private string circleSize = "_CircleSize";
    [SerializeField] private string lineThick = "_LineThick";

    private int circleSizeID;
    private int lineThickID;

    [Header("value")]
    [Range(0f, 1f)]
    [SerializeField] private float startCircleSize;
    [Range(0f, 1f)]
    [SerializeField] private float startLineThick, endCircleSize, endLineThick = 0;

    private static bool _isReset;
    private static float _lifeTime;

    public void OnEnable()
    {
        if (!_isReset)
        {
            _lifeTime = transform.GetComponent<ParticleSystem>().startLifetime;

            circleSizeID = Shader.PropertyToID(circleSize);
            lineThickID = Shader.PropertyToID(lineThick);

            _isReset = true;
        }

        _material.SetFloat(circleSizeID, startCircleSize);

        DOTween.To(() => startCircleSize, size => { _material.SetFloat(circleSizeID, size); }, endCircleSize, _lifeTime);
        DOTween.To(() => startLineThick, size => { _material.SetFloat(lineThickID, size); }, endLineThick, _lifeTime);
    }
}
