using UnityEngine;

public class GameOverEffect : MonoBehaviour
{
    [SerializeField] private GameObject Effect;
    private int count = 0;

    private void Awake()
    {
        Effect.SetActive(false);
    }

    private void OnEnable()
    {
        GameOverCount.OnGameOverEffectStart += EffectEnable;
        GameOverCount.OnGameOverEffectEnd += EffectDisable;
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
