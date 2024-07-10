using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    private AudioSource audioSource = null;
    [SerializeField]
    private AudioClip audioClip = null;

    [SerializeField]
    private float finishAudioTime = 1.5f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(audioClip);
            Destroy(gameObject, finishAudioTime);
        }
    }

}
