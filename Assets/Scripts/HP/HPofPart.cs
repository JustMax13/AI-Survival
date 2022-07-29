using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HP;

public class HPofPart : MonoBehaviour, IHaveHP
{
    [SerializeField]
    [Range(0,1000000)] 
    private float _HP;
    public float HP 
    { 
        get { return _HP; } 
        set { _HP = value; } 
    }
    public void MakeDamage(float damage) => HP -= damage;

    private void FixedUpdate()
    {
        if (HP <= 0)
            Destroy(gameObject);
    }
}
