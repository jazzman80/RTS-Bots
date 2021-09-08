using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] TaskEvent singleTask;
    [SerializeField] TaskEvent poolTask;

    Task nullTask = new Task();

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetTask();

            if (Input.GetKey(KeyCode.LeftShift)) poolTask.Raise(SetTask());
            else singleTask.Raise(SetTask());
        }
    }

    private Task SetTask()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Task newTask = new Task();
            newTask.SetData(hit);
            return newTask;
        }
        else return nullTask;
    }
}
