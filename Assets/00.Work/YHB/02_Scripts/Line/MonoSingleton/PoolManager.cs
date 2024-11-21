using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<PoolItemSO, Stack<GameObject>> _pool = new Dictionary<PoolItemSO, Stack<GameObject>>();

    public void AddPoolItem(PoolItemSO poolItemSO)
    {
        if (_pool.ContainsKey(poolItemSO))
        {
            Debug.LogError("ContainsKey : " + poolItemSO.key);
            return;
        }

        _pool.Add(poolItemSO, new Stack<GameObject>());
    }

    public void Push(PoolItemSO poolItemSO, GameObject gameObject)
    {
        try
        {
            _pool[poolItemSO].Push(gameObject);
            gameObject.SetActive(false);
            gameObject.transform.position = transform.position;
        }
        catch
        {
            if (poolItemSO != null)
                Debug.LogWarning("can't find key : " + poolItemSO.key);
        }
    }

    public GameObject Pop(PoolItemSO poolItemSO)
    {
        try
        {
            GameObject gameObject = null;

            if (_pool[poolItemSO].Count > 0)
            {
                gameObject = _pool[poolItemSO].Pop();
                gameObject.SetActive(true);
                gameObject.transform.parent = transform;
            }
            else
            {
                gameObject = Instantiate(poolItemSO.prefab, transform);
                gameObject.SetActive(true);
            }

            return gameObject;
        }
        catch
        {
            if (poolItemSO != null)
                Debug.LogWarning("can't find key : " + poolItemSO.key);
            return null;
        }
    }
}
