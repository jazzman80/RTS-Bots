using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] Transform targetPoint;

    public Transform TargetPoint { get => targetPoint; }

}
