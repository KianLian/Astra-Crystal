using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private AudioSource audioSource = null;
    [SerializeField]
    private AudioClip audioClip = null;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }  
      public void SoundTouch()
        {
            audioSource.PlayOneShot(audioClip);
        }

}
