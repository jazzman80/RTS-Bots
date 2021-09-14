using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TaskListItem[] taskItemList;
    [SerializeField] GameObject robotPortrait;

    public void ShowTaskList(List<Task> taskList)
    {
        Clear();

        for(int i=0; i < taskList.Count; i++)
        {
            taskItemList[i].SetText(taskList[i].taskName);
        }
    }

    public void SetState(string name)
    {
        Clear();
        taskItemList[0].SetText(name);
    }

    private void Clear()
    {
        foreach (TaskListItem item in taskItemList) item.Clear();
    }

    public void ShowPortrait()
    {
        robotPortrait.SetActive(true);
    }

    public void HidePortrait()
    {
        robotPortrait.SetActive(false);
        Clear();
    }
}
