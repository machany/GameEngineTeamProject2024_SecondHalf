using System;
using UnityEngine;

[Serializable]
public class NotifyValue<T>
{
    public event Action<T, T> OnValueChanged;

    [SerializeField] private T _value;

    public T Value
    {
        get => _value;

        set
        {
            T before = _value;
            _value = value;

            if ((before == null && value != null) || !before.Equals(_value))
                OnValueChanged?.Invoke(before, _value);
        }
    }

    public NotifyValue()
    {
        _value = default;
    }

    public NotifyValue(T value)
    {
        _value = value;
    }
}
