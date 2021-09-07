using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    Transform target;
    string name;
    Robot.State state;

    public void SetData(RaycastHit hit)
    {
        string tag = hit.collider.gameObject.tag;

        SetName(hit, tag);
        SetState(hit, tag);
        SetTarget(hit);
    }

    private void SetTarget(RaycastHit hit)
    {
        target.position = hit.point;
    }

    private void SetName(RaycastHit hit, string tag)
    {
        name = "Move to " + tag;
    }

    private void SetState(RaycastHit hit, string tag)
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
