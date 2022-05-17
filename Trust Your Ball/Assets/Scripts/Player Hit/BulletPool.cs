using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour

{
    public static BulletPool instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [System.Serializable]
    public class BltPool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<BltPool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;



    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (BltPool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }

    }

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.transform.position = position;
            objectToSpawn.SetActive(true);
            poolDictionary[tag].Enqueue(objectToSpawn);
            return objectToSpawn;
        }
        else
        {
            Debug.Log("Pool with tag " + tag + " does not exist");
            return null;
        }
    }
}
