using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static bool IsDestroyed;

    public static T Instance
    {
        get
        {
            if (IsDestroyed)
                _instance = null;

            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                    Debug.LogError($"{typeof(T).Name} 싱글톤이 존재하지 않습니다.");
                else
                    IsDestroyed = false;
            }

            return _instance;
        }
    }

    private void OnDisable()
    {
        // IsDestroyed = true;
    }
}