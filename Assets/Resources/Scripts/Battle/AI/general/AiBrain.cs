using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatMechanics.AI
{
    public class AiBrain : MonoBehaviour
    {
        [SerializeField] GameObject ckesh, _player;
        [SerializeField] private AIShooting[] _guns;
        // Start is called before the first frame update
        void Start()
        {

            Instantiate(ckesh, new Vector3(0, 0, 0), Quaternion.identity);

        }
        public void FoundPlayer(GameObject player)
        {
            _player = player.gameObject;
           _guns[0].FoundPlayer(_player);
            StartCoroutine(StartWaiting());
        }
        private IEnumerator StartWaiting()
        {
            yield return new WaitForSeconds(1f);
            
            _guns[0].StartProtses();
            StartCoroutine(StartWaiting());
        }
    }
}
