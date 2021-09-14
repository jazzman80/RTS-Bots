using System.Collections.Generic;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    [SerializeField] Robot robotPrefab;
    [SerializeField] UIManager uiManager;
    [SerializeField] int robotsCount;
    [SerializeField] float instantiationDistance;

    public int RobotsCount { get => robotsCount; }

    private void Start()
    {
        for(int i = 0; i < robotsCount; i++)
        {
            Robot newRobot = Instantiate(robotPrefab);
            newRobot.SetData(transform, i * instantiationDistance, uiManager);
        }
    }

}
