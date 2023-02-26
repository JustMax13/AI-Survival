using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    public class MiniCameraSpawn : MonoBehaviour
    {
        [SerializeField] private Vector4[] pointToSpawn;
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
                    
                            obj =Instantiate(cameraToSpawn, gameObject.transform.position, Quaternion.identity);
                            obj.transform.parent = gameObject.transform;
                            obj.transform.GetChild(0).GetComponent<Camera>().rect = new Rect(pointToSpawn[count].x, pointToSpawn[count].y, pointToSpawn[count].z, pointToSpawn[count].w);
                    foreach (Transform childInCaild in child.transform)
                    {
                        if (childInCaild.gameObject.GetComponent<SightAlways>())
                        {
                            childInCaild.GetComponent<SightAlways>().FoundSight(obj);
                        }
                    }
                    count++;
                    if (count >= pointToSpawn.Length)
                        break;
                }
            }
        }
    }
}
