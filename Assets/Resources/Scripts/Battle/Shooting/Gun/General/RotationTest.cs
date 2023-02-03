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
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_time);
            float random;
            random = Random.Range(_min, _max);
            _gun.RotationMoveCulcut(2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + random);
            StartCoroutine(Timer());
        }
    }
}
