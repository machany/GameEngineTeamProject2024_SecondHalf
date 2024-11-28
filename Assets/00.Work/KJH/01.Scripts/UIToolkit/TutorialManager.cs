using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class TutorialManager : MonoBehaviour, ITalkImage
{

    public Action OnTalkEnd;
    
    private VisualElement _root;

    private int _currentText = 1;

    [SerializeField] private List<Sprite> _sprites = new();

    public List<Sprite> Sprite {get; set; }
    public List<string> Text {get; set; }
    
    private void Awake()
    {
        Sprite = _sprites;
        _root = transform.GetChild(0).GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("BG");
    }

    private void Start()
    {
        _root.style.backgroundImage = Sprite[0].texture;
    }

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TalkUpdate();
        }
    }
    
    private void TalkUpdate()
    {
        try
        {
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

    public void NextSprite(Sprite sprite)
    {
        _root.style.backgroundImage = sprite.texture;
    }
}
