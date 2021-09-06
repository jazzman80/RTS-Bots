using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    public Transform boxPoint;
    
    private enum State
    {
        idle,
        moveToPoint,
        moveToStorage,
        moveToFabric,
        moveToShelter
    }
    
    [SerializeField] GameObject selectMarker;
    [SerializeField] NavMeshAgent agent;

    bool selected = false;
    State state = State.idle;

    private void Update()
    {
        if (selected && Input.GetMouseButtonDown(1)) SetTarget();
    }

    private void SetTarget()
    {
        Deselect();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);

            string objectType = hit.collider.gameObject.tag;
            Debug.Log("I'm move to " + objectType);

            switch (objectType)
            {
                case "Terrain":
                    state = State.moveToPoint;
                    break;

                case "Fabric":
                    state = State.moveToFabric;
                    break;

                case "Storage":
                    state = State.moveToStorage;
                    break;

                case "Shelter":
                    state = State.moveToShelter;
                    break;
            }
        }
    }

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

    public bool NeedsBox()
    {
        if (state == State.moveToStorage) return true;
        else return false;
    }
}
