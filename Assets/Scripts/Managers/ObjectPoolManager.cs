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
        CreatePool("FireBall", "Projectiles/FireBall", poolSize);
        CreatePool("BlackHole", "Projectiles/BlackHole", poolSize);

        CreatePool("HitEffect", "Effect/HitEffect", poolSize);
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
        if (!pools.ContainsKey(key))
        {
            Debug.LogError("No pool exists for this key: " + key);
            return null;
        }

        if (pools[key].Count > 0)
        {
            GameObject obj = pools[key].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return CreateAdditionalObject(key);
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
    private GameObject CreateAdditionalObject(string key)
    {
        string resourcePath = "";

        switch (key)
        {
            case "Arrow":
                resourcePath = "Projectiles/Arrow";
                break;
            case "FireBall":
                resourcePath = "Projectiles/FireBall";
                break;
            case "BlackHole":
                resourcePath = "Projectiles/BlackHole";
                break;
            case "HitEffect":
                resourcePath = "Effect/HitEffect";
                break;
        }

        GameObject prefab = Resources.Load<GameObject>(resourcePath);

        GameObject obj = Instantiate(prefab);
        obj.SetActive(true);
        obj.transform.SetParent(transform);
        pools[key].Enqueue(obj);

        return obj;
    }
}
