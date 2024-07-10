using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private int level = 0;
 
    public void GotoMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }
}

