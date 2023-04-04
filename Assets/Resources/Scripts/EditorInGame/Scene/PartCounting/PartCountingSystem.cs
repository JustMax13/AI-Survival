using Editor.Interface;
using General.PartOfBots;
using System;
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

        public static event Action CountPartUpdate;

        public static Dictionary<TypeOfPart, int> MaxNumberOfType { get; private set; }
        public static Dictionary<TypeOfPart, float> CurrentNumberOfType { get; private set; }
        private void Start()
        {
            MaxNumberOfType = new Dictionary<TypeOfPart, int>
            {
                { TypeOfPart.CentralBlock, _numberOfCentralBlock },
                { TypeOfPart.BaseBlock, _numberOfBaseBlock },
                { TypeOfPart.Armor, _numberOfArmorPlates },
                { TypeOfPart.Gun, _numberOfGun },
                { TypeOfPart.Wheel, _numberOfWheels }
            };
            CurrentNumberOfType = new Dictionary<TypeOfPart, float>
            {
                { TypeOfPart.CentralBlock, 0 },
                { TypeOfPart.BaseBlock, 0 },
                { TypeOfPart.Armor, 0 },
                { TypeOfPart.Gun, 0 },
                { TypeOfPart.Wheel, 0 }
            };

            SpawnPart.SpawnPartEnd += OnSpawnPartEnd;
            ButtonForDestroyObject.BeforeDestroyPart += OnDestroyPart;
        }
        //private void Update()
        //{
        //    foreach (var item in CurrentNumberOfType)
        //        Debug.Log($"{item.Key} | кількість деталей на сцені: {item.Value}");
        //}
        public static bool AddIsPosible(PartCountValue partCountValue)
        {
            if (MaxNumberOfType.GetValueOrDefault(partCountValue.TypeOfPart) - CurrentNumberOfType.GetValueOrDefault(partCountValue.TypeOfPart)
                < partCountValue.OccupiedPlace)
                return false;
            else
                return true;
        }
        private static void AddPart(TypeOfPart typeOfPart, float occupiedPlace)
        {
            float currentNumber = CurrentNumberOfType.GetValueOrDefault(typeOfPart);
            CurrentNumberOfType.Remove(typeOfPart);
            CurrentNumberOfType.Add(typeOfPart, currentNumber + occupiedPlace);

            CountPartUpdate?.Invoke();
        }
        private static void RemovePart(TypeOfPart typeOfPart, float occupiedPlace)
        {
            float currentNumber = CurrentNumberOfType.GetValueOrDefault(typeOfPart);
            CurrentNumberOfType.Remove(typeOfPart);
            CurrentNumberOfType.Add(typeOfPart, currentNumber - occupiedPlace);

            CountPartUpdate?.Invoke();
        }
        public static void OnDestroyPart(GameObject gameObject)
        {
            var value = gameObject.GetComponent<PartCountValue>();
            RemovePart(value.TypeOfPart, value.OccupiedPlace);
        }
        public static void OnSpawnPartEnd(GameObject gameObject)
        {
            var value = gameObject.GetComponent<PartCountValue>();
            AddPart(value.TypeOfPart, value.OccupiedPlace);
        }

    }
}