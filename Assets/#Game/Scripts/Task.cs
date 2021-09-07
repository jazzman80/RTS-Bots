using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
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

    private void SetTarget(RaycastHit hit)
    {
        target = hit.point;

        Debug.Log("Set position");
    }

    private void SetName(string tag)
    {
        taskName = "Move to " + tag;
        Debug.Log("Name " + taskName);
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

        Debug.Log("Tag " + tag);
    }
}
