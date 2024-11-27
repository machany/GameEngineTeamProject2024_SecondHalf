using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuUI : UIToolkit
{
    private readonly string[] _stageGroupStr = { "GroupBox_Korea", "GroupBox_China" };
    private readonly string[] _stageLabelStr = {"Label_Korea", "Label_China"};
    private readonly string[] _stageButtonStr = {"Button_Korea", "Button_China"};

    // 타이틀
    private VisualElement _titleVisualElement;
    private Button _titleExitButton;

    // 메뉴
    private VisualElement _menuVisualElement;
    private Button _startButton;
    private Button _guideButton;
    private Button _settingButton;
    private Button _exitButton;
    private Button _menuExitButton;

    // 설정
    private VisualElement _settingVisualElement;
    private Button _settingExitButton;
    private Slider _masterVolumeSlider;
    private Slider _musicVolumeSlider;
    private Slider _effectVolumeSlider;
    private DropdownField _screenDropdown;

    // 스테이지
    private VisualElement _stageVisualElement;
    private Button _stageExitButton;
    private ScrollView _stageScrollView;
    private GroupBox[] _stageGroupBoxes = new GroupBox[5];
    private Label[] _stageLabels = new Label[5];
    private Button[] _stageButtons = new Button[5];

    private void OnEnable()
    {
        GetUIElements();
        
        LoadHighScore();
        
        LoadStageInformation();
        
        Initialize();

        _stageScrollView.AddToClassList("hidden-scrollbars");

        _titleExitButton.clicked += ClickTitleExitButton;

        _startButton.clicked += ClickStartButton;
        _guideButton.clicked += ClickGuideButton;
        _settingButton.clicked += ClickSettingButton;
        _exitButton.clicked += ClickExitButton;
        _menuExitButton.clicked += ClickMenuExitButton;

        _settingExitButton.clicked += ClickSettingExitButton;
        _masterVolumeSlider.RegisterValueChangedCallback(MasterSlider);
        _musicVolumeSlider.RegisterValueChangedCallback(MusicSlider);
        _effectVolumeSlider.RegisterValueChangedCallback(EffectSlider);
        _screenDropdown.RegisterValueChangedCallback(ScreenDropdown);

        _stageExitButton.clicked += ClickStageExitButton;

        _stageButtons[0].clicked += ClickKoreaButton;
        _stageButtons[1].clicked += ClickChinaButton;
    }

    private void OnDisable()
    {
        _titleExitButton.clicked -= ClickTitleExitButton;

        _startButton.clicked -= ClickStartButton;
        _guideButton.clicked -= ClickGuideButton;
        _settingButton.clicked -= ClickSettingButton;
        _exitButton.clicked -= ClickExitButton;
        _menuExitButton.clicked -= ClickMenuExitButton;

        _settingExitButton.clicked -= ClickSettingExitButton;
        _masterVolumeSlider.UnregisterValueChangedCallback(MasterSlider);
        _musicVolumeSlider.UnregisterValueChangedCallback(MusicSlider);
        _effectVolumeSlider.UnregisterValueChangedCallback(EffectSlider);
        _screenDropdown.UnregisterValueChangedCallback(ScreenDropdown);

        _stageExitButton.clicked -= ClickStageExitButton;
        
        _stageButtons[0].clicked -= ClickKoreaButton;
        _stageButtons[1].clicked -= ClickChinaButton;
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _titleVisualElement = root.Q<VisualElement>("VisualElement_Title");
        _titleExitButton = root.Q<Button>("Button_TitleExit");

        _menuVisualElement = root.Q<VisualElement>("VisualElement_Menu");
        _startButton = root.Q<Button>("Button_Start");
        _guideButton = root.Q<Button>("Button_Guide");
        _settingButton = root.Q<Button>("Button_Setting");
        _exitButton = root.Q<Button>("Button_Exit");
        _menuExitButton = root.Q<Button>("Button_MenuExit");

        _settingVisualElement = root.Q<VisualElement>("VisualElement_Setting");
        _settingExitButton = root.Q<Button>("Button_SettingExit");
        _masterVolumeSlider = root.Q<Slider>("Slider_MasterVolume");
        _musicVolumeSlider = root.Q<Slider>("Slider_MusicVolume");
        _effectVolumeSlider = root.Q<Slider>("Slider_EffectVolume");
        _screenDropdown = root.Q<DropdownField>("DropdownField_Screen");

        _stageVisualElement = root.Q<VisualElement>("VisualElement_Stage");
        _stageExitButton = root.Q<Button>("Button_StageExit");
        _stageScrollView = root.Q<ScrollView>("ScrollView_Stage");

        for (int i = 0; i < _stageGroupStr.Length; ++i)
        {
            _stageGroupBoxes[i] = root.Q<GroupBox>(_stageGroupStr[i]);
            _stageLabels[i] = root.Q<Label>(_stageLabelStr[i]);
            _stageButtons[i] = root.Q<Button>(_stageButtonStr[i]);
        }
    }
    
    private void Initialize()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 50);
        _masterVolumeSlider.value = masterVolume;

        float musicVolume = PlayerPrefs.GetFloat("MasterVolume", 50);
        _musicVolumeSlider.value = musicVolume;

        float effectVolume = PlayerPrefs.GetFloat("EffectVolume", 50);
        _effectVolumeSlider.value = effectVolume;

        // 사운드 매니저 값 세팅
        
        string screenValue = PlayerPrefs.GetString("ScreenSetting", "전체 화면");

        if (_screenDropdown.choices.Contains(screenValue))
            _screenDropdown.value = screenValue;
        else
        {
            _screenDropdown.choices = new List<string> { "전체 화면", "창 화면" };
            _screenDropdown.value = screenValue;
            Debug.LogWarning($"screen value {screenValue} is not found in choices.");
        }
    }

    #region 타이틀

    private void ClickTitleExitButton()
    {
        _titleVisualElement.RemoveFromClassList("visual-element-title-1");
        _menuVisualElement.RemoveFromClassList("visual-element-menu-3");
    }

    #endregion

    #region 메뉴

    private void ClickStartButton()
    {
        // 이동
        _menuVisualElement.AddToClassList("visual-element-menu-2");
        _settingVisualElement.AddToClassList("visual-element-setting-2");
        _stageVisualElement.AddToClassList("visual-element-stage-1");
    }

    private void ClickGuideButton()
    {
        // 튜토리얼 씬 전환 FadeManager.FadeIn
    }

    private void ClickSettingButton()
    {
        // 이동
        _stageVisualElement.AddToClassList("visual-element-stage-2");
        _menuVisualElement.AddToClassList("visual-element-menu-1");
        _settingVisualElement.AddToClassList("visual-element-setting-1");
    }

    private void ClickExitButton()
    {
        FadeManager.FadeOut(Application.Quit);
    }

    private void ClickMenuExitButton()
    {
        _titleVisualElement.AddToClassList("visual-element-title-1");
        _menuVisualElement.AddToClassList("visual-element-menu-3");
    }

    #endregion

    #region 설정

    private void ClickSettingExitButton()
    {
        // 이동
        _stageVisualElement.RemoveFromClassList("visual-element-stage-2");
        _menuVisualElement.RemoveFromClassList("visual-element-menu-1");
        _settingVisualElement.RemoveFromClassList("visual-element-setting-1");
    }
    
    private void MasterSlider(ChangeEvent<float> changeEvent)
    {
        // 사운드 세팅
        PlayerPrefs.SetFloat("MasterVolume", changeEvent.newValue);
    }

    private void MusicSlider(ChangeEvent<float> changeEvent)
    {
        // 사운드 세팅
        PlayerPrefs.SetFloat("MusicVolume", changeEvent.newValue);
    }

    private void EffectSlider(ChangeEvent<float> changeEvent)
    {
        // 사운드 세팅
        PlayerPrefs.SetFloat("EffectVolume", changeEvent.newValue);
    }

    private void ScreenDropdown(ChangeEvent<string> changeEvent)
    {
        switch (changeEvent.newValue)
        {
            case "전체 화면":
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                PlayerPrefs.SetString("ScreenSetting", changeEvent.newValue);
                break;
            
            case "창 화면":
                Screen.fullScreenMode = FullScreenMode.Windowed;
                PlayerPrefs.SetString("ScreenSetting", changeEvent.newValue);
                break;
        }
    }

    #endregion

    #region 스테이지

    private void ClickStageExitButton()
    {
        _menuVisualElement.RemoveFromClassList("visual-element-menu-2");
        _settingVisualElement.RemoveFromClassList("visual-element-setting-2");
        _stageVisualElement.RemoveFromClassList("visual-element-stage-1");
    }
    
    private void ClickKoreaButton()
    {
        // 씬이동
    }
    
    private void ClickChinaButton()
    {
        // 씬이동
    }
    
    private void LoadStageInformation()
    {
        //SaveGame.Instance.Save();
    }
    
    private void LoadHighScore()
    {
        // 파일 입출력으로 스코어 업데이트
        //SaveGame.Instance.Load();
    }

    #endregion
}