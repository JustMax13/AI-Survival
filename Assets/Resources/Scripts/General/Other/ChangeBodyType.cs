using UnityEngine;

namespace General.Other
{
    public class ChangeBodyType : MonoBehaviour
    {
        public static void InChildrenToKinematic(GameObject parent)
        {
            var childrens = parent.GetComponentsInChildren<Rigidbody2D>();

            foreach (var item in childrens)
                item.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}