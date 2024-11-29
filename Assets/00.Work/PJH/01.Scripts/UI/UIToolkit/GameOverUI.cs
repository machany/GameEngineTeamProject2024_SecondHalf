using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverUI : UIToolkit
{
    private const string _descStr = "Label_Desc";
    private const string _titleStr = "Button_Title";
    private const string _exitStr = "Button_Exit";

    private Label _descLabel;
    private Button _titleButton;
    private Button _exitButton;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        GetUIElements();
        
        ChangeDescLabel();

        _titleButton.clicked += ClickTitleButton;
        _exitButton.clicked += ClickExitButton;
    }

    private void OnDisable()
    {
        _titleButton.clicked -= ClickTitleButton;
        _exitButton.clicked -= ClickExitButton;
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _descLabel = root.Q<Label>(_descStr);
        _titleButton = root.Q<Button>(_titleStr);
        _exitButton = root.Q<Button>(_exitStr);
    }

    private void ClickTitleButton()
    {
        // 씬이동 Fade
    }

    private void ClickExitButton()
    {
        FadeManager.FadeOut(Application.Quit);
    }

    private void ChangeDescLabel()
    {
        _descLabel.text = "당신의 회사는 너무 혼잡해 폐쇄되었습니다.\n" +
                          $"당신은 {SpeedUI.currentDate}일 동안 회사를 운영했습니다.";
    }
}