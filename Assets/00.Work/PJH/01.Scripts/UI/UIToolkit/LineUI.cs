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

    private const string _toggleStyle = "button-toggle-2";
    private const string _redDarkStyle = "button-red-dark";
    private const string _yellowDarkStyle = "button-yellow-dark";
    private const string _greenDarkStyle = "button-green-dark";
    private const string _blueDarkStyle = "button-blue-dark";
    private const string _purpleDarkStyle = "button-purple-dark";

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
        _toggleButton.ToggleInClassList(_toggleStyle);
        OnToggleLineEvent?.Invoke();
    }

    private void ClickRedButton()
    {
        _isLineSelected[0] = !_isLineSelected[0];
        _lineButtons[0].RemoveFromClassList(_redDarkStyle);
        
        _lineButtons[1].AddToClassList(_yellowDarkStyle);
        _lineButtons[2].AddToClassList(_greenDarkStyle);
        _lineButtons[3].AddToClassList(_blueDarkStyle);
        _lineButtons[4].AddToClassList(_purpleDarkStyle);

        for (int i = 1; i < 5; ++i)
            _isLineSelected[i] = false;

        if (ResourceManager.Instance.TryUseLineGroup(LineGroupType.Red))
            OnRedLineEvent?.Invoke();
    }

    private void ClickYellowButton()
    {
        _isLineSelected[1] = !_isLineSelected[1];
        _lineButtons[1].RemoveFromClassList(_yellowDarkStyle);
        
        _lineButtons[0].AddToClassList(_redDarkStyle);
        _lineButtons[2].AddToClassList(_greenDarkStyle);
        _lineButtons[3].AddToClassList(_blueDarkStyle);
        _lineButtons[4].AddToClassList(_purpleDarkStyle);

        for (int i = 0; i < 5; ++i)
            _isLineSelected[i] = i == 1 && _isLineSelected[i];

        if (ResourceManager.Instance.TryUseLineGroup(LineGroupType.Yellow))
            OnYellowLineEvent?.Invoke();
    }

    private void ClickGreenButton()
    {
        _isLineSelected[2] = !_isLineSelected[2];
        _lineButtons[2].RemoveFromClassList(_greenDarkStyle);
        
        _lineButtons[0].AddToClassList(_redDarkStyle);
        _lineButtons[1].AddToClassList(_yellowDarkStyle);
        _lineButtons[3].AddToClassList(_blueDarkStyle);
        _lineButtons[4].AddToClassList(_purpleDarkStyle);

        for (int i = 0; i < 5; ++i)
            _isLineSelected[i] = i == 2 && _isLineSelected[i];

        if (ResourceManager.Instance.TryUseLineGroup(LineGroupType.Green))
            OnGreenLineEvent?.Invoke();
    }

    private void ClickBlueButton()
    {
        _isLineSelected[3] = !_isLineSelected[3];
        _lineButtons[3].RemoveFromClassList(_blueDarkStyle);
        
        _lineButtons[0].AddToClassList(_redDarkStyle);
        _lineButtons[1].AddToClassList(_yellowDarkStyle);
        _lineButtons[2].AddToClassList(_greenDarkStyle);
        _lineButtons[4].AddToClassList(_purpleDarkStyle);

        for (int i = 0; i < 5; ++i)
            _isLineSelected[i] = i == 3 && _isLineSelected[i];

        if (ResourceManager.Instance.TryUseLineGroup(LineGroupType.Blue))
            OnBlueLineEvent?.Invoke();
    }

    private void ClickPurpleButton()
    {
        _isLineSelected[4] = !_isLineSelected[4];
        _lineButtons[4].RemoveFromClassList(_purpleDarkStyle);
        
        _lineButtons[0].AddToClassList(_redDarkStyle);
        _lineButtons[1].AddToClassList(_yellowDarkStyle);
        _lineButtons[2].AddToClassList(_greenDarkStyle);
        _lineButtons[3].AddToClassList(_blueDarkStyle);

        for (int i = 1; i < 4; ++i)
            _isLineSelected[i] = false;

        if (ResourceManager.Instance.TryUseLineGroup(LineGroupType.Purple))
            OnPurpleLineEvent?.Invoke();
    }
}