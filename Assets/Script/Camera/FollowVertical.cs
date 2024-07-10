using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowVertical : MonoBehaviour
{
    [SerializeField]
    private float offsetx = 12;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraManager.Instance.CanFollowVetical();
            CameraManager.Instance.setX = offsetx;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraManager.Instance.CanFollowVetical();
        }
    }
}
