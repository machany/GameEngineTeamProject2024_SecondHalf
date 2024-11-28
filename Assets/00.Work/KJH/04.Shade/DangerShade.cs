using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DangerShade : MonoBehaviour
{
    public Image _Image;
    private Coroutine _coroutine;

    private void Awake()
    {
        _Image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(SmoothChangeVignetteSoftness());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator SmoothChangeVignetteSoftness()
    {
        while (true)
        {
            yield return StartCoroutine(In());
            yield return StartCoroutine(Out());
        }
    }

    private IEnumerator In()
    {
        while (_Image.color.a < 1) // 알파 값이 1보다 작을 때까지 증가
        {
            var color = _Image.color;
            color.a = Mathf.Clamp01(color.a + Time.deltaTime); // 알파 값 증가
            _Image.color = color;
            yield return null;
        }
    }

    private IEnumerator Out()
    {
        while (_Image.color.a > 0) // 알파 값이 0보다 클 때까지 감소
        {
            var color = _Image.color;
            color.a = Mathf.Clamp01(color.a - Time.deltaTime); // 알파 값 감소
            _Image.color = color;
            yield return null;
        }
    }
}