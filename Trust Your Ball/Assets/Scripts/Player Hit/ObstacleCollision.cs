
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    [SerializeField] private GameObject explodeEffect;

    private PlayerHealth healthsys;

    private PlayerShooting playerShooting;
    private void Awake()
    {
        healthsys = GetComponent<PlayerHealth>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!LevelController.instance.canTochable)
            return;

        if (other.gameObject.CompareTag("obstacle"))
        {
            //particle effect
            Instantiate(explodeEffect, transform.position, Quaternion.identity);

            // Game Stop
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            // other.gameObject.transform.parent = transform.parent ;


            // Health Decrease
            healthsys.Damage(35);

            // Break the ball

        }

        if (other.gameObject.CompareTag("cylinderobs"))
        {
            //particle effect
            Instantiate(explodeEffect, transform.position, Quaternion.identity);


            // Game End


            // Break the ball
        }


        if (other.gameObject.CompareTag("danger"))
        {
            //particle effect
            Instantiate(explodeEffect, transform.position, Quaternion.identity);


            // Game End
            healthsys.Damage(150);


            // Break the ball
        }
        if (other.gameObject.CompareTag("bullet"))
        {
            playerShooting.AddBullet(Random.Range(3,10));
            other.gameObject.SetActive(false);
        }
    }

}
