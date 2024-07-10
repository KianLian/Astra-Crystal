using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager   : MonoBehaviour
{

    public static VideoManager Instance { get; private set; } = null;

    public static bool IsPaused { get; private set; } = false;


   [SerializeField]
    private GameObject videoProducer = null;

    [SerializeField]
    private VideoPlayer videoPlayer;

   

    public static bool IsPlaying { get; private set; } = false;

    public void StartPlaying()
    {
            videoPlayer.Play();
          
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
        IsPlaying = true;
        AudioManager.Instance.StopMusic();
    }

    private void Update()
    {
        Debug.Log(videoPlayer);
        videoPlayer.loopPointReached += EndReached;

    }

     private void EndReached(VideoPlayer vp)
    {
        Debug.Log("hihihihih");
        StopPlaying();
    }

    public void StopPlaying()
    {
        videoProducer.SetActive(false);
        IsPlaying = false;
      
        Time.timeScale = 1f;
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }
}
