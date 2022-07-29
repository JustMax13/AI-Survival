using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLostConected : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (GetComponent<FixedJoint2D>().connectedBody == null)
            Destroy(GetComponent<ShotGun>());
    }
}
