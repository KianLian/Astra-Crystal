using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggered : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue;

    [SerializeField]
    private bool itsTheDialoqueBeforeFight = false;
    [SerializeField]
    private bool itsTheLastDialogue = false;
    [SerializeField]
    private bool playerEnteringInCave = false;

    private bool playerConfirm = false;

    private void CanDialogate()
    {    
      StartCoroutine(DialogueManager.Instance.StartTheDialogue(dialogue));
        playerConfirm = true;
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !playerConfirm &&!VideoManager.IsPlaying)    
        {
            CanDialogate();
            Debug.Log(playerConfirm);       

            if(playerEnteringInCave)
            {
                DialogueManager.IsTheDialogueWhenEnteringInCave = playerEnteringInCave;
            }

            if(itsTheDialoqueBeforeFight)
            {
                DialogueManager.IsTheDialogueBeforeFight = itsTheDialoqueBeforeFight;
            } 

            if(itsTheLastDialogue)
            {
                DialogueManager.IsTheLastDialogue = itsTheLastDialogue;
            }

        }
    }


}
