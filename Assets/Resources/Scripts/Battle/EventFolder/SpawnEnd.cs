using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace CombatMechanics
{
    public class SpawnEnd : MonoBehaviour
    {
        public static event Action SpawnEnded;
        public static void CallSpawnEnded() => SpawnEnded?.Invoke();
    }
}

