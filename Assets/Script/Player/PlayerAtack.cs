using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtack : Player
{
    [SerializeField]
    private Transform atackPoint = null;
    [SerializeField]
    private float atackRange = 0.5f;
    [SerializeField]
    private LayerMask layerEnemy;

    [SerializeField]
    private PlayerMovement playerMovement = null;

    [SerializeField]
    private Animator astraSlash = null;
    [SerializeField]
    private AudioClip attackSound = null;

    [SerializeField]
    private int damage = 1;



    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsAlive)
        {
            canDoActions =   playerMovement.CanDoAction();

            atack = playerInput.Player.Atack.triggered;

            if (atack && canDoActions)
            {
                AnimationAtack();
            }
        }

    }

    private void AnimationAtack()
    {
        myAnimator.SetTrigger("Atack");
    }

    //Call in Animation
    private void Attack()
    {

        astraSlash.SetTrigger("Attack");
        playerAudioSource.PlayOneShot(attackSound);
        Collider2D[] EnemiesToDamage = Physics2D.OverlapCircleAll(atackPoint.position, 
            atackRange, layerEnemy);

        for (int i = 0; i<EnemiesToDamage.Length; i++)
        {
            EnemiesToDamage[i].GetComponent<EnemyLife>().EnemyTakeDamage(damage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(atackPoint.position, atackRange);
    }


}
