using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuUI : UIToolkit
{
    private const string _startStr = "Button_Start";
    private const string _guideStr = "Button_Guide";
    private const string _settingStr = "Button_Setting";
    private const string _exitStr = "Button_Exit";

    private Button _startButton;
    private Button _guideButton;
    private Button _settingButton;
    private Button _exitButton;

    private void OnEnable()
    {
        GetUIElements();

        _startButton.clicked += ClickStartButton;
        _guideButton.clicked += ClickGuideButton;
        _settingButton.clicked += ClickSettingButton;
        _exitButton.clicked += ClickExitButton;
    }

    private void OnDisable()
    {
        _startButton.clicked -= ClickStartButton;
        _guideButton.clicked -= ClickGuideButton;
        _settingButton.clicked -= ClickSettingButton;
        _exitButton.clicked -= ClickExitButton;
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _startButton = root.Q<Button>(_startStr);
        _guideButton = root.Q<Button>(_guideStr);
        _settingButton = root.Q<Button>(_settingStr);
        _exitButton = root.Q<Button>(_exitStr);
    }

    private void ClickStartButton()
    {
        // 이동
    }

    private void ClickGuideButton()
    {
        // 튜토리얼 씬 전환 FadeManager.FadeIn
    }

    private void ClickSettingButton()
    {
        // 이동
    }

    private void ClickExitButton()
    {
        FadeManager.FadeOut(Application.Quit);
    }
}