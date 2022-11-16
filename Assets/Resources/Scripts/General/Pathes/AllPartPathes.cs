using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace General.Pathes
{
    public class AllPartPathes : MonoBehaviour
    {
        public static List<TurnkeyPath> PartPathes { get; }

        static AllPartPathes()
        {
            PartPathes = new List<TurnkeyPath>();
            PartPathes.Add(new TurnkeyPath("Classic central block",
                "Prefab/PartOfBot/CebtralBlock/ClassicCentralBlock"));
            PartPathes.Add(new TurnkeyPath("Classic frame",
                "Prefab/PartOfBot/Frame/ClassicFrame"));
        }
        public static string GetPathOfPart(string key)
        {
            foreach (var item in PartPathes)
            {
                if (item.Key == key)
                    return item.Path;
            }
            throw new System.NotImplementedException($"Can't found part by key {key}");
        }
    }
}