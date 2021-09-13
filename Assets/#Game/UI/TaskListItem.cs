using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskListItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI item;

    public void SetText(string text)
    {
        item.text = text;
    }

    public void Clear()
    {
        item.text = "";
    }
}
