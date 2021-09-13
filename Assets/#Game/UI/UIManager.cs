using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TaskListItem[] taskItemList;

    public void ShowTaskList(List<Task> taskList)
    {
        Clear();

        for(int i=0; i < taskList.Count; i++)
        {
            taskItemList[i].SetText(taskList[i].taskName);
        }
    }

    public void SetIdleState()
    {
        Clear();
        taskItemList[0].SetText("Idle");
    }

    private void Clear()
    {
        foreach (TaskListItem item in taskItemList) item.Clear();
    }
}
