using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerHprizontally : MonoBehaviour
{
    [SerializeField]
    private float offsetY = 12;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CameraManager.Instance.CanFollowHorizontal();
            CameraManager.Instance.setY = offsetY;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraManager.Instance.CanFollowHorizontal();

        }
    }

}
