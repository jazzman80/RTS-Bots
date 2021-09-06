using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public void ResetPosition(Transform storage)
    {
        transform.position = storage.position;
        transform.rotation = storage.rotation;
    }
}
