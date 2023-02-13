using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class PartCountValue : MonoBehaviour
    {
        [SerializeField] private int _maxPossibleConnectionToPoint;

        public int MaxPossibleConnectionToPoint { get => _maxPossibleConnectionToPoint; set { _maxPossibleConnectionToPoint = value; } }
    }
}