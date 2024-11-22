using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceUI : UIToolkit
{
    private const string _resourceStr = "VisualElement_Resource";
    private const string _typeStr = "GroupBox_Type"; // 타입 5개 가져오기
    private const string _colorStr = "VisualElement_Color"; // 5개
    private const string _countStr = "Label_Count"; // 5개

    private VisualElement _resourceVisualElement;

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _resourceVisualElement = root.Q<VisualElement>(_resourceStr);
    }
}
