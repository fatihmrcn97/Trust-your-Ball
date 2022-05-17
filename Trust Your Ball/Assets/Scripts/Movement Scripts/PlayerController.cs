using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovementDetailSO playerMovementDetailSO;

    private Camera mainCam;

    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3F;
    void Start()
    {
        mainCam = Camera.main;   
    } 
    void Update()
    {
        if (!LevelController.instance.isGameContinou)
            return;

        //mainCam.transform.position = transform.position + playerMovementDetailSO.offset; 
        mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position, transform.position + playerMovementDetailSO.offset, ref velocity, smoothTime);

        transform.position = new Vector3(transform.position.x + playerMovementDetailSO.speed * Time.deltaTime, transform.position.y, transform.position.z);                 
    }

}
