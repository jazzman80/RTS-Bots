using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : Building
{
    [SerializeField] int robotsCount;
    [SerializeField] RobotSpawner robotSpawner;
    [SerializeField] Animator gatesAnimator;

    bool gatesIsClosed = false;

    private void Start()
    {
        robotsCount = robotSpawner.RobotsCount;
    }

    private void Update()
    {
        if(robotsCount==0 && !gatesIsClosed)
        {
            gatesIsClosed = true;
            gatesAnimator.Play("Close");
        }
    }

    public void OnRobotInside()
    {
        robotsCount--;
    }

    public void OnAlarmStop()
    {
        gatesIsClosed = false;
        gatesAnimator.Play("Open");
        robotsCount = robotSpawner.RobotsCount;
    }
}
