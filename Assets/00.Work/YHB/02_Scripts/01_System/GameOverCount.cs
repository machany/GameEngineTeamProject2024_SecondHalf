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

    public static Action OnGameOverEffectStart;
    public static Action OnGameOverEffectEnd;

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
            OnGameOverEffectStart?.Invoke();
            OnGameOver?.Invoke();
        }
    }
    /// <summary>카운트 다운을 멈춤</summary>
    public void RequestCancelCountDown()
    {
        OnGameOverEffectEnd?.Invoke();
        StopCoroutine(CountDown());
        _countDownTime = CompanyManager.Instance.companyInfo.startCountDownTime;
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
