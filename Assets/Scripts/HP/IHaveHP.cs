using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HP
{
    interface IHaveHP
    {
        float HP { get; set; }
        void MakeDamage(float damage);
    }
}
