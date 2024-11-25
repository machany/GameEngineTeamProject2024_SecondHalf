using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIToolkit : MonoBehaviour
{
    protected VisualElement root;

    protected virtual void GetUIElements()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
    }
}