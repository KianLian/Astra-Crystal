using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; } = null;

    private int level = 0;
    public static bool IsPaused { get; private set; } = false;

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
    }

    private IEnumerator LoadNextLevelAsync(int level)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);
        while (!asyncLoad.isDone)
        {
            print(asyncLoad.progress);
            yield return null;
        }
        print(asyncLoad.progress);
        yield return null;
    }

    public void LoadNextLevel(int level)
    {
            
            SceneManager.LoadScene(level);
        Cursor.visible = false;
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadNextLevelAsync(level));
        level = 0;

        Cursor.visible = true;
    }

    public void PauseGame(bool pause)
    {
       
        if (pause)
        { if(VideoManager.Instance)
            {
                if (VideoManager.IsPlaying)
                {
                    VideoManager.Instance.PauseVideo();
                }
                else if (!VideoManager.IsPlaying)
                {
                    Time.timeScale = 0f;
                }

                Cursor.visible = true;
                IsPaused = true;
            }
           
       
          
        }
        else
        {
            if(VideoManager.Instance)
            {
                if (VideoManager.IsPlaying)
                {
                    VideoManager.Instance.StartPlaying();
                }
                else if (!VideoManager.IsPlaying)
                {
                    Time.timeScale = 1f;
                }

                Cursor.visible = false;
                IsPaused = false;
            }
               
         
        }

        UIManager.Instance.ShowPanelPause(IsPaused);
    }
}
