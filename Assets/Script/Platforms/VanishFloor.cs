 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishFloor : MonoBehaviour
{
    private PlayerInteract playerinteract = null;
    [SerializeField]
    private GameObject floor = null;
    [SerializeField]
    private GameObject buttonOn = null;

    public void VanishThisFloor()
    {
        floor.SetActive(false);
        buttonOn.SetActive(true);
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
        playerinteract = collider.GetComponent<PlayerInteract>();

        if (playerinteract != null)
        {
            playerinteract.DisableInteractionWithButtom(this);
        }
    }
}
