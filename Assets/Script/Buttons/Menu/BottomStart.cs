using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomStart : MonoBehaviour
{
    private int level = 1;
    [SerializeField]
    private GameObject SetNotActive = null;

    public void StartGame()
    {
        GameManager.Instance.LoadNextLevel(level);
        SetNotActive.SetActive(false);
        Time.timeScale = 0f;
    }
}
