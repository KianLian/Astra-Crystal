using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   [SerializeField]
    private Color disapearedBullet = new Color (0,0,0);
    private SpriteRenderer bulletSpriteRender = null;


    private int damage = 1;
 
    [SerializeField]
    float knockDuration = 0.005f;
    [SerializeField]
    float knochForceUP = 90f;
    [SerializeField]
    float knockForceX = 80f;    

    [SerializeField]
    private float timeToGetDestroyed = 0;

    private bool untargetble = true;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerLife>().TakeDamage(damage);
            DestroyBullet();
        }
        if(collision.CompareTag("Ground"))
        {
            DestroyBullet();
        }
          
    }   

    private void DestroyBullet()
    {   
        Destroy(gameObject, timeToGetDestroyed);
    }
    
}
