using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatMechanics
{
    public class SightAlways : MonoBehaviour
    {
        private GunTraectory _traectory;
        private GunRotation _gunRotation;
        private Vector3  _rotationSpeedInMoment;
        [SerializeField] private float _bulletSpeed = 6, _radius = 0.25f;
        [SerializeField] private GameObject _gun, _prisel, _gunEnd;
        [SerializeField] private LayerMask  _layersAll;
        void Start()
        {
            _traectory = gameObject.transform.parent.GetComponent<GunTraectory>();
            
            _gunRotation = gameObject.transform.parent.gameObject.GetComponent<GunRotation>();
            //foreach (Transform child in gameObject.transform.parent.transform.parent.transform)
            //{
            //    if (child.gameObject.tag == "sight")
            //    {
            //        _prisel = child.gameObject;
            //    }
            //}
            foreach (Transform child in gameObject.transform.parent.transform)
            {
                if (child.gameObject.tag == "gunRotation")
                {
                    _gun = child.gameObject;
                    foreach (Transform childInCaild in child.transform)
                    {
                        if (childInCaild.gameObject.tag == "Weapon")
                        {
                            foreach (Transform childInCaild2 in childInCaild.transform)
                            {
                                if (childInCaild2.gameObject.tag == "BulletSpawn")
                                {
                                    _gunEnd = childInCaild2.gameObject;
                                }
                            }
                        }
                    }
                }
            }
            
        }
        public void FoundSight(GameObject sight)
        {
            _prisel = sight;
            StartCoroutine(PrisellingAudit());
        }
        private IEnumerator PrisellingAudit()
        {
            yield return new WaitForSeconds(0.1f);
            _rotationSpeedInMoment = new Vector3(_bulletSpeed * Mathf.Cos(2 * Mathf.Asin(_gun.transform.rotation.z) * Mathf.Rad2Deg * Mathf.Deg2Rad),
                _bulletSpeed * Mathf.Sin(2 * Mathf.Asin(_gun.transform.rotation.z) * Mathf.Rad2Deg * Mathf.Deg2Rad), 0);
            _traectory.ShowTrajectory(_gunEnd.transform.position, _rotationSpeedInMoment, _radius, _prisel, _layersAll);
            StartCoroutine(PrisellingAudit());
        }

    }
}