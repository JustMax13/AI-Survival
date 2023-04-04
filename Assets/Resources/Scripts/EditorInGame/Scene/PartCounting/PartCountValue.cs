using General.PartOfBots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.Counting
{
    public class PartCountValue : MonoBehaviour
    {
        [SerializeField] private float _occupiedPlace;
        [SerializeField] private TypeOfPart _typeOfPart;

        public float OccupiedPlace { get => _occupiedPlace; set => _occupiedPlace = value; }
        public TypeOfPart TypeOfPart { get => _typeOfPart; set => _typeOfPart = value; }
    }
}

