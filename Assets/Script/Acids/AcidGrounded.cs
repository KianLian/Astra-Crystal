using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidGrounded : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerLife>().StartAcidDamage();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerLife>().StopAcidDamage();
        }
    }
}
