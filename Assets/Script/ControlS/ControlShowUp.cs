using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShowUp : MonoBehaviour
{

    [SerializeField]
    private GameObject control = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            control.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            control.SetActive(false) ;
        }
    }
}
