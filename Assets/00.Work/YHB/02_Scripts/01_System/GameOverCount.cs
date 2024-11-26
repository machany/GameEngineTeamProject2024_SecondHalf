using System;
using System.Collections;
using UnityEngine;

public class GameOverCount : MonoBehaviour
{
    public bool countDown;
    private float _countDownTime;
    
    /// <summary>게임오버시 발생되는 이벤트</summary>
    public static Action OnGameOver;

    public static Action OnGameOverEffectStart;
    public static Action OnGameOverEffectEnd;

    public GameOverCount()
    {
        _countDownTime = CompanyManager.Instance.companyInfo.startCountDownTime;
    }

    /// <summary>카운트 다운을 시작합니다.</summary>
    public void RequestOverCountDown()
    {
        SoundManager.Instance.PlaySound(SoundType.SFX, "GameOverCountDown");
        countDown = true;
        OnGameOverEffectStart?.Invoke();
        _countDownTime = CompanyManager.Instance.companyInfo.startCountDownTime;  
        StartCoroutine(CountDown());
    }
    /// <summary>카운트 다운을 멈춤</summary>
    public void RequestCancelCountDown()
    {
        SoundManager.Instance.StopSound(SoundType.SFX, "GameOverCountDown");
        countDown = false;
        OnGameOverEffectEnd?.Invoke();
        StopCoroutine(CountDown());
    }

    /// <summary>카운트 다운을 하는 코루틴</summary>
    private IEnumerator CountDown()
    {
        while (_countDownTime > 0) 
        {
            yield return new WaitForSeconds(Time.deltaTime);
            _countDownTime -= Time.deltaTime;
        }
        OnGameOver?.Invoke();
    }
}