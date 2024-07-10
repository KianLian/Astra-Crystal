using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBoss : MonoBehaviour
{
    [SerializeField]
    private Animator Nextcene = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Nextcene.SetTrigger("StartTransition"); 
        }
    }

  
}
