using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoSingleton<FadeManager>
{
    [Header("fade settings")]
    [SerializeField] private Image image;
    [SerializeField] private Color color;
    [SerializeField] private float fadeDuration;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void FadeOut(Action action) => Instance.Out(action);

    public static void FadeIn(Action action) => Instance.In(action);

    private void In(Action action)
    {
        image.raycastTarget = false;
        StartCoroutine(FadeInCoroutine(action));
    }

    private IEnumerator FadeInCoroutine(Action action)
    {
        color = image.color;

        while (image.color.a >= 0)
        {
            float f = Time.deltaTime / fadeDuration;
            color.a -= f;
            image.color = color;
            yield return null;
        }

        action?.Invoke();
    }

    private void Out(Action action)
    {
        image.raycastTarget = true;
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(action));
    }

    private IEnumerator FadeOutCoroutine(Action action)
    {
        color = image.color;

        while (image.color.a <= 1)
        {
            float f = Time.deltaTime / fadeDuration;
            color.a += f;
            image.color = color;

            yield return null;
        }

        action?.Invoke();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        In(() => { });
    }
}