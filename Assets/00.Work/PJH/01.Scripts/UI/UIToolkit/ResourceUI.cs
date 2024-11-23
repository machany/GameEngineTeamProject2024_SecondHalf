using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceUI : UIToolkit
{
    private const string _resourceStr = "VisualElement_Resource";

    private readonly string[] _typeStr =
    {
        "GroupBox_Red", "GroupBox_Yellow", "GroupBox_Green", "GroupBox_Blue", "GroupBox_Purple"
    };

    private readonly string[] _colorStr =
    {
        "VisualElement_Red", "VisualElement_Yellow", "VisualElement_Green", "VisualElement_Blue", "VisualElement_Purple"
    };

    private readonly string[] _countStr =
    {
        "Label_Red", "Label_Yellow", "Label_Green", "Label_Blue", "Label_Purple"
    };

    private VisualElement _resourceVisualElement;
    
    private GroupBox[] _typeGroupBoxes = new GroupBox[5];
    private VisualElement[] _colorVisualElements = new VisualElement[5];
    private Label[] _countLabels = new Label[5];

    private void OnEnable()
    {
        GetUIElements();
    }

    private void OnDisable()
    {
        
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _resourceVisualElement = root.Q<VisualElement>(_resourceStr);

        for (int i = 0; i < 5; ++i)
        {
            _typeGroupBoxes[i] = root.Q<GroupBox>(_typeStr[i]);
            _colorVisualElements[i] = root.Q<VisualElement>(_colorStr[i]);
            _countLabels[i] = root.Q<Label>(_countStr[i]);
        }
    }
}