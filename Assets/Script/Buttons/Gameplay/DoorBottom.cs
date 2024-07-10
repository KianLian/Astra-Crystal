using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBottom : MonoBehaviour
{

    private PlayerInteract playerinteract = null;

    [SerializeField]
    private GameObject door = null;
    [SerializeField]
    private GameObject layingDoor = null;
    [SerializeField]
    private SpriteRenderer bottompressed = null;

    private void Awake()
    {
        layingDoor.SetActive(false);
    }

    public void CallTheDoor()
    {
        bottompressed.enabled = true;

        door.SetActive(false);
        layingDoor.SetActive(true);
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
