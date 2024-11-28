using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RewardUI : UIToolkit
{
    private const string _dayStr = "Label_Day";

    private const string _oneResStr = "Button_OneRes";
    private const string _oneNameStr = "Label_OneName";
    private const string _oneDescStr = "Label_OneDesc";
    private const string _oneCountStr = "Label_OneCount";

    private const string _twoResStr = "Button_TwoRes";
    private const string _twoNameStr = "Label_TwoName";
    private const string _twoDescStr = "Label_TwoDesc";
    private const string _twoCountStr = "Label_TwoCount";

    private const string _threeResStr = "Button_ThreeRes";
    private const string _threeNameStr = "Label_ThreeName";
    private const string _threeDescStr = "Label_ThreeDesc";
    private const string _threeCountStr = "Label_ThreeCount";

    private Label _dayLabel;

    private Button _oneResButton;
    private Label _oneNameLabel;
    private Label _oneDescLabel;
    private Label _oneCountLabel;

    private Button _twoResButton;
    private Label _twoNameLabel;
    private Label _twoDescLabel;
    private Label _twoCountLabel;

    private Button _threeResButton;
    private Label _threeNameLabel;
    private Label _threeDescLabel;
    private Label _threeCountLabel;

    private RewardItemSO _oneItemSO;
    private RewardItemSO _twoItemSO;
    private RewardItemSO _threeItemSO;

    private void Awake()
    {
        SpeedUI.OnDateChanged += GetSO;
        root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        GetUIElements();

        SetDay();
    }

    private void OnDisable()
    {
    }

    protected override void GetUIElements()
    {
        base.GetUIElements();

        _dayLabel = root.Q<Label>(_dayStr);

        _oneResButton = root.Q<Button>(_oneResStr);
        _oneNameLabel = root.Q<Label>(_oneNameStr);
        _oneDescLabel = root.Q<Label>(_oneDescStr);
        _oneCountLabel = root.Q<Label>(_oneCountStr);

        _twoResButton = root.Q<Button>(_twoResStr);
        _twoNameLabel = root.Q<Label>(_twoNameStr);
        _twoDescLabel = root.Q<Label>(_twoDescStr);
        _twoCountLabel = root.Q<Label>(_twoCountStr);

        _threeResButton = root.Q<Button>(_threeResStr);
        _threeNameLabel = root.Q<Label>(_threeNameStr);
        _threeDescLabel = root.Q<Label>(_threeDescStr);
        _threeCountLabel = root.Q<Label>(_threeCountStr);
    }

    private void SetDay()
    {
        _dayLabel.text = $"{SpeedUI.currentDate}일차";
    }

    private void GetSO(RewardItemSO[] itemSO)
    {
        SetOneBox(itemSO[0]);
        SetTwoBox(itemSO[1]);
        SetThreeBox(itemSO[2]);
    }

    private void SetOneBox(RewardItemSO itemSO)
    {
        _oneItemSO = itemSO;
        
        _oneResButton.style.backgroundImage = itemSO.itemSprite.texture;
        _oneNameLabel.text = itemSO.itemName;
        _oneDescLabel.text = itemSO.itemDescription;

        if (itemSO.itemCount == 1)
            _oneCountLabel.style.display = DisplayStyle.None;
        else
            _oneCountLabel.text = itemSO.itemCount.ToString();
    }

    private void SetTwoBox(RewardItemSO itemSO)
    {
        _twoItemSO = itemSO;
        
        _twoResButton.style.backgroundImage = itemSO.itemSprite.texture;
        _twoNameLabel.text = itemSO.itemName;
        _twoDescLabel.text = itemSO.itemDescription;

        if (itemSO.itemCount == 1)
            _twoCountLabel.style.display = DisplayStyle.None;
        else
            _twoCountLabel.text = itemSO.itemCount.ToString();
    }

    private void SetThreeBox(RewardItemSO itemSO)
    {
        _threeItemSO = itemSO;
        
        _threeResButton.style.backgroundImage = itemSO.itemSprite.texture;
        _threeNameLabel.text = itemSO.itemName;
        _threeDescLabel.text = itemSO.itemDescription;

        if (itemSO.itemCount == 1)
            _threeCountLabel.style.display = DisplayStyle.None;
        else
            _threeCountLabel.text = itemSO.itemCount.ToString();
    }
    
    private void ClickOneResButton()
    {
        root.style.display = DisplayStyle.None;
        
        AddRewardItem(_oneItemSO);
    }
    
    private void AddRewardItem(RewardItemSO itemSO)
    {
        // switch (itemSO.itemType)
        // {
        //     case ERewardItemType.Line:
        //         ResourceManager.Instance.
        // }
    }
}