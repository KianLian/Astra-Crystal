using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("Atrributes")]
    [SerializeField]
    private float enemyspeed = 0;
    [SerializeField]
    private float distanceToStop = 2f;
    [SerializeField]
    private float distanceToGo = 4f;
    [SerializeField]
    private float distanceToRun = 2f;

    private float x;
    private float y;
    private float t = 0f;

    private Transform player = null;
    private Animator enemyanimator = null;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyanimator = GetComponent<Animator>();
        StartCoroutine(WithoutPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > distanceToGo)
        {
            WalkWithoutPlayer();
        }

            if (Vector2.Distance(transform.position, player.position) > distanceToStop && 
            Vector2.Distance(transform.position, player.position) < distanceToGo)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyspeed *
                Time.deltaTime);
            enemyanimator.SetFloat("Movement", Mathf.Abs(enemyspeed));
        }
        else if (Vector2.Distance(transform.position, player.position) < distanceToStop &&
            Vector2.Distance(transform.position, player.position) > distanceToRun)
        {
            transform.position = this.transform.position;
            enemyanimator.SetTrigger("Attack");
        }
        else if (Vector2.Distance(transform.position, player.position) < distanceToRun)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -enemyspeed *
               Time.deltaTime);
            enemyanimator.SetFloat("Movement", Mathf.Abs(enemyspeed));
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceToGo);
    }

    private void Attack()
    {
        Debug.Log("hit, hit!");
    }

    private void WalkWithoutPlayer()
    {
        x = (2 * Mathf.Sin(t)) + transform.position.x;
        y = transform.position.y;

        transform.position = new Vector2(x, y);
        t += 0.005f;
    }

    private IEnumerator WithoutPlayer()
    {
      while(Vector2.Distance(transform.position, player.position) > distanceToGo)
        {
            WalkWithoutPlayer(); 
            yield return new WaitForSeconds(1);
            yield return null;
        }

    }

}
