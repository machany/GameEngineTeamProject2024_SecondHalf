using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<int, GameObject> _prefab = new Dictionary<int, GameObject>();
    private Dictionary<int, Stack<GameObject>> _pool = new Dictionary<int, Stack<GameObject>>();
    
    public void AddPoolItem(PoolItemSO poolItemSO)
    {
        if (_pool.ContainsKey(poolItemSO.key))
        {
            Debug.LogError("ContainsKey : " + poolItemSO.key);
            return;
        }

        _pool.Add(poolItemSO.key, new Stack<GameObject>());
        _prefab.Add(poolItemSO.key, poolItemSO.prefab);
    }

    public void Push(int key, GameObject gameObject)
    {
        try
        {
            _pool[key].Push(gameObject);
            gameObject.SetActive(false);
            gameObject.transform.position = transform.position;
        }
        catch
        {
                Debug.LogWarning("can't find key : " + key);
        }
    }

    public GameObject Pop(int key)
    {
        try
        {
            GameObject gameObject = null;

            if (_pool[key].Count > 0)
            {
                gameObject = _pool[key].Pop();
                gameObject.SetActive(true);
                gameObject.transform.parent = transform;
            }
            else
            {
                gameObject = Instantiate(_prefab[key], transform);
                gameObject.SetActive(true);
            }

            return gameObject;
        }
        catch
        {
                Debug.LogWarning("can't find key : " + key);
            return null;
        }
    }
}
