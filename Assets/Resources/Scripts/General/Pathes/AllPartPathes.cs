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
                // Armor
                {
                    "EasyArmor",
                    "Prefab/PartOfBot/Part/ArmorPlates/EasyArmor"
                },

                // FasteningBlock
                {
                    "CarbonNoise",
                    "Prefab/PartOfBot/Part/BaseBlock/CarbonNoise"
                },
                {
                    "CarbonShine",
                    "Prefab/PartOfBot/Part/BaseBlock/CarbonShine"
                },

                // Wheels
                {
                    "BigWheel",
                    "Prefab/PartOfBot/Part/Wheels/BigWheel"
                },

                // CentralBlocks
                {
                    "ClassicCentralBlock",
                    "Prefab/PartOfBot/Part/CentralBlock/ClassicCentralBlock"
                },

                // Guns
                {
                    "OldGun",
                    "Prefab/PartOfBot/Part/Guns/OldGun"
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