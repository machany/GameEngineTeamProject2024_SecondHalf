using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuUI : UIToolkit
{
    private readonly string[] _stageGroupStr = {"GroupBox_Korea", "GroupBox_Korea"};
    
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

    // 스테이지
    private VisualElement _stageVisualElement;
    private Button _stageExitButton;
    private ScrollView _stageScrollView;
    private List<GroupBox> _stageGroupBoxes = new();

    private void OnEnable()
    {
        GetUIElements();

        _stageScrollView.AddToClassList("hidden-scrollbars");

        _titleExitButton.clicked += ClickTitleExitButton;

        _startButton.clicked += ClickStartButton;
        _guideButton.clicked += ClickGuideButton;
        _settingButton.clicked += ClickSettingButton;
        _exitButton.clicked += ClickExitButton;
        _menuExitButton.clicked += ClickMenuExitButton;

        _settingExitButton.clicked += ClickSettingExitButton;

        _stageExitButton.clicked += ClickStageExitButton;
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

        _stageExitButton.clicked -= ClickStageExitButton;
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

        _stageVisualElement = root.Q<VisualElement>("VisualElement_Stage");
        _stageExitButton = root.Q<Button>("Button_StageExit");
        _stageScrollView = root.Q<ScrollView>("ScrollView_Stage");

        //for (int i = 0; i < _stageGroupBoxes.Count; ++i)
          //  _stageGroupBoxes[i] = root.Q<GroupBox>(_stageGroupStr[i]);
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

    #endregion

    #region 스테이지

    private void ClickStageExitButton()
    {
        _menuVisualElement.RemoveFromClassList("visual-element-menu-2");
        _settingVisualElement.RemoveFromClassList("visual-element-setting-2");
        _stageVisualElement.RemoveFromClassList("visual-element-stage-1");
    }

    #endregion
}