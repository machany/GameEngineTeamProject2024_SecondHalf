using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OptionUI : UIToolkit, IInputable, IDraggable
{
    [SerializeField] private SoundChannelSO musicSound;
    [SerializeField] private SoundChannelSO effectSound;

    public static Action<bool> OnOptionPanel;

    [field: SerializeField] public InputReader InputCompo { get; private set; }

    private const string _optionStr = "Button_Option";
    private const string _exitStr = "Button_Exit";
    private const string _settingStr = "VisualElement_Setting";
    private const string _masterStr = "Slider_MasterVolume";
    private const string _musicStr = "Slider_MusicVolume";
    private const string _effectStr = "Slider_EffectVolume";
    private const string _screenStr = "DropdownField_Screen";
    private const string _gameExitStr = "Button_GameExit";

    private Button _optionButton;
    private Button _exitButton;
    private Button _gameExitButton;

    private VisualElement _settingVisualElement;

    private Slider _masterVolumeSlider;
    private Slider _musicVolumeSlider;
    private Slider _effectVolumeSlider;

    private DropdownField _screenDropdownField;

    private Vector2 _defaultMousePosition;

    private bool _isDrag;

    private void OnEnable()
    {
        GetUIElements();
        Initialize();

        _settingVisualElement.style.display = DisplayStyle.None;

        _optionButton.clicked += ClickOptionButton;
        _exitButton.clicked += ClickExitButton;
        _gameExitButton.clicked += ClickGameExitButton;

        InputCompo.OnOptionEvent += OnOptionEvent;

        _settingVisualElement.RegisterCallback<MouseDownEvent>(OnMouseDown);
        _settingVisualElement.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        _settingVisualElement.RegisterCallback<MouseUpEvent>(OnMouseUp);

        _masterVolumeSlider.RegisterValueChangedCallback(MasterSlider);
        _musicVolumeSlider.RegisterValueChangedCallback(MusicSlider);
        _effectVolumeSlider.RegisterValueChangedCallback(EffectSlider);

        _screenDropdownField.RegisterValueChangedCallback(ScreenDropdown);
    }

    private void OnDisable()
    {
        _optionButton.clicked -= ClickOptionButton;
        _exitButton.clicked -= ClickExitButton;
        _gameExitButton.clicked -= ClickGameExitButton;

        InputCompo.OnOptionEvent -= OnOptionEvent;

        _settingVisualElement.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        _settingVisualElement.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        _settingVisualElement.UnregisterCallback<MouseUpEvent>(OnMouseUp);

        _masterVolumeSlider.UnregisterValueChangedCallback(MasterSlider);
        _musicVolumeSlider.UnregisterValueChangedCallback(MusicSlider);
        _effectVolumeSlider.UnregisterValueChangedCallback(EffectSlider);

        _screenDropdownField.UnregisterValueChangedCallback(ScreenDropdown);
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _optionButton = root.Q<Button>(_optionStr);
        _exitButton = root.Q<Button>(_exitStr);
        _gameExitButton = root.Q<Button>(_gameExitStr);

        _settingVisualElement = root.Q<VisualElement>(_settingStr);

        _masterVolumeSlider = root.Q<Slider>(_masterStr);
        _musicVolumeSlider = root.Q<Slider>(_musicStr);
        _effectVolumeSlider = root.Q<Slider>(_effectStr);

        _screenDropdownField = root.Q<DropdownField>(_screenStr);
    }

    private void Initialize()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 50);
        _masterVolumeSlider.value = masterVolume;

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 50);
        _musicVolumeSlider.value = musicVolume;

        float effectVolume = PlayerPrefs.GetFloat("EffectVolume", 50);
        _effectVolumeSlider.value = effectVolume;
        
        //masterSound.volume = masterVolume;
        musicSound.volume = musicVolume;
        effectSound.volume = effectVolume;

        _screenDropdownField.choices = new List<string> { "전체 화면", "창 화면" };

        string screenValue = PlayerPrefs.GetString("ScreenSetting", "전체 화면");

        if (_screenDropdownField.choices.Contains(screenValue))
            _screenDropdownField.value = screenValue;
        else
            Debug.LogWarning($"screen value {screenValue} is not found in choices.");
    }

    private void ClickOptionButton()
    {
        _settingVisualElement.style.display = DisplayStyle.Flex;
        Time.timeScale = 0;
        OnOptionPanel?.Invoke(true);
    }

    private void ClickExitButton()
    {
        _settingVisualElement.style.display = DisplayStyle.None;
        _isDrag = false;
        Time.timeScale = 1;
        OnOptionPanel?.Invoke(false);
    }

    private void ClickGameExitButton()
    {
        // 씬 전환 페이드 ㅇ니아웃
        FadeManager.FadeOut(() => SceneManager.LoadScene("Title"));
    }

    private void OnOptionEvent()
    {
        if (_settingVisualElement.style.display == DisplayStyle.Flex)
            ClickExitButton();
        else
            ClickOptionButton();
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

    private void MasterSlider(ChangeEvent<float> changeEvent)
    {
        // 사운드 세팅
        musicSound.volume = changeEvent.newValue;
        effectSound.volume = changeEvent.newValue;
        PlayerPrefs.SetFloat("MasterVolume", changeEvent.newValue);
    }

    private void MusicSlider(ChangeEvent<float> changeEvent)
    {
        // 사운드 세팅
        musicSound.volume = changeEvent.newValue;
        PlayerPrefs.SetFloat("MusicVolume", changeEvent.newValue);
    }

    private void EffectSlider(ChangeEvent<float> changeEvent)
    {
        // 사운드 세팅
        effectSound.volume = changeEvent.newValue;
        PlayerPrefs.SetFloat("EffectVolume", changeEvent.newValue);
    }

    private void ScreenDropdown(ChangeEvent<string> changeEvent)
    {
        switch (changeEvent.newValue)
        {
            case "전체화면":
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                PlayerPrefs.SetString("ScreenSetting", changeEvent.newValue);
                break;

            case "창 화면":
                Screen.fullScreenMode = FullScreenMode.Windowed;
                PlayerPrefs.SetString("ScreenSetting", changeEvent.newValue);
                break;
        }
    }
}