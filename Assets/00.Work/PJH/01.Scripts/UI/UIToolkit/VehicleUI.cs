using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VehicleUI : UIToolkit, IInputable
{
    [field: SerializeField] public InputReader InputCompo { get; private set; }
    
    private const string _carStr = "Button_Car";
    private const string _truckStr = "Button_Truck";
    private const string _trailerStr = "Button_Trailer";
    
    private Button _carButton;
    private Button _truckButton;
    private Button _trailerButton;
    
    private bool _isCarSelected;
    private bool _isTruckSelected;
    private bool _isTrailerSelected;
    
    private void OnEnable()
    {
        GetUIElements();

        _carButton.clicked += OnCarEvent;
        _truckButton.clicked += OnTruckEvent;
        _trailerButton.clicked += OnTrailerEvent;
        
        InputCompo.OnCarEvent += OnCarEvent;
        InputCompo.OnTruckEvent += OnTruckEvent;
        InputCompo.OnTrailerEvent += OnTrailerEvent;
    }
    
    private void OnDisable()
    {
        _carButton.clicked -= OnCarEvent;
        _truckButton.clicked -= OnTruckEvent;
        _trailerButton.clicked -= OnTrailerEvent;
        
        InputCompo.OnCarEvent -= OnCarEvent;
        InputCompo.OnTruckEvent -= OnTruckEvent;
        InputCompo.OnTrailerEvent -= OnTrailerEvent;
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();
        
        _carButton = root.Q<Button>(_carStr);
        _truckButton = root.Q<Button>(_truckStr);
        _trailerButton = root.Q<Button>(_trailerStr);
    }
    
    private void OnCarEvent()
    {
        _isCarSelected = !_isCarSelected;
    }
    
    private void OnTruckEvent()
    {
        _isTruckSelected = !_isTruckSelected;
    }
    
    private void OnTrailerEvent()
    {
        _isTrailerSelected = !_isTrailerSelected;
    }
}