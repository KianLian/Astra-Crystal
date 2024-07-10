    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBottom : MonoBehaviour
{
    [SerializeField]
    private GameObject bottomOn = null;

    [SerializeField]
    private GameObject[] platforToMove = null;

    private PlayerInteract playerinteract = null;

    public void CallThePlatform()
    {
        bottomOn.SetActive(true);

        for (int i = 0; i <platforToMove.Length; i++)
        {

        platforToMove[i].GetComponent<Platform>().JustMove=true;
            Debug.Log(platforToMove[i].GetComponent<Platform>().JustMove);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        playerinteract = collider.GetComponent<PlayerInteract>();

        if (playerinteract != null)
        {
            playerinteract.EnableInteractionWithButtom(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (playerinteract != null)
        {
            playerinteract.DisableInteractionWithButtom(this);
        }
    }
}
