using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 5f;

    private float moveX;
    void Start()
    {
        
    }

    [System.Obsolete]
    private void OnMouseDrag()
    {
        if (!LevelController.instance.isGameContinou)
            return;
        float rotX = Input.GetAxis("Mouse X") * rotateSpeed * Mathf.Deg2Rad;
        transform.RotateAround(Vector3.right,-rotX);
    }

    private void Update()
    {
       
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            moveX = -Input.GetTouch(0).deltaPosition.x / Screen.width;
        
            transform.Rotate(0f, moveX * rotateSpeed * Time.deltaTime, 0f);
        }


    }
}
