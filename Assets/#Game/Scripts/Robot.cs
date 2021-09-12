using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    public enum State
    {
        idle,
        moveToPoint,
        moveToStorage,
        moveToFabric,
        moveToShelter
    }

    [SerializeField] Transform boxPoint;
    [SerializeField] GameObject selectMarker;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameEvent robotSelect;
    [SerializeField] Animator animator;

    List<Task> taskPool = new List<Task>();
    int taskIndex = 0;
    bool selected = false;
    bool alarm = false;
    bool haveBox = false;
    State state = State.idle;
    State previousSate = State.idle;
    Task startTask;

    private void Start()
    {
        startTask.SetData(transform, "Idle");
        SetActiveTask(startTask);
        taskPool.Add(startTask);
    }

    private void Update()
    {
        if (previousSate != state) ChangeAnimation();

        previousSate = state;

        if (agent.remainingDistance == 0 && taskPool.Count > 1 && !alarm)
        {
            taskIndex++;
            if (taskIndex >= taskPool.Count) taskIndex = 0;

            SetActiveTask(taskPool[taskIndex]);
        }
        else if (agent.remainingDistance == 0 && taskPool.Count == 1) state = State.idle;
    }

    public void OnSingleTask(Task task)
    {
        if (selected && !alarm)
        {
            taskPool.Clear();
            taskIndex = 0;

            SetActiveTask(task);
            taskPool.Add(task);
        }
    }

    public void OnPoolTask(Task task)
    {
        if (selected && !alarm)
        {
            taskPool.Add(task);
        }
    }

    public void OnAlarmTask(Task task)
    {
        alarm = true;
        SetActiveTask(task);
    }

    public void OnAlarmStop()
    {
        alarm = false;
        SetActiveTask(taskPool[taskIndex]);
    }

    private void SetActiveTask(Task task)
    {
        agent.destination = task.target;
        state = task.state;
    }

    private void OnMouseDown()
    {
        if (!selected) Select();
        else Deselect();
    }

    private void Select()
    {
        robotSelect.Raise();
        selectMarker.SetActive(true);
        selected = true;
    }

    public void Deselect()
    {
        selectMarker.SetActive(false);
        selected = false;
    }

    public bool NeedsBox()
    {
        if (state == State.moveToStorage && !haveBox) return true;
        else return false;
    }

    public void TakeBox(Box box)
    {
        haveBox = true;
        box.LinkToRobot(boxPoint);
    }

    public void GiveBox()
    {
        haveBox = false;
    }

    private void ChangeAnimation()
    {
        if(state == State.idle)
        {
            animator.ResetTrigger("Walk");
            animator.SetTrigger("Stop");
        }
        else
        {
            animator.ResetTrigger("Stop");
            animator.SetTrigger("Walk");
        }
    }
}
