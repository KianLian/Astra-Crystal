using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    [SerializeField]
    GameObject objectToSetNotAtcive = null;

    [SerializeField]
    private GameObject objectToSetAtcive = null;

    public void PessButton()
    {
        objectToSetAtcive.SetActive(true);
        objectToSetNotAtcive.SetActive(false);
    }
}
