using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBossScene : MonoBehaviour
{
    private int level = 2;
    private void GoToBoss()
    {
        GameManager.Instance.LoadNextLevel(level);
    }
}
