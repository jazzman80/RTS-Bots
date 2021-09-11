using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Task
{
    public string taskName;
    public Robot.State state;
    public Vector3 target;

    public void SetData(RaycastHit hit)
    {
        string tag = hit.collider.gameObject.tag;
        SetName(tag);
        SetState(tag);
        SetTarget(hit);
    }

    public void SetData(Transform point, string name)
    {
        SetName(name);
        SetState(name);
        target = point.position;
    }

    private void SetTarget(RaycastHit hit)
    {
        if (taskName == "Move to Terrain") target = hit.point;
        else
        {
            target = hit.collider.gameObject.GetComponent<Building>().TargetPoint.position;
        }
    }

    private void SetName(string tag)
    {
        taskName = "Move to " + tag;
    }

    private void SetState(string tag)
    {
        switch (tag)
        {
            case "Terrain":
                state = Robot.State.moveToPoint;
                break;

            case "Fabric":
                state = Robot.State.moveToFabric;
                break;

            case "Storage":
                state = Robot.State.moveToStorage;
                break;

            case "Shelter":
                state = Robot.State.moveToShelter;
                break;
        }
    }
}
