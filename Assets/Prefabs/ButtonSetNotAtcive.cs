using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetNotAtcive : MonoBehaviour
{
    [SerializeField]
    private GameObject SetNotAtcive = null;
   
    public void SetNotActive()
    {
        SetNotAtcive.SetActive(false);
    }

}
