using UnityEngine;
using UnityEngine.Events;

public class TaskEventListener : MonoBehaviour
{
    public TaskEvent Event;
    public UnityEvent<Task> Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(Task task)
    { Response.Invoke(task); }
}
