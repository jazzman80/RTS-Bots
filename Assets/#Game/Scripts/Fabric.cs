using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabric : Building
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            Box box = other.GetComponent<Box>();

            box.LinkToFabric(this.TargetPoint);
        }

        if (other.CompareTag("Robot"))
        {
            Robot robot = other.GetComponent<Robot>();
            robot.GiveBox();
        }
    }

}
