using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private  int recovery = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.GetComponent<PlayerLife>().playerFullLife)
        {
            collision.GetComponent<PlayerLife>().RecoverLife(recovery);
            DestroyCristal();
        }
    }
        private void DestroyCristal()
        {
            Destroy(gameObject, 0f);
        }
    }

