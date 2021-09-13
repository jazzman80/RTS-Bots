using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] Animator animator;
    
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

    private void OnEnable()
    {
        animator.Play("Box Create");
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

    public void LinkToFabric(Transform fabricTransform)
    {
        isTransported = false;
        transform.position = fabricTransform.position;
        transform.rotation = fabricTransform.rotation;
        animator.Play("Box Recycle");
    }

    public void OnRecycleAnimationDone()
    {
        gameObject.SetActive(false);
    }

}
