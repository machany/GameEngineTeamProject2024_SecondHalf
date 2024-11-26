using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BridgeManager : MonoSingleton<BridgeManager>, IInitialize
{
    [SerializeField] private int maxBridge = 3;
    [SerializeField] private int startBridge = 3;

    //최대 사용 가능 다리
    private int _availableBridge;
    
    public int AvailableBridge
    {
        get => _availableBridge;
        set => _availableBridge = Mathf.Clamp(value, 0, maxBridge);
    }
    
    //현재 사용하고 있는 다리 수량
    private int curUsedBridge = 0;
    
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        LineController.Instance.OnBridgeChanged += HandleUpdateCurrentBridge;
        AvailableBridge = startBridge;
    }

    /// <summary>
    /// OnBridgeChanged가 호출 될때 마다 현재남은 사용 가능한 다리수가 변경됨
    /// </summary>
    /// <param name="obj">현재 다리에서 쓰고 있는 모든 다리의 수/param>
    private void HandleUpdateCurrentBridge(int obj)
    {
        curUsedBridge = obj;
    }

    /// <summary>
    /// 현재 사용 가능한 다리의 수보다 현재 다리에서 쓰고 있는 모든 다리의 수가 많은지 적은지 판단
    /// </summary>
    /// <param name="allBridge">현재 다리에서 쓰고 있는 모든 다리의 수</param>
    /// <returns>현재 사용 가능한 다리의 수보다 매개변수가 크면 false를 아니면 true</returns>
    public bool CheckBridge(int allBridge)
        => AvailableBridge >= allBridge;

    public int AvailableBridgeCount()
        => AvailableBridge - curUsedBridge;

    private void OnDisable()
    {
        Disable();
    }

    public void Disable()
    {
        LineController.Instance.OnBridgeChanged -= HandleUpdateCurrentBridge;
    }
}
