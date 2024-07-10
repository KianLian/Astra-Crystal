using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    //states
    protected bool isalive = true;
    protected bool interact = false;
    protected bool pause = false;
    protected bool atack = false;
    protected bool jump = false;
    public bool isGrounded = false;
    protected bool isInteracting = false;
    protected bool playerIsAlive = true;
    protected bool isPlayerAtacking = false;
    protected bool canDoActions = true;

    //components
    protected Rigidbody2D myRigidbody = null;
    protected Animator myAnimator = null;
    protected PlayerInput playerInput = null;
    protected SpriteRenderer playerSpriteRender = null;
    protected Collider2D playerCollider2D = null;
    protected AudioSource playerAudioSource = null;

    protected void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerSpriteRender = GetComponent<SpriteRenderer>();
        playerCollider2D = GetComponent<Collider2D>();
        playerAudioSource = GetComponent<AudioSource>();
    }

}

