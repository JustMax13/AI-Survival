using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Pathes
{
    public class PartPath : MonoBehaviour
    {
        [SerializeField] private string _partName;
        public Transform PartTransform => gameObject.transform;
        public string Path => AllPartPathes.GetPathOfPart(_partName);

    }
}