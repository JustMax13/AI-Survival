using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    public class MiniCameraSpawn : MonoBehaviour
    {
        [SerializeField] private Vector2[] pointToSpawn;
        [SerializeField] private GameObject cameraToSpawn;
        private int count;
        // Start is called before the first frame update
        void Start()
        {
            GameObject obj;
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.tag == "Gun")
                {
                    foreach (Transform childInchild in gameObject.transform)
                    {
                        if (childInchild.gameObject.tag == "sight")
                        {
                            obj = Instantiate(cameraToSpawn, childInchild.transform.position, Quaternion.identity);
                            obj.transform.parent = childInchild.transform;
                            obj.GetComponent<Camera>().ViewportToScreenPoint (pointToSpawn[count]);
                            count++;
                            break;
                        }
                    }
                    if (count >= pointToSpawn.Length)
                        break;
                }
            }
        }
    }
}
