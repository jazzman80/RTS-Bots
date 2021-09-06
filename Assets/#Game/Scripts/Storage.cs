using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] Box boxPrefab;
    [SerializeField] Transform generationPoint;
    
    List<Box> boxPool = new List<Box>();

    private void Start()
    {
        GenerateBox();
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
                needsInstantiation = false;
                break;
            }
        }

        if (needsInstantiation)
        {
            Box box = Instantiate(boxPrefab);
            box.ResetPosition(generationPoint);
            boxPool.Add(box);
        }
    }
}
