using UnityEngine;
using UnityEngine.Analytics;

public class GameOverEffect : MonoBehaviour
{
    [SerializeField] private GameObject Effect;
    [SerializeField] private GameObject GameOverPanel;
    private int count = 0;

    private void Awake()
    {
        Effect.SetActive(false);
    }

    private void OnEnable()
    {
        GameOverCount.OnGameOverEffectStart += EffectEnable;
        GameOverCount.OnGameOverEffectEnd += EffectDisable;
        GameOverCount.OnGameOver +=GameOver;
    }

    private void GameOver()
    {
        GameOverPanel.SetActive(true);
    }


    private void OnDisable()
    {
        GameOverCount.OnGameOverEffectStart -= EffectEnable;
        GameOverCount.OnGameOverEffectEnd -= EffectDisable;
    }

    private void EffectEnable()
    {
        if (Effect.activeSelf == false)
        {
            Effect.SetActive(true);
        }
        count++;
    }
    
    private void EffectDisable()
    {
        count--;
        if (count >= 0) 
        {
            Effect.SetActive(false);
        }
    }
}
