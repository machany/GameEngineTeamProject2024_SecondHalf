using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpeedUI : UIToolkit, IInputable
{
    [field: SerializeField] public InputReader InputCompo { get; private set; }
    
    [SerializeField] private List<float> speedValueList = new();

    private const string _speedStr = "VisualElement_Speed";
    private const string _speedToggleStr = "Button_SpeedToggle";
    private const string _stopStr = "Button_Stop";
    private const string _speed1Str = "Button_Speed1";
    private const string _speed2Str = "Button_Speed2";
    private const string _speed3Str = "Button_Speed3";
    
    private const string _toggleStyle = "visual-element-speed-toggle";

    private VisualElement _speedVisualElement;

    private Button _speedToggleButton;
    private Button _stopButton;
    private Button _speed1Button;
    private Button _speed2Button;
    private Button _speed3Button;

    private int _beforeTime;

    private void OnEnable()
    {
        GetUIElements();

        _speedToggleButton.clicked += ClickSpeedToggleButton;
        _stopButton.clicked += ClickStopButton;
        _speed1Button.clicked += ClickSpeed1Button;
        _speed2Button.clicked += ClickSpeed2Button;
        _speed3Button.clicked += ClickSpeed3Button;
        
        InputCompo.OnTimeStopEvent += OnTimeStopEvent;
        InputCompo.OnTimeSpeedUpEvent += OnTimeSpeedUpEvent;
        InputCompo.OnTimeSpeedDownEvent += OnTimeSpeedDownEvent;
    }

    private void OnDisable()
    {
        _speedToggleButton.clicked -= ClickSpeedToggleButton;
        _stopButton.clicked -= ClickStopButton;
        _speed1Button.clicked -= ClickSpeed1Button;
        _speed2Button.clicked -= ClickSpeed2Button;
        _speed3Button.clicked -= ClickSpeed3Button;
        
        InputCompo.OnTimeStopEvent -= OnTimeStopEvent;
        InputCompo.OnTimeSpeedUpEvent -= OnTimeSpeedUpEvent;
        InputCompo.OnTimeSpeedDownEvent -= OnTimeSpeedDownEvent;
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _speedVisualElement = root.Q<VisualElement>(_speedStr);

        _speedToggleButton = root.Q<Button>(_speedToggleStr);
        _stopButton = root.Q<Button>(_stopStr);
        _speed1Button = root.Q<Button>(_speed1Str);
        _speed2Button = root.Q<Button>(_speed2Str);
        _speed3Button = root.Q<Button>(_speed3Str);
    }

    private void OnTimeStopEvent()
    {
        Time.timeScale = Time.timeScale == 0 ? _beforeTime : 0;
    }

    private void OnTimeSpeedUpEvent()
    {
        if (_beforeTime >= speedValueList.Count - 1)
            return;
        
        Time.timeScale = speedValueList[++_beforeTime];
        Debug.Log(_beforeTime);
    }
    
    private void OnTimeSpeedDownEvent()
    {
        if (_beforeTime <= 0)
            return;
        
        Time.timeScale = speedValueList[--_beforeTime];
        Debug.Log(_beforeTime);
    }

    private void ClickSpeedToggleButton()
    {
        _speedVisualElement.ToggleInClassList(_toggleStyle);
    }

    private void ClickStopButton()
    {
        Time.timeScale = 0;
    }

    private void ClickSpeed1Button()
    {
        Time.timeScale = speedValueList[0];
        _beforeTime = 0;
    }

    private void ClickSpeed2Button()
    {
        Time.timeScale = speedValueList[1];
        _beforeTime = 1;
    }

    private void ClickSpeed3Button()
    {
        Time.timeScale = speedValueList[2];
        _beforeTime = 2;
    }
}