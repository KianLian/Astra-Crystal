using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : Player
{
    [Header("Lifes")]
    [SerializeField]
    public int playerLifes = 3;
    [SerializeField]
    private GameObject[] threelifes= new GameObject[4];

    [SerializeField]
    private Animator[] barLifeAnimator = new Animator[3];

    public bool playerFullLife { get; private set; } = true;
    public bool takeItDamage { get; private set; } = false;

    [Header("ChangeColor")]
    [SerializeField]
    private Color recoveryColor = new Color(0, 1, 0, 1);
    [SerializeField]
    private Color damageColor = new Color(0, 1, 0, 1);
    private float halfLife = 0.5f;
    [SerializeField]
    private Color backUpColor;

    [Header("AcidContact")]
    private int acidDamage = 1;
    [SerializeField]
    private int timeTodamageAgain = 2;
    [SerializeField]
    private float changinTime = 0;

    private PlayerMovement playerMovement = null;
    public bool canBeDamaged = true;

    private int[] layerToCollide = new int[3];
    [SerializeField]
    private float timeForPlayerUntargetble = 2f;
    private int layerPlayer = 8;
    private int layerEnemy = 6;
    private int layerEnemyBullet = 7;
    private int layerBossBullet = 13;

    [SerializeField]
    private GameObject playerManager = null;

    [SerializeField]
    private AudioClip[] damageSound = null;
    [SerializeField]
    private AudioClip deathSound = null;

    private void GoToMainMenu()
    {
        UIManager.Instance.PlayerDead();
    }

    public void StartAcidDamage()
    {
        StartCoroutine(AcidDamage());
        playerMovement.CanJump = false; ;
    }

    public void StopAcidDamage()    
    {
        StopCoroutine(AcidDamage());
        playerMovement.CanJump = true;
    }

    private void Start()
    {
        canBeDamaged = true;
        playerLifes = 3;
        playerMovement = GetComponent<PlayerMovement>();
        layerToCollide[0] = layerEnemy; 
        layerToCollide[1] = layerEnemyBullet;
        layerToCollide[2] = layerBossBullet;
        playerIsAlive = true;
        playerFullLife = true;
        ActivateCollision();
    }

    private void Update()
    {
        if(DialogueManager.IsTalking)
        {
            threelifes[playerLifes].SetActive(false);
        }
        else
        {
            threelifes[playerLifes].SetActive(true);
        }

        if (!canBeDamaged)
        {
            DesactivateCollision();
        }
       else
        {
            ActivateCollision();
        }

        //   Debug.Log(canBeDamaged);
    }


    public void TakeDamage(int damage)
    {
        if (playerIsAlive && canBeDamaged)
        {
            StartCoroutine(PlayerInvencible());
            StartCoroutine(Colored(damageColor, backUpColor, changinTime));
                playerLifes -= damage;

            SoundDamage();

            if (playerLifes < 0)
                    {
                        playerLifes = 0;
                    }
                    if (playerLifes == 0)
                    {
                      Die();
                    }
            barLifeAnimator[playerLifes].SetTrigger("Damaged");
            threelifes[playerLifes].SetActive(true);
            playerFullLife = false;

        }
    }

    public void RecoverLife(int recovery)
    {
        if (playerIsAlive)
        {

            barLifeAnimator[playerLifes-1].SetTrigger("Recovered");


            playerLifes += recovery;

            StartCoroutine(Colored(recoveryColor, backUpColor, changinTime));

            if (playerLifes >=  3)
            {
                playerLifes = 3;
                playerFullLife = true;
            }
        }
    }

    private IEnumerator AcidDamage()
    {
        while(playerLifes > 0)
        {
            TakeDamage(acidDamage);
            yield return new WaitForSeconds(timeTodamageAgain);
        }
    }


    private IEnumerator Colored(Color endValue, Color startValue,  float duration)
    {
        canBeDamaged = false;
        float time = 0;

        while (time < duration * halfLife)
        {
            playerSpriteRender.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        playerSpriteRender.color = endValue;

        while (duration > time && time > duration * halfLife)
        {
            playerSpriteRender.color = Color.Lerp(endValue, startValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        playerSpriteRender.color = startValue;

        canBeDamaged = true;

    }

    public IEnumerator PlayerInvencible()
    {
        Debug.Log(timeForPlayerUntargetble);
        Debug.Log("called the yahhh");

        if (!canBeDamaged)
        {
            DesactivateCollision();

            yield return new WaitForFixedUpdate();
        }

      else
        {
            ActivateCollision();
        }
        yield return new WaitForFixedUpdate();  
        Debug.Log("descalled the yaahhh");
        
      
    }

    public void Die()
    {
        myAnimator.SetTrigger("Die");
        playerAudioSource.PlayOneShot(deathSound);
        playerIsAlive = false;
        canBeDamaged = false;
        Cursor.visible = true;
        DesactivateCollision();
    }

    private void ActivateCollision()
    {
        for (int i = 0; i < layerToCollide.Length; i++)
        {
            Physics2D.IgnoreLayerCollision(layerPlayer, layerToCollide[i], false);
        
        }
    }

    private void DesactivateCollision()
    {
        for (int i = 0; i < layerToCollide.Length; i++)
        {
            Physics2D.IgnoreLayerCollision(layerPlayer, layerToCollide[i], true);
        }
    }

    private void Destroy()
    {
        Destroy(playerManager);
    }

    private void SoundDamage()
    {
        AudioClip audioDamageToPlay = damageSound[UnityEngine.Random.Range(0, damageSound.Length)];
        playerAudioSource.PlayOneShot(audioDamageToPlay);
    }

}