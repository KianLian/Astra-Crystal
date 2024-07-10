using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public  bool Isfollowing = false;
    public  bool IsfollowingOnVertical = false;
    public  bool IsfollowingOnHorizontal = false;

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
