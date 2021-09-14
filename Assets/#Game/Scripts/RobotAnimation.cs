using UnityEngine;
using UnityEngine.AI;

public class RobotAnimation : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    private enum State
    {
        idle,
        walk
    }

    State state = State.idle;
    State previousState = State.idle;

    private void Update()
    {
        float speed = agent.velocity.magnitude;

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
