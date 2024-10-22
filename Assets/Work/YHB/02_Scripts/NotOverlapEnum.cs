using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// enum의 값을 중복되지 않게 뱉어냅니다.
/// Get()메서드를 이용해 사용합니다.
/// </summary>
/// <typeparam name="T">반드시 enum형식을 넣으세요.</typeparam>
public class NotOverlapEnumType<T>
{
    private List<T> list = new List<T>();

    /// <summary>
    /// enum을 중복없이 렌덤으로 return합니다.
    /// </summary>
    /// <returns>enum값을 렌덤으로 반환합니다.</returns>
    public T Get()
    {
        try
        {
            if (list.Count == 0)
            {
                list.Clear();

                foreach (T item in Enum.GetValues(typeof(T)))
                {
                    list.Add(item);
                }
            }

            int randomIndex = Random.Range(0, list.Count);
            T returnValue = list[randomIndex];
            list.RemoveAt(randomIndex);
            return returnValue;
        }
        catch (ArgumentException)
        {
            Debug.Log(typeof(T).Name + " isn't enum");
            return default(T);
        }
    }
}
