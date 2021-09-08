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

    List<Task> taskPool = new List<Task>();
    int taskIndex = 0;
    bool selected = false;
    bool haveBox = false;
    State state = State.idle;

    private void Update()
    {
        if (taskPool.Count != 0)
        {
            if (agent.remainingDistance == 0 && taskPool.Count > 1)
            {
                SetActiveTask(taskPool[taskIndex]);
                taskIndex++;
                if (taskIndex >= taskPool.Count) taskIndex = 0;
            }
        }
    }

    public void OnSingleTask(Task task)
    {
        if (selected)
        {
            taskPool.Clear();
            taskIndex = 0;

            SetActiveTask(task);
            taskPool.Add(task);
        }
    }

    public void OnPoolTask(Task task)
    {
        if (selected)
        {
            taskPool.Add(task);
        }
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
        selectMarker.SetActive(true);
        selected = true;
    }

    private void Deselect()
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
}
