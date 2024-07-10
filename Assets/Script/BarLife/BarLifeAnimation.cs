using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLifeAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject nextlife = null;

    private void Invisble()
    {
        gameObject.SetActive(false);
    }

    private void NextLife()
    {
        nextlife.SetActive(true);
    }
}
