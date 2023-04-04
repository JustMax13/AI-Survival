using General.PartOfBots;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.Counting
{
    public class PartCountingSystem : MonoBehaviour
    {
        [SerializeField] private int _numberOfCentralBlock;
        [SerializeField] private int _numberOfBaseBlock;
        [SerializeField] private int _numberOfArmorPlates;
        [SerializeField] private int _numberOfGun;
        [SerializeField] private int _numberOfWheels;

        public static Dictionary<TypeOfPart, int> MaxNumberOfType { get; private set; }
        public static Dictionary<TypeOfPart, int> CurrentNumberOfType { get; private set; }
        private void Start()
        {
            MaxNumberOfType = new Dictionary<TypeOfPart, int>
            {
                { TypeOfPart.CentralBlock, _numberOfCentralBlock },
                { TypeOfPart.BaseBlock, _numberOfBaseBlock },
                { TypeOfPart.ArmorPlates, _numberOfArmorPlates },
                { TypeOfPart.Gun, _numberOfGun },
                { TypeOfPart.Wheel, _numberOfWheels }
            };
            CurrentNumberOfType = new Dictionary<TypeOfPart, int>
            {
                { TypeOfPart.CentralBlock, 0 },
                { TypeOfPart.BaseBlock, 0 },
                { TypeOfPart.ArmorPlates, 0 },
                { TypeOfPart.Gun, 0 },
                { TypeOfPart.Wheel, 0 }
            };
        }
        public static bool AddIsPosible(TypeOfPart typeOfPart, int occupiedPlace)
        {
            if (MaxNumberOfType.GetValueOrDefault(typeOfPart) - CurrentNumberOfType.GetValueOrDefault(typeOfPart) < occupiedPlace)
                return false;
            else
                return true;
        }
        public static void AddPart(TypeOfPart typeOfPart, int occupiedPlace)
        {
            int currentNumber = CurrentNumberOfType.GetValueOrDefault(typeOfPart);
            CurrentNumberOfType.Remove(typeOfPart);
            CurrentNumberOfType.Add(typeOfPart, currentNumber - occupiedPlace);
        }
        public static void RemovePart(TypeOfPart typeOfPart, int occupiedPlace)
        {
            int currentNumber = CurrentNumberOfType.GetValueOrDefault(typeOfPart);
            CurrentNumberOfType.Remove(typeOfPart);
            CurrentNumberOfType.Add(typeOfPart, currentNumber + occupiedPlace);
        }
    }
}