using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BridgeManager : MonoSingleton<BridgeManager>
{
    //최대 사용 가능 다리
    public int _maxUsedBridge = 0;
    //현재 다리 수량
    public int _currentBridge = 0;
    //현재 사용 가능한 다리
    public int _currentUsedBridge = 0;

    // 현재 남은 사용 가능한 다리 수
    public int _availableBridges;
    
    private void OnEnable()
    {
        LineController.Instance.OnBridgeChanged += UpdateCurrentBridge;
    }

    /// <summary>
    /// OnBridgeChanged가 호출 될때 마다 현재남은 사용 가능한 다리수가 변경됨
    /// </summary>
    /// <param name="obj">현재 다리에서 쓰고 있는 모든 다리의 수/param>
    private void UpdateCurrentBridge(int obj)
    {   
       _currentUsedBridge = obj;
       _availableBridges = _currentBridge - _currentUsedBridge;
    }

    /// <summary>
    /// 현재 남은 사용 가능한 다리 수 초기화
    /// </summary>
    private void Awake()
    {
        _availableBridges = _currentBridge - _currentUsedBridge;
    }
    /// <summary>
    /// 현재 사용 가능한 다리의 수보다 현재 다리에서 쓰고 있는 모든 다리의 수가 많은지 적은지 판단
    /// </summary>
    /// <param name="allBridge">현재 다리에서 쓰고 있는 모든 다리의 수</param>
    /// <returns>현재 사용 가능한 다리의 수보다 매개변수가 크면 false를 아니면 true</returns>
    public bool HandleCheckBridge(int allBridge)
    {
        return !(_currentUsedBridge < allBridge);
    }
}
