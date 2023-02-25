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
                    //Instantiate(cameraToSpawn, new Vector3(child.transform.position.x, child.transform.position.y,-10), Quaternion.identity);
                    foreach (Transform childInchild in child.transform)
                    {
                        if (childInchild.gameObject.tag == "sight")
                        {
                            obj =Instantiate(cameraToSpawn, new Vector3(childInchild.transform.position.x, childInchild.transform.position.y, -10), Quaternion.identity);
                            obj.transform.parent = childInchild.transform;
                            obj.GetComponent<Camera>().rect = new Rect(pointToSpawn[count].x, pointToSpawn[count].y, 0.15f, 0.15f);
                            //childInchild.transform.parent = gameObject.transform;
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
