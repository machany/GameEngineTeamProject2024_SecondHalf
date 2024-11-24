using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineUI : UIToolkit, IInputable
{
    public static Action OnToggleLineEvent;
    public static Action OnRedLineEvent;
    public static Action OnYellowLineEvent;
    public static Action OnGreenLineEvent;
    public static Action OnBlueLineEvent;
    public static Action OnPurpleLineEvent;

    [field: SerializeField] public InputReader InputCompo { get; private set; }

    private const string _toggleStr = "Button_Toggle";

    private readonly string[] _lineStr =
    {
        "Button_Red", "Button_Yellow", "Button_Green", "Button_Blue", "Button_Purple"
    };

    private Button _toggleButton;

    private Button[] _lineButtons = new Button[5];

    private bool _isInput;
    private bool[] _isLineSelected = new bool[5];

    private void OnEnable()
    {
        GetUIElements();

        _toggleButton.clicked += ClickToggleButton;
        _lineButtons[0].clicked += ClickRedButton;
        _lineButtons[1].clicked += ClickYellowButton;
        _lineButtons[2].clicked += ClickGreenButton;
        _lineButtons[3].clicked += ClickBlueButton;
        _lineButtons[4].clicked += ClickPurpleButton;

        InputCompo.OnToggleLineEvent += ClickToggleButton;
        InputCompo.OnRedLineEvent += ClickRedButton;
        InputCompo.OnYellowLineEvent += ClickYellowButton;
        InputCompo.OnGreenLineEvent += ClickGreenButton;
        InputCompo.OnBlueLineEvent += ClickBlueButton;
        InputCompo.OnPurpleLineEvent += ClickPurpleButton;
    }

    private void OnDisable()
    {
        _toggleButton.clicked -= ClickToggleButton;
        _lineButtons[0].clicked -= ClickRedButton;
        _lineButtons[1].clicked -= ClickYellowButton;
        _lineButtons[2].clicked -= ClickGreenButton;
        _lineButtons[3].clicked -= ClickBlueButton;
        _lineButtons[4].clicked -= ClickPurpleButton;

        InputCompo.OnToggleLineEvent -= ClickToggleButton;
        InputCompo.OnRedLineEvent -= ClickRedButton;
        InputCompo.OnYellowLineEvent -= ClickYellowButton;
        InputCompo.OnGreenLineEvent -= ClickGreenButton;
        InputCompo.OnBlueLineEvent -= ClickBlueButton;
        InputCompo.OnPurpleLineEvent -= ClickPurpleButton;
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _toggleButton = root.Q<Button>(_toggleStr);

        for (int i = 0; i < 5; ++i)
            _lineButtons[i] = root.Q<Button>(_lineStr[i]);
    }

    private void ClickToggleButton()
    {
        _isInput = !_isInput;
        OnToggleLineEvent?.Invoke();
    }

    private void ClickRedButton()
    {
        _isLineSelected[0] = !_isLineSelected[0];

        for (int i = 1; i < 5; ++i)
            _isLineSelected[i] = false;

        OnRedLineEvent?.Invoke();
    }

    private void ClickYellowButton()
    {
        _isLineSelected[1] = !_isLineSelected[1];

        for (int i = 0; i < 5; ++i)
            _isLineSelected[i] = i == 1 && _isLineSelected[i];

        OnYellowLineEvent?.Invoke();
    }

    private void ClickGreenButton()
    {
        _isLineSelected[2] = !_isLineSelected[2];
        
        for (int i = 0; i < 5; ++i)
            _isLineSelected[i] = i == 2 && _isLineSelected[i];

        OnGreenLineEvent?.Invoke();
    }

    private void ClickBlueButton()
    {
        _isLineSelected[3] = !_isLineSelected[3];
        
        for (int i = 0; i < 5; ++i)
            _isLineSelected[i] = i == 3 && _isLineSelected[i];

        OnBlueLineEvent?.Invoke();
    }

    private void ClickPurpleButton()
    {
        _isLineSelected[4] = !_isLineSelected[4];

        for (int i = 1; i < 4; ++i)
            _isLineSelected[i] = false;

        OnPurpleLineEvent?.Invoke();
    }
}