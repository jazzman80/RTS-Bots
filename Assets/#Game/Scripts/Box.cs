using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Transform robotTransform;
    bool isTransported = false;

    private void Update()
    {
        if (isTransported)
        {
            transform.position = robotTransform.position;
            transform.rotation = robotTransform.rotation;
        }
    }

    public void ResetPosition(Transform storage)
    {
        transform.position = storage.position;
        transform.rotation = storage.rotation;
    }

    public void LinkToRobot(Transform newRobotTransform)
    {
        robotTransform = newRobotTransform;
        isTransported = true;
    }
}
