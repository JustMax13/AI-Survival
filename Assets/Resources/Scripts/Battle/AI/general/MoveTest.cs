using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatMechanics.AI
{
    public class MoveTest : MonoBehaviour
    {
        [SerializeField] private AIMoving _move;
        [SerializeField] private float _min, _max,_time;
        [SerializeField] private float point;
        private void Start()
        {
            
            StartCoroutine(Waiting());
        }
        private IEnumerator Waiting()
        {
            yield return new WaitForSeconds(_time);
            point = Random.Range(_min, _max);
            _move.StartMove(point);
            StartCoroutine(Waiting());
        }
    }
}

