using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class TutorialManager : MonoBehaviour, ITalk , ITalkImage
{

    public Action OnTalkEnd;
    
    private Label _text;
    private VisualElement _root;

    private int _currentText = 1;

    [SerializeField] private List<Sprite> _sprites = new();
    [SerializeField] private List<string> _texts = new();

    public List<Sprite> Sprite {get; set; }
    public List<string> Text {get; set; }
    
    private void Awake()
    {
        Text = _texts;
        Sprite = _sprites;
        _root = transform.GetChild(0).GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("BG");
        _text = transform.GetChild(1).GetComponent<UIDocument>().rootVisualElement.Q<Label>("Text");
    }

    private void Start()
    {
        _text.text = string.Empty;
        
        DOTween.To(() => _text.text, 
                value => _text.text = value, 
                Text[0], 
                1f) 
            .SetEase(Ease.InOutQuad);
        _root.style.backgroundImage = Sprite[0].texture;
    }

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            TalkUpdate();
        }
    }
    
    private void TalkUpdate()
    {
        try
        {
            NextText(Text[_currentText]);
            NextSprite(Sprite[_currentText]);
            _currentText++;
        }
        catch (Exception e)
        {
            Debug.Log("튜토리얼이 끝났습니다!");
            OnTalkEnd?.Invoke();
            gameObject.SetActive(false);
        }
    }


    public void NextText(string text)
    {
     
        _text.text = string.Empty;
        
        DOTween.To(() => _text.text, 
                value => _text.text = value, 
                text, 
                1f) 
            .SetEase(Ease.InOutQuad);
    }


    public void NextSprite(Sprite sprite)
    {
        _root.style.backgroundImage = sprite.texture;
    }
}
