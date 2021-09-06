using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField] GameObject selectMarker;

    bool selected = false;

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
}
