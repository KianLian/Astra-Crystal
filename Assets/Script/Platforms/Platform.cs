using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private Transform[] points = null;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    [SerializeField]
    private int targetIndex = 1;

    [SerializeField]
    private float startTime = 0f;
    private float startCanResetTime = 0;
    private float startJustMoveTime = 0;
    [SerializeField]
    private float duration = 4f;

    public bool JustMove = false;
    public bool ContinuosMove = false;
    public bool canReset = false;

    private void Start()
    {
        if(points.Length > 1)
        {
        startPosition = points[0].position;
        targetPosition = points[targetIndex].position;
        }
    }

    private void Update()
    {


        if (canReset)
        {
            if (startCanResetTime == 0)
            {
                startPosition = transform.position;
                targetPosition = points[0].position;
                startCanResetTime = Time.time;
            }
            if (startCanResetTime > 0)
            {
                RetrivePosition();
            }

        }
        else
        {
            startCanResetTime = 0;
        }

        if (JustMove)
        {
            if (startJustMoveTime == 0)
            {
                startPosition = transform.position;
                targetPosition = points[1].position;
                startJustMoveTime = Time.time;
            }
            if (startJustMoveTime > 0)
            {
                PlatformMoving();
            }
        }
        else
        {
            startJustMoveTime = 0;
        }


        if (ContinuosMove)   
        {
           if (startTime == 0)
           {
                startPosition = transform.position;
                targetPosition = points[1].position;
                startTime = Time.time;
           }

           if (startTime > 0)
           {

                ContinueMoving();

           }
        }
        else
        {
            startTime = 0;
            targetIndex = 1;
        }
        

    }

    private void RetrivePosition()
    {
       

        Debug.Log("called"); 
        float t = (Time.time - startCanResetTime) / duration ;

        transform.position = new Vector3(
             Mathf.Lerp(startPosition.x, targetPosition.x, t),
             Mathf.Lerp(startPosition.y, targetPosition.y, t),
             0);

        if (t > 1)
        {
            canReset = false;
            startCanResetTime = 0;
            targetIndex = 1;
        }
    }



    //Script to Move 1 time
    private void PlatformMoving()
    {
        float t = (Time.time - startJustMoveTime) / duration;

        transform.position = new Vector3(
            Mathf.SmoothStep(startPosition.x, targetPosition.x, t),
            Mathf.SmoothStep(startPosition.y, targetPosition.y, t),
            0);
    }


    //Scripts to Continuing moving time
    public void ContinueMoving()
    {
        float t = (Time.time - startTime) / duration;

        transform.position = new Vector3(
            Mathf.Lerp(startPosition.x, targetPosition.x, t),
            Mathf.Lerp(startPosition.y, targetPosition.y, t),
            transform.position.z
            );

        if (t >= 1f)
        {
            MoveToNextPoint();
        }
    }

    private void MoveToNextPoint()
    {
        startPosition = targetPosition;

        targetIndex = (targetIndex + 1) % points.Length;
        targetPosition = points[targetIndex].position;
        startTime = Time.time;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < points.Length; ++i)
        {
            Gizmos.DrawWireSphere(points[i].position, 2f);
            Gizmos.DrawWireSphere(
                   points[(i + 1) % points.Length].position,
                2f);
        }
    }
}
