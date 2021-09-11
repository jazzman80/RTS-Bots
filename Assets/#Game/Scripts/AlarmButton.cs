using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmButton : MonoBehaviour
{
    [SerializeField] GameObject alarmButton;
    [SerializeField] GameObject alarmStopButton;

    public void OnAlarm()
    {
        alarmButton.SetActive(false);
        alarmStopButton.SetActive(true);
    }

    public void OnAlarmStop()
    {
        alarmButton.SetActive(true);
        alarmStopButton.SetActive(false);
    }
}
