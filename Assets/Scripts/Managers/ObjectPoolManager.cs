using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance = null;
    public int poolSize = 10;

    Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        InitializePools();
    }

    private void InitializePools()
    {
        CreatePool("Arrow", "Projectiles/Arrow", poolSize);
    }

    private void CreatePool(string poolKey, string resourcePath, int poolSize)
    {
        GameObject prefab = Resources.Load<GameObject>(resourcePath);
        Queue<GameObject> newPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            newPool.Enqueue(obj);
        }
        pools.Add(poolKey, newPool);
    }

    public GameObject GetPool(string key)
    {
        if (pools.ContainsKey(key) && pools[key].Count > 0)
        {
            GameObject obj = pools[key].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return null;
        }
    }

    public void ReturnPool(string key, GameObject obj)
    {
        if (pools.ContainsKey(key))
        {
            obj.SetActive(false);
            pools[key].Enqueue(obj);
        }
        else
        {
            Debug.LogError("No pool exists for this key: " + key);
        }
    }
}
