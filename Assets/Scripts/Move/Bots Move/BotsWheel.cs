using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Move;

public class BotsWheel : MonoBehaviour, IMover
{
    [SerializeField]
    [Range(0, 120)]
    private float _addSpeed = 50f;
    public float AddSpeed 
    { 
        get => _addSpeed; 
        set 
        {
            if(value < 0) 
                value = 0;
            _addSpeed = value;
        } 
    }

    public void MoveLeft() => GetComponent<Rigidbody2D>().AddTorque(AddSpeed);

    public void MoveRight() => GetComponent<Rigidbody2D>().AddTorque(-AddSpeed);
    private void FixedUpdate()
    {
        if (GetComponent<WheelJoint2D>().connectedBody == null)
        {
            Destroy(GetComponent<WheelJoint2D>());
            Destroy(GetComponent<BotsWheel>());
        } 
    }
}
