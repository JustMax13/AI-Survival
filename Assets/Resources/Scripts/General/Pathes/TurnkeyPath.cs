using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Pathes
{
    public struct TurnkeyPath : IPathOfPart
{
        public string Key { get; set; }

        public string Path { get; set; }

        public TurnkeyPath(string key, string path)
        {
            Key = key;
            Path = path;
        }
    }
}