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

    Queue<Task> taskPool = new Queue<Task>();
    List<Task> tasksPool = new List<Task>();
    int taskIndex = 0;
    bool selected = false;
    bool haveBox = false;
    State state = State.idle;

    private void Update()
    {
        if (selected && Input.GetMouseButtonDown(1))
        {
            if(!Input.GetKey(KeyCode.LeftShift)) ResetTaskPool();
            SetTask();
        }

        if (agent.remainingDistance == 0) state = State.idle;

        // if idle and taskPool not empty start new task
        if (state == State.idle && tasksPool.Count > 1)
        {
            taskIndex++;
            if (taskIndex == tasksPool.Count) taskIndex = 0;
            SetActiveTask(tasksPool[taskIndex]);
        }
    }

    private void ResetTaskPool()
    {
        tasksPool.Clear();
        taskIndex = 0;
    }

    private void SetTask()
    {
        //Deselect();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            Task newTask = new Task();
            newTask.SetData(hit);
            //taskPool.Enqueue(newTask);
            SetActiveTask(newTask);
            tasksPool.Add(newTask);
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
