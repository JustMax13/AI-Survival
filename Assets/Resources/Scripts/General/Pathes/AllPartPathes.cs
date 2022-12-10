using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace General.Pathes
{
    public class AllPartPathes : MonoBehaviour
    {
        public static Dictionary<string,string> PartPathes { get; }

        static AllPartPathes()
        {
            PartPathes = new Dictionary<string, string>
            {
                { 
                    "EasyArmor",
                    "Prefab/PartOfBot/Armor/EasyArmor"
                },
                {
                    "HardArmor",
                    "Prefab/PartOfBot/Armor/HardArmor"
                },
                {
                    "BigWheel",
                    "Prefab/PartOfBot/Wheel/BigWheel"
                },
                {
                    "ClassicCentralBlock",
                    "Prefab/PartOfBot/CentralBlock/ClassicCentralBlock"
                },
                {
                    "CarbonNoise",
                    "Prefab/PartOfBot/BaseBlock/CarbonNoise"
                },
                {
                    "CarbonShine",
                    "Prefab/PartOfBot/BaseBlock/CarbonShine"
                },
            };
        }
        public static string GetPathOfPart(string key)
        {
            foreach (var item in PartPathes)
            {
                if (item.Key == key)
                    return item.Value;
            }
            throw new System.NotImplementedException($"Can't found part by key {key}");
        }
    }
}