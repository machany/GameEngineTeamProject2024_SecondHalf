using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, Controls.IPlayerActions
{
    public event Action OnTimeStopEvent;
    public event Action OnTimeSpeedUpEvent;
    public event Action OnTimeSpeedDownEvent;

    public event Action OnOptionEvent;
    public event Action OnHideUIEvent;
    public event Action OnToggleLineEvent;

    public event Action OnCarEvent;
    public event Action OnTruckEvent;
    public event Action OnTrailerEvent;

    public event Action OnRedLineEvent;
    public event Action OnYellowLineEvent;
    public event Action OnGreenLineEvent;
    public event Action OnBlueLineEvent;
    public event Action OnPurpleLineEvent;

    public Vector2 InputVector { get; private set; }

    private Controls _controls;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }
        
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    public void OnCameraMove(InputAction.CallbackContext context)
    {
        InputVector = context.ReadValue<Vector2>();
    }

    public void OnTimeStop(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnTimeStopEvent?.Invoke();
    }

    public void OnTimeSpeedUp(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnTimeSpeedUpEvent?.Invoke();
    }

    public void OnTimeSpeedDown(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnTimeSpeedDownEvent?.Invoke();
    }

    public void OnOption(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnOptionEvent?.Invoke();
    }

    public void OnHideUI(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnHideUIEvent?.Invoke();
    }

    public void OnToggleLine(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnToggleLineEvent?.Invoke();
    }

    public void OnCar(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnCarEvent?.Invoke();
    }

    public void OnTruck(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnTruckEvent?.Invoke();
    }

    public void OnTrailer(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnTrailerEvent?.Invoke();
    }

    public void OnRedLine(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnRedLineEvent?.Invoke();
    }

    public void OnYellowLine(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnYellowLineEvent?.Invoke();
    }
    public void OnGreenLine(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnGreenLineEvent?.Invoke();
    }

    public void OnBlueLine(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnBlueLineEvent?.Invoke();
    }

    public void OnPurpleLine(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnPurpleLineEvent?.Invoke();
    }
}