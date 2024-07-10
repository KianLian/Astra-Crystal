using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private int damage = 1;
    private Animator slash = null;

    private void Awake()
    {
        slash = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            slash.SetTrigger("Attack");
            collision.GetComponent<PlayerLife>().TakeDamage(damage);
        }
    }

}
