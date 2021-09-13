using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    [SerializeField] Robot robotPrefab;
    [SerializeField] int robotsCount;
    [SerializeField] float instantiationDistance;

    public int RobotsCount { get => robotsCount; }

    List<Robot> robotsPool = new List<Robot>();

    private void Start()
    {
        for(int i = 0; i < robotsCount; i++)
        {
            Robot newRobot = Instantiate(robotPrefab);
            newRobot.SetPosition(transform, i * instantiationDistance);
            robotsPool.Add(newRobot);
        }
    }

}
