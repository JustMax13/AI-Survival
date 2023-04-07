using CombatMechanics.Weapon;
using UnityEngine;
namespace CombatMechanics
{
    public class GunRotation : MonoBehaviour
    {
        [SerializeField] private float _minRotationAngel, _maxRotationAngel, _speed;
        [SerializeField] private Transform _rotationGunPoint, _father;
        [SerializeField] private bool _ai;
        [SerializeField] private GunShot _shot;

        float _angel;
        private bool _stopMove, _revers;

        public float MinRotationAngel { get => _minRotationAngel; /*set { _minRotationAngel = value; }*/ }
        public float MaxRotationAngel { get => _maxRotationAngel; /*set { _maxRotationAngel = value; }*/ }

        private void Start()
        {
            _stopMove = true;
            if (_maxRotationAngel < _minRotationAngel)
                _revers = true;
            if (_father == null)
                _father = gameObject.transform;
        }
        public void RotationMoveCulcut(float angel)
        {
            if (_revers)
            {
                if (angel > _father.transform.eulerAngles.z + _minRotationAngel)
                    angel = _father.transform.eulerAngles.z + _minRotationAngel;
                else
                   if (angel < _father.transform.eulerAngles.z + _maxRotationAngel)
                    angel = _father.transform.eulerAngles.z + _maxRotationAngel;
            }
            else
            {
                if (angel > _father.transform.eulerAngles.z + _maxRotationAngel)
                    angel = _father.transform.eulerAngles.z + _maxRotationAngel;
                else
                if (angel < _father.transform.eulerAngles.z + _minRotationAngel)
                    angel = _father.transform.eulerAngles.z + _minRotationAngel;
            }
            _angel = angel;
            _stopMove = false;
        }
        private void RotationMove(float angel)
        {
            if (_stopMove == true)
            {
                return;
            }
            if (_rotationGunPoint.transform.eulerAngles.z <= angel - 0.01f || _rotationGunPoint.transform.eulerAngles.z >= angel + 0.01f)
            {
                _rotationGunPoint.rotation = Quaternion.Euler(
                   Vector3.MoveTowards(new Vector3(_rotationGunPoint.rotation.x, _rotationGunPoint.rotation.y, _rotationGunPoint.transform.eulerAngles.z),
                    new Vector3(_rotationGunPoint.rotation.x, _rotationGunPoint.rotation.y, angel), _speed * Time.deltaTime));
            }
            else
            {
                if (_ai)
                {
                    //fire (only for AI)
                    _shot.CheckReloadAndShot();
                }
                _stopMove = true;
            }


        }

        private void Update()
        {
            RotationMove(_angel);
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

        public void RotateUp()
        {
            float angel;
            angel = _father.transform.eulerAngles.z + _maxRotationAngel;
            _angel = angel;
            _stopMove = false;
        }
        public void RotateDown()
        {
            float angel;
            angel = _father.transform.eulerAngles.z + _minRotationAngel;
            _angel = angel;
            _stopMove = false;
        }
        public void RotateStop()
        {
            _stopMove = true;
        }
    }
}
