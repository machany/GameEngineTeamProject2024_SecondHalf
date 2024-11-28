using System;
using UnityEngine;
using UnityEngine.UIElements;

public class VehicleUI : UIToolkit, IInputable
{
    [field: SerializeField] public InputReader InputCompo { get; private set; }

    public static Action<bool> OnCarSelected;
    public static Action<bool> OnTruckSelected;
    public static Action<bool> OnTrailerSelected;

    private const string _carStr = "Button_Car";
    private const string _truckStr = "Button_Truck";
    private const string _trailerStr = "Button_Trailer";

    private const string _carCountStr = "Label_CarCount";
    private const string _truckCountStr = "Label_TruckCount";
    private const string _trailerCountStr = "Label_TrailerCount";

    private Button _carButton;
    private Button _truckButton;
    private Button _trailerButton;

    private Label _carCountLabel;
    private Label _truckCountLabel;
    private Label _trailerCountLabel;

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

    private void Update()
    {
        UpdateVehicleCount();
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _carButton = root.Q<Button>(_carStr);
        _truckButton = root.Q<Button>(_truckStr);
        _trailerButton = root.Q<Button>(_trailerStr);
        
        _carCountLabel = root.Q<Label>(_carCountStr);
        _truckCountLabel = root.Q<Label>(_truckCountStr);
        _trailerCountLabel = root.Q<Label>(_trailerCountStr);
    }

    private void OnCarEvent()
    {
        _isCarSelected = !_isCarSelected;
        _isTruckSelected = false;
        _isTrailerSelected = false;
        OnCarSelected?.Invoke(_isCarSelected);
    }

    private void OnTruckEvent()
    {
        _isTruckSelected = !_isTruckSelected;
        _isCarSelected = false;
        _isTrailerSelected = false;
        OnTruckSelected?.Invoke(_isTruckSelected);
    }

    private void OnTrailerEvent()
    {
        _isTrailerSelected = !_isTrailerSelected;
        _isCarSelected = false;
        _isTruckSelected = false;
        OnTrailerSelected?.Invoke(_isTrailerSelected);
    }
    
    private void UpdateVehicleCount()
    {
        _carCountLabel.text = ResourceManager.Instance.GetVehicleValue(VehicleType.car).ToString();
        _truckCountLabel.text = ResourceManager.Instance.GetVehicleValue(VehicleType.truck).ToString();
        _trailerCountLabel.text = ResourceManager.Instance.GetVehicleValue(VehicleType.trailer).ToString();
    }
}