using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotAnimation : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    float speed;
    float previousSpeed;
    private enum State
    {
        idle,
        walk
    }

    State state = State.idle;
    State previousState = State.idle;

    private void Update()
    {
        speed = agent.velocity.magnitude;

        if (speed == 0) state = State.idle;
        else state = State.walk;

        if (state != previousState)
        {
            ChangeAnimation();
            previousState = state;
        }

    }

    private void ChangeAnimation()
    {
        if(state == State.idle)
        {
            animator.SetTrigger("Stop");

        }
        else if (state == State.walk)
        {
            animator.SetTrigger("Walk");
        }
    }
}
