using UnityEngine;
using ScreenFix;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        FixedScreen.FixedScreenSet();
    }
}
