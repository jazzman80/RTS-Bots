using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskEvent", menuName = "ScriptableObjects/TaskEvent", order = 1)]

public class TaskEvent : ScriptableObject
{
	private List<TaskEventListener> listeners =
		new List<TaskEventListener>();

	public void Raise(Task task)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised(task);
	}

	public void RegisterListener(TaskEventListener listener)
	{ listeners.Add(listener); }

	public void UnregisterListener(TaskEventListener listener)
	{ listeners.Remove(listener); }
}
