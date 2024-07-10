using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    private AudioSource audioSource = null;
    [SerializeField]
    private AudioClip audioClip = null;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SelecteHover()
    {
        audioSource.PlayOneShot(audioClip);
    }

}
