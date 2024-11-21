using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VehicleUI : UIToolkit, IDraggable
{
    private const string _vehicleStr = "VisualElement_Vehicle";
    private const string _carStr = "VisualElement_Car";
    private const string _truckStr = "VisualElement_Truck";
    private const string _trailerStr = "VisualElement_Trailer";

    private VisualElement _vehicleVisualElement;
    private VisualElement _carVisualElement;
    private VisualElement _truckVisualElement;
    private VisualElement _trailerVisualElement;

    private Vector2 _defaultMousePosition;
    
    private bool _isDrag;
    
    private void OnEnable()
    {
        GetUIElements();
        
        _carVisualElement.RegisterCallback<MouseDownEvent>(OnMouseDown);
        _carVisualElement.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        _carVisualElement.RegisterCallback<MouseUpEvent>(OnMouseUp);
        
        _truckVisualElement.RegisterCallback<MouseDownEvent>(OnMouseDown);
        _truckVisualElement.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        _truckVisualElement.RegisterCallback<MouseUpEvent>(OnMouseUp);
        
        _trailerVisualElement.RegisterCallback<MouseDownEvent>(OnMouseDown);
        _trailerVisualElement.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        _trailerVisualElement.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }
    
    private void OnDisable()
    {
        _carVisualElement.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        _carVisualElement.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        _carVisualElement.UnregisterCallback<MouseUpEvent>(OnMouseUp);
        
        _truckVisualElement.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        _truckVisualElement.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        _truckVisualElement.UnregisterCallback<MouseUpEvent>(OnMouseUp);
        
        _trailerVisualElement.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        _trailerVisualElement.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        _trailerVisualElement.UnregisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _vehicleVisualElement = root.Q<VisualElement>(_vehicleStr);
        _carVisualElement = root.Q<VisualElement>(_carStr);
        _truckVisualElement = root.Q<VisualElement>(_truckStr);
        _trailerVisualElement = root.Q<VisualElement>(_trailerStr);
    }

    public void OnMouseDown(MouseDownEvent downEvent)
    {
        Debug.Log("시발련아");
        _isDrag = true;
        VisualElement target = downEvent.target as VisualElement;
        
        target.CaptureMouse();
        root.Add(target);
        
        _defaultMousePosition = downEvent.localMousePosition;
        Vector2 offset = downEvent.mousePosition - root.worldBound.position - _defaultMousePosition;

        target.style.position = Position.Absolute;
        target.style.left = offset.x;
        target.style.top = offset.y;

        float width = target.worldBound.width;
        target.style.width = new Length(width, LengthUnit.Pixel);
    }

    public void OnMouseMove(MouseMoveEvent moveEvent)
    {
        VisualElement target = moveEvent.target as VisualElement;
        
        if (!_isDrag || !target.HasMouseCapture())
            return;
        
        Vector2 movePos = moveEvent.localMousePosition - _defaultMousePosition;
        float x = target.layout.x;
        float y = target.layout.y;
        
        target.style.left = x + movePos.x;
        target.style.top = y + movePos.y;
    }

    public void OnMouseUp(MouseUpEvent upEvent)
    {
        VisualElement target = upEvent.target as VisualElement;
        
        if (!_isDrag || !target.HasMouseCapture())
            return;
        
        _isDrag = false;
        target.ReleaseMouse();
        _vehicleVisualElement.Add(target);
        
        target.style.position = Position.Relative;
        target.style.left = StyleKeyword.Null;
        target.style.top = StyleKeyword.Null;
        target.style.width = StyleKeyword.Null;
    }
}
