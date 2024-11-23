using System;
using System.Collections;
using UnityEngine;

public class GameOverCount : MonoBehaviour
{
    public bool countDown;
    private float _countDownTime;
    
    private int _referenceCount;
    /// <summary>게임오버시 발생되는 이벤트</summary>
    public Action OnGameOver;

    public GameOverCount()
    {
        _countDownTime = CompanyInfo.Instance.startCountDownTime;
    }

    /// <summary>카운트 다운을 시작합니다.</summary>
    public void RequestOverCountDown()
    {
        Debug.Log("Requesting over countdown");
        StartCoroutine(CountDown());
        if (_countDownTime <= 0) 
        {
            OnGameOver?.Invoke();
        }
    }
    /// <summary>카운트 다운을 멈춤</summary>
    public void RequestCancelCountDown()
    {
        Debug.Log("Requesting In countdown");
        StopCoroutine(CountDown());
        _countDownTime = CompanyInfo.Instance.startCountDownTime;
    }
    
    /// <summary>오브젝트를 활성화 요청</summary>
    public void RequestEnableObject(GameObject target)
    {
        _referenceCount++;
        if (!target.activeSelf)
        {
            target.SetActive(true);
        }
    }
    /// <summary>오브젝트를 비활성화 요청</summary>
    public void RequestDisableObject(GameObject target)
    {
        _referenceCount = Mathf.Max(0, _referenceCount - 1);
        if (_referenceCount == 0 && target.activeSelf)
        {
            target.SetActive(false);
        }
    }

    /// <summary>카운트 다운을 하는 코루틴</summary>
    private IEnumerator CountDown()
    {
        while (_countDownTime <= 0) 
        {
            yield return new WaitForSeconds(Time.deltaTime);
            _countDownTime -= Time.deltaTime;
        }
    }
}
