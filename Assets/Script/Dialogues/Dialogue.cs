using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Dialogue {

    [SerializeField]
    [TextArea(1, 3)]
    private string[] nameChar;

    [SerializeField]
    [TextArea(1, 3)]
    private string[] sentences;


    [SerializeField]
    private GameObject[] imageCharacter;

    public string[] Name
    {
        get
        {
            return nameChar;
        }
    }


    public string[] Sentences
    {
        get
        {
            return sentences; 
        }
    }

    public GameObject[] ImageCharacter
    {
        get
        {

            return imageCharacter;
        }
    }
}
