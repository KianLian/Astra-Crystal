using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform player = null;
    [SerializeField]
    private float smoothTime = 0.5f;
    [SerializeField]
    private float offsetY = 2f;
    [SerializeField]
    private float offsetX = 2f;
    [SerializeField]
    private float correct = 2f;

    private float initialZ;
    private float initialY;
    private float initialX;

    [SerializeField]
    private float playerPositionInX;
    private Vector3 velocity = Vector3.zero;


    private void Awake()
    {
        initialZ = transform.position.z;
        initialY = transform.position.y;
        player = FindObjectOfType<PlayerMovement>().transform;
    }



    private void LateUpdate()
    {
        if (player != null && CameraManager.Instance != null)
        {
             if (CameraManager.Isfollowing == true)
            {
                FollowPlayer();
            }   
             if (CameraManager.IsfollowingOnVertical == true)
               {
                FollowPlayerVertically();
            } 
                if (CameraManager.IsfollowingOnHorizontal == true)
            {
                FollowPlayerHorizontally();
            }

            if (CameraManager.Isfollowing == true && CameraManager.Isfollowing == true)
            {
                FollowPlayer();
            }
            if (CameraManager.Isfollowing == true && CameraManager.IsfollowingOnHorizontal == true)
            {
                FollowPlayer();
            }


        }
        else
        {
            return;
        }
      
    }

    private void FollowPlayerHorizontally()
    {
        Vector3 targetPosition = player.transform.position;
        targetPosition.y = CameraManager.Instance.setY;
        targetPosition.z = initialZ;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void FollowPlayerVertically()
    {
            Vector3 targetPosition = player.transform.position;
            targetPosition.x = CameraManager.Instance.setX;
            targetPosition.y += offsetY;
            targetPosition.z = initialZ;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
       
    }

    private void FollowPlayer() 
    {
        Vector3 targetPosition = player.transform.position;
        targetPosition.y += offsetY;
        targetPosition.z = initialZ;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

    }

}
