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
        if (selected && Input.GetMouseButtonDown(1))
        {
            if(!Input.GetKey(KeyCode.LeftShift))
            {
                ResetTaskPool();
                SetTask();
                SetActiveTask(taskPool[0]);
            }
            else SetTask();
        }

        if (agent.remainingDistance == 0) state = State.idle;

        // if idle and taskPool not empty start new task
        if (state == State.idle && taskPool.Count > 1)
        {
            taskIndex++;
            if (taskIndex == taskPool.Count) taskIndex = 0;
            SetActiveTask(taskPool[taskIndex]);
        }
    }

    private void ResetTaskPool()
    {
        taskPool.Clear();
        taskIndex = 0;
    }

    private void SetTask()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            Task newTask = new Task();
            newTask.SetData(hit);
            //SetActiveTask(newTask);
            taskPool.Add(newTask);
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
}
