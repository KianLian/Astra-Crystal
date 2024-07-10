using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonYellow : MonoBehaviour
{
    [SerializeField]
    private GameObject[] platforToMove = null;

    [SerializeField]
    private GameObject upBottom = null;
    [SerializeField]
    private GameObject downBottom = null;

    private bool boxColliding = false;
    private bool playerColliding = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box") && !boxColliding) 
        {
            boxColliding = true;
            upBottom.SetActive(true);
            downBottom.SetActive(false);

            for (int i = 0; i < platforToMove.Length; i++)
            {
                platforToMove[i].GetComponent<Platform>().ContinuosMove = true;
                platforToMove[i].GetComponent<Platform>().canReset = false;
            }
        }


        //if (other.CompareTag("Player") && !playerColliding)
        //{
        //    playerColliding = true;
        //    upBottom.SetActive(true);
        //    downBottom.SetActive(false);    

        //    for (int i = 0; i < platforToMove.Length; i++)
        //    {
        //        platforToMove[i].GetComponent<Platform>().ContinuosMove = true;
        //        platforToMove[i].GetComponent<Platform>().canReset = false;
        //    }
       // } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Box") && boxColliding)
        {
            boxColliding = false;
            upBottom.SetActive(false);
            downBottom.SetActive(true);

            for (int i = 0; i < platforToMove.Length; i++)
            {
                platforToMove[i].GetComponent<Platform>().ContinuosMove = false;
                platforToMove[i].GetComponent<Platform>().canReset = true;
            }
        }


        //if (other.CompareTag("Player") && playerColliding)
        //{
        //    playerColliding = false;
        //    upBottom.SetActive(false);
        //    downBottom.SetActive(true);

        //    for (int i = 0; i < platforToMove.Length; i++)
        //    {
        //        platforToMove[i].GetComponent<Platform>().ContinuosMove = false;
        //        platforToMove[i].GetComponent<Platform>().canReset = true;
        //    }
        //}

    }

}
