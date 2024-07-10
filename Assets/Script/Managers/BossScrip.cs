using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScrip : MonoBehaviour
{

    private Rigidbody2D myrigidbody = null;
    private Animator bossAnimator = null;

    [SerializeField]
    private Transform[] feet = new Transform[2];
    [SerializeField]
    private LayerMask groundLayerMask = 0;
    private Collider2D[] groundDetectionColliders = new Collider2D[1];


    private bool onGround = true;

    [SerializeField]
    private List<Transform> pointsToJump = new List<Transform>();
    private Transform randomPointsToJump = null;
    private Transform pointChoosed = null;

    [SerializeField]
    private int strenghJump = 350;
    private bool canJump = false;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform shootPoint = null;
    private Transform player = null;
    private Vector2 direction;
    [SerializeField]
    private float offsetAngle = 0;
    [SerializeField]
    private float numberMaxToShoot = 2f;
    [SerializeField]
    private bool canMove = false;

    [SerializeField]
    private float projectileBullet = 0;
    private bool canTurn = true;
    [SerializeField]
    private float fireRate= 0.6f;
    [SerializeField]
    private float waitTimeToChooseThePoint = 3f;

    private EnemyLife bossLife = null;
    [SerializeField]
    private GameObject dialogueAfterDeath = null;

    private int layerBoss = 14;
    private int layerToCollide = 16;

    private AudioSource bossAudioSource = null;
    [SerializeField]
    private AudioClip attack = null;



    private float timeToMove;
    [SerializeField]
    private float duration;
    [SerializeField]
    private Transform[] points = null;
    private Vector3 startPosition;
    private Vector3 targetPosition;


    public void DisableCollision()
    {
        Physics2D.IgnoreLayerCollision(layerBoss, layerToCollide, true);
    }

    private void Start()
    {
        //if(dialogo terminado então)
        myrigidbody = GetComponent<Rigidbody2D>();
        bossAnimator = GetComponent<Animator>();
        bossLife = GetComponent<EnemyLife>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossAudioSource = GetComponent<AudioSource>();
        StartCoroutine(ChooseRandomly());
    }

    private void CanDoActions()
    {
        enabled = true;
    }

    private void Update()
    {
       
    direction = player.position - transform.position;
       // Debug.Log(direction.x);
        if (direction.x >= 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (!bossLife.isEnemyAlive)
        {
            bossAnimator.SetTrigger("Death");
            dialogueAfterDeath.SetActive(true);
            CanNotDoAction();
            
        }
        
    
    }

    private void FixedUpdate()
    {
        onGround = CheckForFloor();
        bossAnimator.SetBool("onGrounded", onGround);

   
        //colocado no fixedupdate por envolver fisica
        if (onGround && canJump)
        {
            StartCoroutine(ChooseRandomly());
        }
        

    }



   private bool CheckForFloor()
   {
            return Physics2D.OverlapPointNonAlloc(feet[0].position, groundDetectionColliders, 
                groundLayerMask) > 0 ||
                Physics2D.OverlapPointNonAlloc(feet[1].position, groundDetectionColliders
                , groundLayerMask) > 0;
   }
    

    private IEnumerator ChooseRandomly()
    {
        canJump = false;

        int a = 0;
        for (a = 0; a <= numberMaxToShoot; a++)
        {
            Debug.Log("StartCoroutine");
            StartCoroutine(Shoot());
            yield return new WaitForSeconds(fireRate);

            if (a >= numberMaxToShoot)
            {
                yield return new WaitForSeconds(waitTimeToChooseThePoint);
                ChooseThePoint();
                bossAnimator.SetTrigger("Jump");
              myrigidbody.AddForce(Vector2.up * strenghJump);
             yield return new WaitForSeconds(0.5f);

                Teletranporter();
                yield return null;
            }
        }
    }

    private IEnumerator Shoot()
    {

        Debug.Log("shooted");

        if (player != null)
        {
           float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           shootPoint.eulerAngles = new Vector3(0f, 0f, angle + offsetAngle);

            bossAudioSource.PlayOneShot(attack);

           GameObject bullets = Instantiate(projectile, shootPoint.transform.position,
           shootPoint.rotation);


            Vector3 dir = (player.transform.position - transform.position).normalized;
            bullets.GetComponent<Rigidbody2D>().velocity = dir*  projectileBullet;


           yield return null;
        }
       
    }


    // call in animator
    private void Teletranporter()
    {
        transform.position = randomPointsToJump.position;
        canJump = true;
    }

    private void ChooseThePoint()
    {
        if(randomPointsToJump == pointChoosed)
        {
            randomPointsToJump = pointsToJump[Random.Range(0, pointsToJump.Count)];
            pointChoosed = randomPointsToJump;
        }
    }

    public void CanShowUp()
    {
        CanDoActions();
    }

    private void CanNotDoAction()
    {
        enabled = false;
    }

    private void CorrectSurImageAfterDeath()
    {
        startPosition = points[0].position;
        targetPosition = points[1].position;
        timeToMove = Time.deltaTime;

        float t = (Time.time - timeToMove) / duration;

        transform.position = new Vector3(
            Mathf.SmoothStep(startPosition.x, targetPosition.x, t),
            Mathf.SmoothStep(startPosition.y, targetPosition.y, t),
            0);
    }
}

