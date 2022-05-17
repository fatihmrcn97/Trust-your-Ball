using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerate : MonoBehaviour
{
    [SerializeField] private List<GameObject> roads;

    [SerializeField] private int index;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (index == 0)
            {
                roads[2].transform.position = new Vector3(roads[0].transform.position.x + 80, roads[0].transform.position.y, roads[0].transform.position.z);
                roads[2].SetActive(false);
                roads[2].SetActive(true);
               
            }
            if (index == 1)
            {
                roads[0].transform.position = new Vector3(roads[1].transform.position.x + 80, roads[1].transform.position.y, roads[1].transform.position.z);
   
                roads[0].SetActive(false);
                roads[0].SetActive(true);
            }
            if (index == 2)
            {
                roads[1].transform.position = new Vector3(roads[2].transform.position.x + 80, roads[2].transform.position.y, roads[2].transform.position.z);
                 
                roads[1].SetActive(false);
                roads[1].SetActive(true);
            }
        }
    }

}
