using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionUI : MonoBehaviour, IRootable
{
    private const string optionStr = "Button_Option";
    private const string exitStr = "Button_Exit";
    private const string settingStr = "VisualElement_Setting";

    private Button _optionButton;
    private Button _exitButton;
    private VisualElement _settingVisualElement;

    private void OnEnable()
    {
        GetUIElements();

        _optionButton.clicked += ClickOptionButton;
        _exitButton.clicked += ClickExitButton;
    }

    private void OnDisable()
    {
        _optionButton.clicked -= ClickOptionButton;
        _exitButton.clicked -= ClickExitButton;
    }

    public void GetUIElements()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        _optionButton = root.Q<Button>(optionStr);
        _exitButton = root.Q<Button>(exitStr);
        _settingVisualElement = root.Q<VisualElement>(settingStr);
    }

    private void ClickOptionButton()
    {
        Debug.Log("옶2ㅕㄴ");
    }

    private void ClickExitButton()
    {
        Debug.Log("엗시틑");
    }
}
