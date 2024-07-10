using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanish : MonoBehaviour
{
    [SerializeField]
    private GameObject floor = null;

    private void VanishThisFloor()
    {
        floor.SetActive(false);
    }
}
