using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatMechanics.AI
{
    public class AiBrain : MonoBehaviour
    {
        [SerializeField] GameObject ckesh, _player;
        [SerializeField] private List <AIShooting> _guns;
        [SerializeField] private Vector2[] _allPoints;
        // Start is called before the first frame update
        void Start()
        {

            Instantiate(ckesh, new Vector3(0, 0, 0), Quaternion.identity);
            
            foreach (Transform child in gameObject.transform)
            {

                if (child.gameObject.tag == "Gun")
                {
                    foreach (Transform childInCaild in child.transform)
                    {
                        if (childInCaild.gameObject.GetComponent<AIShooting>())
                        {
                            _guns.Add(childInCaild.GetComponent<AIShooting>());
                        }
                    }

                }
            }
        }
        public void FoundAllPoints(Vector2[] point) => _allPoints = point;        
        public void FoundPlayer(GameObject player)
        {
            _player = player.gameObject;
            StartCoroutine(StartWaiting());
            _guns[0].FoundPlayer(_player);
        }
        private IEnumerator StartWaiting()
        {
            yield return new WaitForSeconds(1f);
            
            _guns[0].StartProtses();
            StartCoroutine(StartWaiting());
        }
     
    }
}
