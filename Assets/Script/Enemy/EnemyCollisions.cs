using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    private int damage = 1;

    private bool untargetble = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerLife>().TakeDamage(damage);
           // StartCoroutine(collision.GetComponent<PlayerLife>().PlayerInvencible(untargetble));
        }
    }

}
