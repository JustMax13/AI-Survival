using General.Pathes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class CenteringPart : MonoBehaviour
    {
        [SerializeField] private GameObject _parentOfAllPart;

        public void CentringPartToCenterOfScene()
        {
            Transform[] transforms = GetPartTransforms();

            Centering.CentringToPoint(transforms, Vector3.zero);
        }

        private Transform[] GetPartTransforms()
        {
            var children = _parentOfAllPart.GetComponentsInChildren<PartPath>();
            var transforms = new Transform[children.Length];
            for (int i = 0; i < transforms.Length; i++)
                transforms[i] = children[i].transform;

            return transforms;
        }
    }
}