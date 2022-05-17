using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public ObstaclePositionsSO obstaclePositionsSO;
     
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj= Instantiate(pool.prefab,transform);
                
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

    }

    public GameObject SpawnFromPool(string tag,Vector3 position , Quaternion rotation)
    {
     
        if (poolDictionary.ContainsKey(tag))
        {
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);
            Vector3 randomPos = objectToSpawn.transform.localPosition;
            objectToSpawn.transform.localPosition = position+randomPos;

             objectToSpawn.transform.localRotation = rotation;

            if (tag=="tri")
            {
                ChildObjectPositionHandle(objectToSpawn.transform.GetChild(0).gameObject);
            }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;

        }
        else
        {
            Debug.Log("Pool with tag " + tag + " does not exist");
            return null;
        }
    }

    
    
    private void ChildObjectPositionHandle(GameObject childObject)
    {
        childObject.gameObject.GetComponent<MeshRenderer>().enabled = true;
        childObject.gameObject.GetComponent<BoxCollider>().enabled = true;

        var brokableObject = childObject.transform.GetChild(0).gameObject.transform;
        int i = 0;
        foreach (Transform item in brokableObject)
        {
            childObject.transform.GetChild(0).gameObject.SetActive(false); 
            item.localPosition = obstaclePositionsSO.brokenObjectsPositions[i];
            item.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            i++;
          
        }
    }


    public GameObject SpawnFromPoolBulletAmmo(string tag, Vector3 position)
    {

        if (poolDictionary.ContainsKey(tag))
        {
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.transform.localPosition = position;
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
