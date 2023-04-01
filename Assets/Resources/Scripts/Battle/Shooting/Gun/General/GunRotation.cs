using CombatMechanics.Weapon;
using System.Collections;
using UnityEngine;
namespace CombatMechanics
{
    public class GunRotation : MonoBehaviour
    {
        [SerializeField] private float _minRotationAngel, _maxRotationAngel;
        [SerializeField] float _speed;
        [SerializeField] private Transform _rotationGunPoint;
        [SerializeField] private bool _ai;
        [SerializeField] private GunShot _shot;

        private bool _stopMove, _revers;

        public float MinRotationAngel { get => _minRotationAngel; /*set { _minRotationAngel = value; }*/ }
        public float MaxRotationAngel { get => _maxRotationAngel; /*set { _maxRotationAngel = value; }*/ }

        private void Start()
        {
            if (_maxRotationAngel < _minRotationAngel)
                _revers = true;
        }
        public void RotationMoveCulcut(float angel)
        {
            if (_revers)
            {
                if (angel > 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _minRotationAngel)
                    angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _minRotationAngel;
                else
                   if (angel < 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _maxRotationAngel)
                    angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _maxRotationAngel;
            }
            else
            {
                if (angel > 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _maxRotationAngel)
                    angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _maxRotationAngel;
                else
                if (angel < 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _minRotationAngel)
                    angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _minRotationAngel;
            }
            RotationMove(angel);
        }
        private void RotationMove(float angel)
        {
            if (_stopMove == false)
            {
                if (2 * Mathf.Asin(_rotationGunPoint.rotation.z) * Mathf.Rad2Deg <= angel - 0.01f || 2 * Mathf.Asin(_rotationGunPoint.rotation.z) * Mathf.Rad2Deg >= angel + 0.01f)
                {
                    _rotationGunPoint.rotation = Quaternion.Euler(
                       Vector3.MoveTowards(new Vector3(_rotationGunPoint.rotation.x, _rotationGunPoint.rotation.y, 2 * Mathf.Asin(_rotationGunPoint.rotation.z) * Mathf.Rad2Deg),
                        new Vector3(_rotationGunPoint.rotation.x, _rotationGunPoint.rotation.y, angel), _speed * Time.deltaTime));

                    StartCoroutine(Timer(angel));
                }
                else
                {
                    if (_ai)
                    {
                        //fire (only for AI)
                        _shot.CheckReloadAndShot();
                    }

                }
            }

        }
        private IEnumerator Timer(float angel)
        {
            yield return new WaitForSeconds(1f / 60f);
            RotationMove(angel);
        }

        // старая реализация
        //public void StarRotation(bool moveUp)
        //{
        //    float angel;
        //    if (_revers)
        //    {
        //        if (moveUp)
        //            angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _minRotationAngel;
        //        else
        //            angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _maxRotationAngel;
        //        _stopMove = false;
        //    }
        //    else
        //    {

        //        if (moveUp)
        //            angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _maxRotationAngel;
        //        else
        //            angel = 2 * Mathf.Asin(transform.rotation.z) * Mathf.Rad2Deg + _minRotationAngel;
        //        _stopMove = false;

        //    }
        //    RotationMove(angel);
        //}
        //public void StopRotation()
        //{
        //    _stopMove = true;
        //}

        // методы вызываются на клик кнопки поворота, они уже подключены
        public void RotateUp()
        {
            // место для кода поворота
        }
        public void RotateDown()
        {
            // место для кода поворота
        }
    }
}
