using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private Dictionary<GameObject, Transform> oldParents =
                    new Dictionary<GameObject, Transform>();

    [SerializeField]
    private Transform player = null;

    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>().transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        oldParents.Add(other.gameObject, other.transform.parent);
        other.transform.SetParent(transform);

        if(other.CompareTag("Boss"))
        {

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (oldParents.TryGetValue(other.gameObject, out Transform oldParent))
        {
            other.transform.SetParent(oldParent);
            oldParents.Remove(other.gameObject);
        }
        else
        {
            if(other.CompareTag("Player"))
            {
                other.transform.SetParent(player);
            }
            else
            {
                other.transform.SetParent(null);
            }
          
        }
    }   
}
