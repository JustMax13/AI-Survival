using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotation : MonoBehaviour
{
    public Vector2 OldVector { get; private set; }
    public Vector2 NewVector { get; private set; }
    public Vector2 FlightDirection { get; private set; }
    private void Start()
    {
        OldVector = new Vector2(transform.position.x, transform.position.y);
    }
    private void FixedUpdate()
    {
        NewVector = new Vector2(transform.position.x, transform.position.y);
        FlightDirection = NewVector - OldVector;
        OldVector = NewVector;

        transform.Rotate(0, 0, -Vector2.Angle(transform.TransformDirection(Vector2.right), FlightDirection));
    }
}