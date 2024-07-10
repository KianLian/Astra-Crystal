using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject boss = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossScrip>().DisableCollision();
        }
    }
}
