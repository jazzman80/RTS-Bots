using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] TaskEvent singleTask;
    [SerializeField] TaskEvent poolTask;
    [SerializeField] TaskEvent alarmTask;
    [SerializeField] Transform shelterPoint;

    Task newTask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetTask();

            if (Input.GetKey(KeyCode.LeftShift)) poolTask.Raise(newTask);
            else singleTask.Raise(newTask);
        }
    }

    private void SetTask()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) newTask.SetData(hit);
    }

    public void OnAlarm()
    {
        newTask.SetAlarmData(shelterPoint);
        alarmTask.Raise(newTask);
    }
}

