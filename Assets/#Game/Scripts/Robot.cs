using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    public enum State
    {
        moveToPoint,
        moveToStorage,
        moveToFabric,
        moveToShelter
    }

    [SerializeField] Transform boxPoint;
    [SerializeField] GameObject selectMarker;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameEvent robotSelect;
    [SerializeField] GameEvent robotInShelter;
    [SerializeField] ParticleSystem dustFX;

    List<Task> taskPool = new List<Task>();
    UIManager uiManager;
    int taskIndex = 0;
    bool selected = false;
    bool alarm = false;
    bool haveBox = false;
    bool idle = true;
    Task startTask;
    State state;

    private void Start()
    {
        startTask.SetData(transform, "Terrain");
        SetActiveTask(startTask);
        taskPool.Add(startTask);
    }

    private void Update()
    {

        if (ReachedTarget() && taskPool.Count > 1 && !alarm)
        {
            taskIndex++;
            if (taskIndex >= taskPool.Count) taskIndex = 0;

            SetActiveTask(taskPool[taskIndex]);
        }

        SetIdle();
        SetAlarm();
    }

    private bool ReachedTarget()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) return true;
        else return false;
    }

    private void SetIdle()
    {
        if(selected && !idle && agent.velocity.magnitude == 0)
        {
            idle = true;
            uiManager.SetState("Idle");
            dustFX.Stop();
        }
        else if (selected && idle && agent.velocity.magnitude > 0)
        {
            idle = false;
            uiManager.ShowTaskList(taskPool);
            dustFX.Play();
        }
    }

    private void SetAlarm()
    {
        if (selected && alarm) uiManager.SetState("Move to Shelter");
    }

    public void SetData(Transform spawnerPosition, float instantiationDistance, UIManager manager)
    {
        transform.position = spawnerPosition.position + new Vector3(instantiationDistance, instantiationDistance, 0);
        uiManager = manager;
    }

    public void OnSingleTask(Task task)
    {
        if (selected && !alarm)
        {
            taskPool.Clear();
            taskIndex = 0;

            SetActiveTask(task);
            taskPool.Add(task);

            uiManager.ShowTaskList(taskPool);
        }
    }

    public void OnPoolTask(Task task)
    {
        if (selected && !alarm)
        {
            taskPool.Add(task);

            uiManager.ShowTaskList(taskPool);
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
        uiManager.ShowTaskList(taskPool);
    }

    private void SetActiveTask(Task task)
    {
        agent.destination = task.target;
        state = task.state;
    }

    private void OnMouseDown()
    {
        if (!selected)
        {
            Select();
            if (agent.velocity.magnitude == 0) uiManager.SetState("Idle");
            else uiManager.ShowTaskList(taskPool);
        }
        else Deselect();
    }

    private void Select()
    {
        robotSelect.Raise();
        selectMarker.SetActive(true);
        selected = true;
        uiManager.ShowPortrait();
    }

    public void Deselect()
    {
        selectMarker.SetActive(false);
        selected = false;
        uiManager.HidePortrait();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shelter")) robotInShelter.Raise();
    }
}
