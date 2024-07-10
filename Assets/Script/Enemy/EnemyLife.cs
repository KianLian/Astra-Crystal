using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField]
    private float enemyife = 2;
    public bool isEnemyAlive = true;

    [SerializeField]
    private Color targetColor = new Color(0, 1, 0, 1);
    private Color startValue;
    [SerializeField]
    private Material materialToChange = null;

    [SerializeField]
    private float timeToDie = 0;
    [SerializeField]
    private bool isNotTheBoss = false;

    private void Awake()
    {

        materialToChange = gameObject.GetComponent<Renderer>().material;
        startValue = materialToChange.color;
    }

    public void EnemyTakeDamage(int damage)
    {
        if(isEnemyAlive)
        {
            enemyife -= damage;

         StartCoroutine(LerpFunction(targetColor, 2));

        if (enemyife < 0)
        {
            enemyife = 0;
          
        }

        if (enemyife == 0)
        {
           Die();
        }
        }
    }

    IEnumerator LerpFunction(Color endValue, float duration)
    {
        float time = 0;
      

        while (time < duration/2)
        {
            materialToChange.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        materialToChange.color = endValue;
        while (duration > time && time > duration / 2)
        {
            materialToChange.color = Color.Lerp(endValue, startValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        materialToChange.color = startValue;
    }

    private void Die()
    {   
        if(!isNotTheBoss)
            {
             isEnemyAlive = false;
             Destroy(gameObject, timeToDie);
        }
        else
        {
            isEnemyAlive = false;
        }
      
    }
}
