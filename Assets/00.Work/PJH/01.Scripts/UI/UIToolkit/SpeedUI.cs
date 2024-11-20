using UnityEngine;
using UnityEngine.UIElements;

public class SpeedUI : MonoBehaviour, IUIToolkitable
{
    private const string _dateStr = "VisualElement_Date";
    private const string _speedStr = "VisualElement_Speed";
    private const string _speedToggleStr = "Button_SpeedToggle";
    private const string _stopStr = "Button_Stop";
    private const string _speed1Str = "Button_Speed1";
    private const string _speed2Str = "Button_Speed2";
    private const string _speed3Str = "Button_Speed3";

    [SerializeField] private InputReader _inputReader;

    private VisualElement _root;

    private VisualElement _dateVisualElement;
    private VisualElement _speedVisualElement;
    private Button _speedToggleButton;
    private Button _stopButton;
    private Button _speed1Button;
    
    public void GetUIElements()
    {
    }
}