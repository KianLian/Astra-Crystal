using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player
{
    [Header("Movement")]
    [SerializeField]
    private float movementSpeed = 4f;
    [SerializeField]
    private float jumpForce = 0f;

    [Header("Ground Detection")]
    [SerializeField]
    private Transform[] feetTransform = new Transform[2];
    [SerializeField]
    private LayerMask groundLayerMask = 0;
    private CompositeCollider2D [] groundDetectionColliders = new CompositeCollider2D[1];

    [Header("Input")]
    private float horizontalMovement = 0;

    [SerializeField]
    private AudioClip jumpSound = null;

    public bool CanJump = true;


    private void DisableScript()
    {
        canDoActions = false;
    }


    public bool CanDoAction()
    {
        return !DialogueManager.IsTalking;
    }

    private void Update()
   {
        if (playerIsAlive)
        {
            if (canDoActions   )
        {
            horizontalMovement = playerInput.Player.Movement.ReadValue<float>();

            if (!jump)
            {
                jump = playerInput.Player.Jump.triggered;
            }
          }
        }
           
    }


    private void FixedUpdate()
    {
        if(playerIsAlive)
        {
            isGrounded = CheckForFloor();
            myAnimator.SetBool("OnGrounded", isGrounded);
            canDoActions = CanDoAction();
          

              if (!canDoActions)
                 {
                   myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
                    myAnimator.SetFloat("MovementHorizontal", Mathf.Abs(0));    
               }

               else
        {
         if (CheckDirectionChange())
        {
            FlipHorizontally();
        }

        NormalVelocity();

      

        myAnimator.SetFloat("MovementHorizontal", Mathf.Abs(myRigidbody.velocity.x));

        if (jump && isGrounded && CanJump)
        {
            myRigidbody.AddForce(Vector2.up * jumpForce);
            playerAudioSource.PlayOneShot(jumpSound);
            myAnimator.SetTrigger("Jump");
        }
        ResetInputs();  
        }
      
        } else
        {
            myRigidbody.velocity = new Vector2(0, 0);
            isGrounded = false;
        }
       
    }



    //check if the player is on ground
    private bool CheckForFloor()
    {
        return Physics2D.OverlapPointNonAlloc(feetTransform[0].position, groundDetectionColliders, groundLayerMask) > 0 ||
            Physics2D.OverlapPointNonAlloc(feetTransform[1].position, groundDetectionColliders, groundLayerMask) > 0;
    }

    private void ResetInputs()
    {
        jump = false;
    }

    //change direction
    private bool CheckDirectionChange()
    {
        if (transform.right.x > 0 && horizontalMovement < 0 || transform.right.x < 0 && horizontalMovement > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //flip horizontal   
    private void FlipHorizontally()
    {
        Vector3 targetRotation = transform.localEulerAngles;
        targetRotation.y += 180;
        transform.localEulerAngles = targetRotation;
    }

    public void NormalVelocity()
    {
        myRigidbody.velocity = new Vector2(horizontalMovement * movementSpeed, myRigidbody.velocity.y);
    }


    //public IEnumerator KnockBack(float knockbackDuration, float KonckbackforceX, float knockbackForceUp, Vector3 knockbackVector)
    //{
    //    playerInput.Disable();
    //    float time = 0;
    //    while(knockbackDuration >= time)
    //    {
          
    //        time += Time.deltaTime;

    //        //não ganhar mais velocidade no eixo do y, caso haja knockback
    //        if(myRigidbody.velocity.y == 0)
    //        {
    //             myRigidbody.AddForce(new Vector3(knockbackVector.x < transform.position.x ?  KonckbackforceX : -KonckbackforceX, knockbackForceUp, transform.position.z));
    //        }

    //        playerInput.Enable();
    //        yield return new WaitForFixedUpdate();
    //    }
    //}
}

