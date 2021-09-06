using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] Box boxPrefab;
    [SerializeField] Transform generationPoint;
    
    List<Box> boxPool = new List<Box>();
    Box activeBox;

    private void Start()
    {
        GenerateBox();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            Robot robot = other.gameObject.GetComponent<Robot>();

            if (robot.NeedsBox()) activeBox.LinkToRobot(robot.boxPoint);
        }
    }

    private void GenerateBox()
    {
        bool needsInstantiation = true;

        foreach(Box box in boxPool)
        {
            if (!box.gameObject.activeSelf)
            {
                box.gameObject.SetActive(true);
                box.ResetPosition(generationPoint);
                activeBox = box;
                needsInstantiation = false;
                break;
            }
        }

        if (needsInstantiation)
        {
            Box box = Instantiate(boxPrefab);
            box.ResetPosition(generationPoint);
            activeBox = box;
            boxPool.Add(box);
        }
    }
}
