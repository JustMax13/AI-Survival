using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatMechanics
{
    public class RotationTest : MonoBehaviour
    {
        [SerializeField] float _min, _max, _time;
        [SerializeField] GunRotation _gun;
        void Start()
        {
            StartCoroutine(timer());
        }

        private IEnumerator timer()
        {
            yield return new WaitForSeconds(_time);
            float random;
            random = Random.Range(_min, _max);
            _gun.RotationMoveCulcut(random);
            StartCoroutine(timer());
        }
    }
}
