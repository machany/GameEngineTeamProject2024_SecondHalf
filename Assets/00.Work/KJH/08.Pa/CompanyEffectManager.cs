using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyEffectManager : MonoBehaviour
{
    [SerializeField] private Material _material;
    
    public string CircleSize = "_CircleSize";
    public string LineThick = "_LineThick";
    public float value1 = 0.5f;
    public float value2 = 0;

    
    public void StartEffect()
    {
        StartCoroutine(LerpCircleSize());
        StartCoroutine(LerpLineThick());
    }
    
    private IEnumerator LerpCircleSize()
    {
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            _material.SetFloat(CircleSize, Mathf.Lerp(value2, 1, t));
            yield return null;
        }
        _material.SetFloat(CircleSize, value2);
    }
    private IEnumerator LerpLineThick()
    {
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            _material.SetFloat(LineThick, Mathf.Lerp(value1, 1, t));
            yield return null;
        }
        _material.SetFloat(LineThick, value1);
    }
}
