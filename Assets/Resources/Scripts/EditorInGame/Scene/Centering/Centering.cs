using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class Centering : MonoBehaviour
    {
        public static void CentringToPoint(Transform[] transforms, Vector3 point)
        {
            Vector3 centerOfPart = FindCenter(transforms);

            Vector3 dragOn = point - centerOfPart;

            foreach (var item in transforms)
                item.position += dragOn;
        }
        public static Vector3 FindCenter(Transform[] transforms)
        {
            var centerOfPart = new Vector3();
            foreach (var item in transforms)
                centerOfPart += item.position;

            centerOfPart /= transforms.Length;

            return centerOfPart;
        }
    }
}