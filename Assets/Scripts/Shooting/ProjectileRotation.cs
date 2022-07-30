using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotation : MonoBehaviour
{
    public Vector2 OldVector { get; set; }
    public Vector2 NewVector { get; set; }
    public Vector2 FlightDirection { get; set; }

    private void Start()
    {
        OldVector = new Vector2(transform.position.x, transform.position.y);
    }
    private void FixedUpdate()
    {
        NewVector = new Vector2(transform.position.x, transform.position.y);
        FlightDirection = NewVector - OldVector;
        transform.localRotation = Quaternion.FromToRotation(Vector2.right, FlightDirection);
    }

}
