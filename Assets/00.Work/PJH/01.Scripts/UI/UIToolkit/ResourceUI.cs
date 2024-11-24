using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceUI : UIToolkit
{
    [SerializeField] private DistributionCenter center;
    
    private const string _resourceStr = "VisualElement_Resource";
    private const string _toggleStr = "Button_Toggle";
    
    private readonly string[] _countStr =
    {
        "Label_Red", "Label_Yellow", "Label_Green", "Label_Blue", "Label_Purple", "Label_Bridge"
    };

    private const string _toggleStyle = "visual-element-resource-toggle";

    private VisualElement _resourceVisualElement;
    
    private Button _toggleButton;
    
    private Label[] _countLabels = new Label[6];

    private bool _isToggle;

    private void OnEnable()
    {
        GetUIElements();

        _toggleButton.clicked += ClickToggleButton;
    }

    private void OnDisable()
    {
        _toggleButton.clicked -= ClickToggleButton;
    }

    private void Update()
    {
        SetReSourceLabel();
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _resourceVisualElement = root.Q<VisualElement>(_resourceStr);
        
        _toggleButton = root.Q<Button>(_toggleStr);

        for (int i = 0; i < 6; ++i)
            _countLabels[i] = root.Q<Label>(_countStr[i]);
    }
    
    private void SetReSourceLabel()
    {
        _countLabels[0].text = center.Storage[ResourceType.Red].ToString();
        _countLabels[1].text = center.Storage[ResourceType.Yellow].ToString();
        _countLabels[2].text = center.Storage[ResourceType.Green].ToString();
        _countLabels[3].text = center.Storage[ResourceType.Blue].ToString();
        _countLabels[4].text = center.Storage[ResourceType.Purple].ToString();
        //_countLabels[5].text = 브릿지 정보 가져오기
    }
    
    private void ClickToggleButton()
    {
        _isToggle = !_isToggle;
        
        Toggle();
    }
    
    private void Toggle()
    {
        _resourceVisualElement.ToggleInClassList(_toggleStyle);
    }
}