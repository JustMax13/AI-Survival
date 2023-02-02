using System.Collections;
using UnityEngine;
using CombatMechanics.Weapon;
namespace CombatMechanics
{
    public class GunRotation : MonoBehaviour
    {
        public float _minRotation, _maxRotation;
        [SerializeField] float speed;
        [SerializeField] private GameObject _gun;
        [SerializeField] private bool _aI;
        [SerializeField]private GunShot _shot;
        private bool _stopMove, _revers;
        private void Start()
        {
            if (_maxRotation < _minRotation)
                _revers = true;
           
        }
        public void RotationMoveCulcut(float angel)
        {
            if (_revers)
            {
                if (angel > 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _minRotation)
                    angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _minRotation;
                else
                   if (angel < 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _maxRotation)
                    angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _maxRotation;
            }
            else 
            { 
                if (angel > 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _maxRotation)
                angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _maxRotation;
            else
                if (angel < 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _minRotation)
                 angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _minRotation;
            }
            RotationMove(angel);
        }
        private void RotationMove(float angel)
        {

            if (_stopMove == false)
            {
                if (2 * Mathf.Asin(_gun.transform.rotation.z) * Mathf.Rad2Deg <= angel - 0.01f || 2 * Mathf.Asin(_gun.transform.rotation.z) * Mathf.Rad2Deg >= angel + 0.01f)
                {
                    _gun.transform.rotation = Quaternion.Euler(
                       Vector3.MoveTowards(new Vector3(_gun.transform.rotation.x, _gun.transform.rotation.y, 2 * Mathf.Asin(_gun.transform.rotation.z) * Mathf.Rad2Deg),
                        new Vector3(_gun.transform.rotation.x, _gun.transform.rotation.y, angel), speed * Time.deltaTime));

                    StartCoroutine(timer(angel));
                }
                else
                {
                    if (_aI)
                    {
                        //fire (only for AI)
                        _shot.CheckReloadAndShot();
                    }
                   
                }
            }

        }
        private IEnumerator timer(float angel)
        {
            yield return new WaitForSeconds(1f / 60f);
            RotationMove(angel);
        }
        public void StarRotation(bool moveUp)
        {
            float angel;
            if (_revers)
            {
                if (moveUp)
                    angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _minRotation ;
                else
                    angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _maxRotation;
                _stopMove = false;
            }
            else 
            { 
            
             if (moveUp)
                angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _maxRotation;
             else
                angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _minRotation;
             _stopMove = false;
             
            }
            RotationMove(angel);
        }
        public void StopRotation()
        {
            _stopMove = true;
        }
    }
}
