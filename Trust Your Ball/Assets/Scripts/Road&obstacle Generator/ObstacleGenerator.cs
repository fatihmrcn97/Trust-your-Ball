using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private ObstaclePool obstaclePool;

    private void Start()
    {
        obstaclePool = GetComponent<ObstaclePool>(); 
    }

    IEnumerator SpawnObjects()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            obstaclePool.SpawnFromPool("tri", new Vector3( 0, Random.Range(-0.40f, 0.45f)), Quaternion.Euler(0, Random.Range(0, 90), 90));
        }
    }
    IEnumerator SpawnBullets()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.1f);
            obstaclePool.SpawnFromPoolBulletAmmo("ammo", new Vector3(-0.59f, Random.Range(-0.40f, 0.45f),0.18f));
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnObjects());
        StartCoroutine(SpawnBullets());
    }
    
}
