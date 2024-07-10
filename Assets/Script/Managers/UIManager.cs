using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } = null;

    [SerializeField]
    private GameObject panelHUD = null;

    [SerializeField]
    private GameObject[] panelPause = null;

    [SerializeField]
    private GameObject deadDisplay = null;

    [SerializeField]
    private GameObject winDispay = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            panelPause[0].SetActive(false);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
        
    public void ShowPanelPause(bool value)
    {
        if(value)
        {
         panelPause[0].SetActive(true);
        } else if(!value)
        {
            for (int i= 0; i< panelPause.Length; i++)
            {
                panelPause[i].SetActive(false);
            }
        }
   

    }

    public void ShowHUD(bool value)
    {
        panelHUD.SetActive(value);
    }

    public void PlayerDead()
    {
        deadDisplay.SetActive(true);
    }

    public void PlayerWin()
    {
        winDispay.SetActive(true);
    }
}
