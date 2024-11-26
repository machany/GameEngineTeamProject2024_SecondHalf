using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIToolkit : MonoBehaviour
{
    protected VisualElement root;

    protected virtual void GetUIElements()
    {
        try
        {
            root = GetComponent<UIDocument>().rootVisualElement;
        }
        catch (Exception exception)
        {
            root = null;
            Debug.LogWarning($"root visual element not found in UI document. exception : {exception}");
        }
    }
}