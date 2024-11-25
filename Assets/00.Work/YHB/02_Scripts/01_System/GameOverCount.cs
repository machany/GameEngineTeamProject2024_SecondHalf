using System;
using System.Collections;
using UnityEngine;

public class GameOverCount : MonoBehaviour
{
    public bool countDown;
    private float _countDownTime;

    public GameObject target;

    private int _referenceCount;
    /// <summary>게임오버시 발생되는 이벤트</summary>
    public Action OnGameOver;

    public GameOverCount()
    {
        _countDownTime = CompanyManager.Instance.companyInfo.startCountDownTime;
    }

    /// <summary>카운트 다운을 시작합니다.</summary>
    public void RequestOverCountDown()
    {
        StartCoroutine(CountDown());
        if (_countDownTime <= 0) 
        {
            OnGameOver?.Invoke();
        }
    }
    /// <summary>카운트 다운을 멈춤</summary>
    public void RequestCancelCountDown()
    {
        StopCoroutine(CountDown());
        _countDownTime = CompanyManager.Instance.companyInfo.startCountDownTime;
    }
    
    /// <summary>오브젝트를 활성화 요청</summary>
    public void RequestEnableObject()
    {
        _referenceCount++;
        if (!target.activeSelf)
        {
            target.SetActive(true);
        }
    }
    /// <summary>오브젝트를 비활성화 요청</summary>
    public void RequestDisableObject()
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
