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
                    "Classic central block",
                    "Prefab/PartOfBot/CentralBlock/ClassicCentralBlock"
                },
                {
                    "Classic frame",
                    "Prefab/PartOfBot/Frame/ClassicFrame"
                }
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