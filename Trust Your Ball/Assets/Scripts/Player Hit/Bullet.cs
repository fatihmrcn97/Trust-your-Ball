using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject explodeEffect;
     
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("obstacle"))
        {
            //particle effect
            Instantiate(explodeEffect, transform.position, Quaternion.identity);

            // Game Stop
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            // other.gameObject.transform.parent = transform.parent ;
            ScoreHandle.instance.AddScore(10);

            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("danger"))
        {
            // nothing happen
            gameObject.SetActive(false);
            // sprite put
 
        }
    }
}
