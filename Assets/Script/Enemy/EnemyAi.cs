using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAi : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float speed = 80f;
    private float backupSpeed;
    [SerializeField]
    private float nextWayPointDistance = 2f;
    [SerializeField]
    private float distanceToGoTOPlayer = 4f;
    [SerializeField]
    private float distanceToStop = 2f;
    [SerializeField]
    private float forceToStop = 30f;

    [Header("Path")]
    private Path path;
    private int currentWayPoint = 0;
    private bool reachedTheEnd = false;

    private Seeker seeker = null;
    private Rigidbody2D enemyRigidBody2D = null;
    [SerializeField]
    private Transform player = null;
    private bool CanPlayerBeDamaged = false;
    private EnemyLife enemyLife = null;

    [Header("Shoot")]
    [SerializeField]
    private GameObject bullet = null;
    [SerializeField]
    private Transform pointshoot = null;
    [SerializeField]
    private float enemyBulletSpeed = 2f;
    [SerializeField]
    private float offsetAngle = 0;
    private bool coroutineRunning = false;
    private float timeToShoot = 3f;
    private Vector2 direction;

    [SerializeField]
    private AudioClip attackSound = null;
    private AudioSource enemyAudioSource = null;
    // Start is called before the first frame update
    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyLife = GetComponent<EnemyLife>();
        CanPlayerBeDamaged = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>().canBeDamaged;
        enemyAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {

        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }

    private void Update()
    {

        //After update, enemy check the force and flip. The flip using angles wasn't possible.
        if(enemyLife.isEnemyAlive && player != null)
        {
                 direction = player.position - transform.position;


        if (direction.x < 0 )
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (direction.x >= 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        }
       

    }
    private void FixedUpdate()
    {
        //this if is to confirm if enemy is dead or not
        if (enemyLife.isEnemyAlive)

        {    //this if it's to check if the path is made or not
                if (path == null)
                {
                    return;
                }

                //as the path isn't null they check if the enemy reached the end of the path
                if (currentWayPoint >= path.vectorPath.Count)
                {
                    reachedTheEnd = true;
                    return;
                }

                //the enemy didn't reach the end of the path so the script from here it's to create and update 
                // the path
                else
                {
                    reachedTheEnd = false;
                }
            if(player != null)
            {
                Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - enemyRigidBody2D.position).normalized;
                 Vector2 force = 1.5f * speed * Time.fixedDeltaTime * direction;


            //this if is to confirm if the player is between the range to shot and the range to stop
            if (Vector2.Distance(enemyRigidBody2D.position, player.position) < distanceToGoTOPlayer &&
                Vector2.Distance(enemyRigidBody2D.position, player.position) > distanceToStop)
            {


                enemyRigidBody2D.AddForce(force);
                float distance = Vector2.Distance(enemyRigidBody2D.position, path.vectorPath[currentWayPoint]);

                if (distance < nextWayPointDistance)
                {
                    currentWayPoint++;
                }
            }


            // if player is inside the range to shoot, enemy shoot
            if (Vector2.Distance(enemyRigidBody2D.position, player.position) <= distanceToStop) 
            {
                if (coroutineRunning == false)
                {
                    enemyRigidBody2D.AddForce(force / forceToStop);
                   StartCoroutine(Shoot());
                }
            }


            // if player is outside the range to shoot, enemy shoot
            if (Vector2.Distance(enemyRigidBody2D.position, player.position) >= distanceToStop )
            {
                if (coroutineRunning == true)
                {
                    StopCoroutine(Shoot());
                }
            }


        }
            }
          else
        {
            return;
        }

    }


    //create all the paths that enemy can go
    private void UpdatePath()
    {
        if (seeker.IsDone() && player != null)
        {
            seeker.StartPath(enemyRigidBody2D.position, player.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    } 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceToGoTOPlayer);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToStop);
    }

    private IEnumerator Shoot()
    {
        if (player.transform != null && CanPlayerBeDamaged)
         {
             coroutineRunning = true;


            while (enemyRigidBody2D.velocity.magnitude > 0.01)
            {
            yield return new WaitForFixedUpdate();
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            pointshoot.eulerAngles = new Vector3(0f, 0f, angle + offsetAngle);


            enemyAudioSource.PlayOneShot(attackSound);

            GameObject bullets = Instantiate(bullet, pointshoot.transform.position, pointshoot.rotation);
            bullets.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * enemyBulletSpeed;

             yield return new WaitForSeconds(timeToShoot);
        }

       
        coroutineRunning = false;
    }


}
