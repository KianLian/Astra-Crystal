using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLandingMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    private float backupSpeed;

    private Rigidbody2D enemyRigidBody2D = null;

    private bool alive = true;

    [SerializeField]
    private LayerMask groundLayerMask;


    [SerializeField]
    private Transform flipEdgeVectorDetection = null;

    [SerializeField]
    private float flipEdgeDetectionRange = 2f;

    [SerializeField]
    private LayerMask obstacleLayerMask;
    [SerializeField]
    private LayerMask enemieLayerMask;
    private bool CanPlayerBeDamaged = true;
   

    [SerializeField]
    private Transform floorEdgeVectorDetection = null;
    [SerializeField]
    private LayerMask floorLayerMask;
    private float floorEdgeDetectionRange = 2f;

    private RaycastHit2D[] results = new RaycastHit2D[1];

   
    private Animator enemyAnimator = null;
    private int damage = 1;
    private PlayerLife player = null;
    [SerializeField]
    private float timeToComeBack = 1f;
    private bool enemyAttacking = false;
    private float timeToAttackAgain = 2f;

    [SerializeField]
    private AudioClip attackSound = null;
    private AudioSource landEnemyAudioSource = null;

    private void Awake()
    {
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        backupSpeed = speed;
        enemyAnimator = GetComponent<Animator>();
        CanPlayerBeDamaged = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>().canBeDamaged;
        landEnemyAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if(alive && !enemyAttacking)
        {
            enemyRigidBody2D.velocity = new Vector2(-speed * transform.right.x, enemyRigidBody2D.velocity.y);

            if(CheckForEnemy())
            {
                StartCoroutine(nameof(SetAnimation));          
            }
        }   
    }

    private void FixedUpdate()
    {
        if(!CheckForFloor() || CheckForObstacle())
        {
             Flip();

        }

        
    }

    private void Flip()
    {
        Vector3 targetRotation = transform.localEulerAngles;
        targetRotation.y += 180f;
        transform.localEulerAngles = targetRotation;
    }
    
    private bool CheckForFloor()
    {
            
        Debug.DrawLine (floorEdgeVectorDetection.position, floorEdgeVectorDetection.transform.right + floorEdgeVectorDetection.position.normalized * floorEdgeDetectionRange);
    
        return Physics2D.LinecastNonAlloc(floorEdgeVectorDetection.position, 
            floorEdgeVectorDetection.position + floorEdgeVectorDetection.transform.right 
            * floorEdgeDetectionRange, results, floorLayerMask) > 0;
    }

    private bool CheckForEnemy()
    {

      

        if (CanPlayerBeDamaged)
        {
           Debug.DrawLine(flipEdgeVectorDetection.position,
                    flipEdgeVectorDetection.position + flipEdgeVectorDetection.transform.right
                    * flipEdgeDetectionRange);

                return Physics2D.LinecastNonAlloc(flipEdgeVectorDetection.position,
                    flipEdgeVectorDetection.position + flipEdgeVectorDetection.transform.right
                    * flipEdgeDetectionRange, results, enemieLayerMask) > 0;
        } else
        {
            return false;
        }
     
    }

    private bool CheckForObstacle()
    {
        Debug.DrawLine(flipEdgeVectorDetection.position,
            flipEdgeVectorDetection.position + flipEdgeVectorDetection.transform.right
            * flipEdgeDetectionRange);

        return Physics2D.LinecastNonAlloc(flipEdgeVectorDetection.position,
            flipEdgeVectorDetection.position + flipEdgeVectorDetection.transform.right
            * flipEdgeDetectionRange, results, obstacleLayerMask) > 0;
    }

    private IEnumerator SlowDown()
    {
        speed = 1f;
        yield return new WaitForSeconds(timeToComeBack);
        speed = backupSpeed;
    }

    private IEnumerator SetAnimation()
    {
        enemyAnimator.SetTrigger("Attack");
        landEnemyAudioSource.PlayOneShot(attackSound);
        yield return new WaitForSeconds(timeToAttackAgain);
    }

    private void Attack()
    {
        enemyAttacking = true;

    }

    private void CanWalkAgain()
    {
        enemyAttacking = false;
        StartCoroutine(nameof(SlowDown)); 
    }

}       
