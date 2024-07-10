using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerInteract : Player
{
    private TriggerBottom triggerbotom = null;
    private DoorBottom doorbotomtriggered = null;
    private VanishFloor vanishfloor = null;

    private float timeToSkip = 1f;
    private float holdTime = 0;

    private float pass = 0f;



    public void EnableInteractionWithButtom(TriggerBottom bottomtopress)
    {
        triggerbotom = bottomtopress;
    }

    public void DisableInteractionWithButtom(TriggerBottom bottomtopress)
    {
        if (triggerbotom == bottomtopress)
            triggerbotom = bottomtopress;
    }

    public void EnableInteractionWithButtom(DoorBottom doorBottom)
    {
        doorbotomtriggered = doorBottom;
    }


    public void DisableInteractionWithButtom(DoorBottom doorBottom)
    {
        if (doorbotomtriggered == doorBottom)
            doorbotomtriggered = doorBottom;
    }


    public void EnableInteractionWithButtom(VanishFloor vanish)
    {
        vanishfloor = vanish;
    }

    public void DisableInteractionWithButtom(VanishFloor vanish)
    {
        if (vanishfloor == vanish)
            vanishfloor = vanish;
    }

    private void Update()
    {
        if (playerIsAlive)
        {
            interact = playerInput.Player.Interact.triggered;

            if (interact && triggerbotom != null)
            {
                triggerbotom.CallThePlatform();
            }

            if (interact && doorbotomtriggered != null)
            {
                doorbotomtriggered.CallTheDoor();
            }

            if (interact && vanishfloor != null)
            {
                vanishfloor.VanishThisFloor();
            }

            if(interact && DialogueManager.CanNext)
            {
                DialogueManager.Instance.ShowNextSentence();
            }

 



            if (VideoManager.Instance != null)
            {
               if(VideoManager.IsPlaying)
               {
                    pass = playerInput.Player.Pass.ReadValue<float>();

                    if(pass > 0) 
                    {
                      TryToIncrementUntillPass();
                       Debug.Log("Passed");
                    }
                else
                {
                    holdTime = 0;
                }
               }
            }




            pause = playerInput.Player.Pause.triggered;

            if (pause && !GameManager.IsPaused)    
            {
                GameManager.Instance.PauseGame(true);
            }
            else if(pause && GameManager.IsPaused)
            {
                GameManager.Instance.PauseGame(false);
            }
        }

    }

    private void TryToIncrementUntillPass()
    {
        holdTime += Time.unscaledDeltaTime;

        if(holdTime >= timeToSkip )
        {
            VideoManager.Instance.StopPlaying();
            holdTime = 0;
        }
    }
}
