using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DangerShade : MonoBehaviour
{
    public Material _material;
    public string VignetteSoftness = "_VignetteSoftness";
    public float value1 = 0.5f;
    public float value2 = 0.3f;
    public float transitionDuration = 2.0f;
    private Coroutine _vignetteCoroutine;
    private void Awake()
    {
        _material = GetComponent<Image>().material;
    }

    private void OnEnable()
    {
         StartCoroutine(SmoothChangeVignetteSoftness());
    }
    private void OnDisable()
    {
        if (_vignetteCoroutine != null)
        {
            StopCoroutine(_vignetteCoroutine);
            _vignetteCoroutine = null;
        }
    }
    private IEnumerator SmoothChangeVignetteSoftness()
    {
        while (true)
        {
            yield return LerpVignetteSoftness(value1, value2);
            yield return LerpVignetteSoftness(value2, value1);
        }
    }

    private IEnumerator LerpVignetteSoftness(float startValue, float endValue)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / transitionDuration)
        {
            _material.SetFloat(VignetteSoftness, Mathf.Lerp(startValue, endValue, t));
            yield return null;
        }
        _material.SetFloat(VignetteSoftness, endValue);
    }
}