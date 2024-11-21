using UnityEngine;
using UnityEngine.UIElements;

public class OptionUI : UIToolkit, IInputable, IDraggable
{
    [field: SerializeField] public InputReader InputCompo { get; private set; }
    
    private const string _optionStr = "Button_Option";
    private const string _exitStr = "Button_Exit";
    private const string _settingStr = "VisualElement_Setting";

    private Button _optionButton;
    private Button _exitButton;
    
    private VisualElement _settingVisualElement;

    private Vector2 _defaultMousePosition;

    private bool _isDrag;
    
    private void OnEnable()
    {
        GetUIElements();

        _settingVisualElement.style.display = DisplayStyle.None;

        _optionButton.clicked += ClickOptionButton;
        _exitButton.clicked += ClickExitButton;
        
        InputCompo.OnOptionEvent += OnOptionEvent;

        _settingVisualElement.RegisterCallback<MouseDownEvent>(OnMouseDown);
        _settingVisualElement.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        _settingVisualElement.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    private void OnDisable()
    {
        _optionButton.clicked -= ClickOptionButton;
        _exitButton.clicked -= ClickExitButton;
        
        InputCompo.OnOptionEvent -= OnOptionEvent;

        _settingVisualElement.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        _settingVisualElement.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        _settingVisualElement.UnregisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _optionButton = root.Q<Button>(_optionStr);
        _exitButton = root.Q<Button>(_exitStr);
        
        _settingVisualElement = root.Q<VisualElement>(_settingStr);
    }

    private void ClickOptionButton()
    {
        _settingVisualElement.style.display = DisplayStyle.Flex;
        Time.timeScale = 0;
    }

    private void ClickExitButton()
    {
        _settingVisualElement.style.display = DisplayStyle.None;
        _isDrag = false;
        Time.timeScale = 1;
    }

    private void OnOptionEvent()
    {
        if (_settingVisualElement.style.display == DisplayStyle.Flex)
        {
            _settingVisualElement.style.display = DisplayStyle.None;
            Time.timeScale = 1;
        }
        else
        {
            _settingVisualElement.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
        }
    }

    public void OnMouseDown(MouseDownEvent downEvent)
    {
        _isDrag = true;
        _settingVisualElement.CaptureMouse();

        _defaultMousePosition = downEvent.localMousePosition;
        Vector2 offset = downEvent.mousePosition - root.worldBound.position - _defaultMousePosition;

        _settingVisualElement.style.position = Position.Absolute;
        _settingVisualElement.style.left = offset.x;
        _settingVisualElement.style.top = offset.y;

        float width = _settingVisualElement.worldBound.width;
        _settingVisualElement.style.width = new Length(width, LengthUnit.Pixel);
    }

    public void OnMouseMove(MouseMoveEvent moveEvent)
    {
        if (!_isDrag || !_settingVisualElement.HasMouseCapture())
            return;

        Vector2 movePos = moveEvent.localMousePosition - _defaultMousePosition;
        float x = _settingVisualElement.layout.x;
        float y = _settingVisualElement.layout.y;

        _settingVisualElement.style.left = x + movePos.x;
        _settingVisualElement.style.top = y + movePos.y;
    }

    public void OnMouseUp(MouseUpEvent upEvent)
    {
        if (!_isDrag || !_settingVisualElement.HasMouseCapture())
            return;

        _isDrag = false;
        _settingVisualElement.ReleaseMouse();

        if (ScreenOut(_settingVisualElement))
        {
            _settingVisualElement.style.position = Position.Relative;
            _settingVisualElement.style.left = StyleKeyword.Null;
            _settingVisualElement.style.top = StyleKeyword.Null;
            _settingVisualElement.style.width = StyleKeyword.Null;
        }
    }

    private bool ScreenOut(VisualElement visualElement)
    {
        Rect worldBound = visualElement.worldBound;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (worldBound.xMin < 0 || worldBound.xMax > screenWidth || worldBound.yMin < 0 ||
            worldBound.yMax > screenHeight)
            return true;

        return false;
    }
}