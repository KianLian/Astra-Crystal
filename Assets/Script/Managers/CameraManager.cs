using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; } = null;


    public static bool Isfollowing = false;
    public static bool IsfollowingOnVertical = false;
    public static bool IsfollowingOnHorizontal = false;

    public float setY = 12;
    public float setX = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);   
        }

        IsfollowingOnHorizontal = false;
        IsfollowingOnVertical = false;
        Isfollowing = false;

    }

    public void CanFollow()
    {
        Isfollowing = !Isfollowing;
    }

    public void CanFollowVetical()
    {
        IsfollowingOnVertical = !IsfollowingOnVertical;
    }

    public void CanFollowHorizontal()
    {  
        IsfollowingOnHorizontal = !IsfollowingOnHorizontal;
    }

}
