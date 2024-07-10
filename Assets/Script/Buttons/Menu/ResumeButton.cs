using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    private bool pause = false;
    
        public void ResumeGame()
    {
        GameManager.Instance.PauseGame(pause);
    }
}
