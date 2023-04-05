using General.Pathes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class CenteringPart : MonoBehaviour
    {
        [SerializeField] private GameObject _parentOfAllPart;

        public void CentringPartToCenterOfScene()
        {
            GetPartTransforms(out Transform[] parts, out Transform[] counters);

            Vector3 centerOfTransform = Centering.FindCenter(parts);
            var allTransform = new Transform[parts.Length + counters.Length];

            int index = 0;
            for (int i = 0; i < parts.Length; i++, index++)
                allTransform[index] = parts[i];
            for (int i = 0; i < counters.Length; i++, index++)
                allTransform[index] = counters[i];

            Centering.CentringToPoint(allTransform, Vector3.zero, centerOfTransform);
        }

        private void GetPartTransforms(out Transform[] parts, out Transform[] partCounter)
        {
            var children = _parentOfAllPart.GetComponentsInChildren<PartPath>();
            parts = new Transform[children.Length];
            for (int i = 0; i < parts.Length; i++)
                parts[i] = children[i].transform;

            var counters = _parentOfAllPart.GetComponentsInChildren<PartCounter>();
            partCounter = new Transform[counters.Length];
            for (int i = 0; i < partCounter.Length; i++)
                partCounter[i] = counters[i].transform;

        }
    }
}