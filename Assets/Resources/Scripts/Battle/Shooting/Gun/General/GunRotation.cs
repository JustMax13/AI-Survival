using System.Collections;
using UnityEngine;
namespace CombatMechanics
{
    public class GunRotation : MonoBehaviour
    {
        [SerializeField] private float _minRotation, _maxRotation, speed, r, p;
        [SerializeField] private GameObject _gun;
        private bool _stopMove;
        public void RotationMoveCulcut(float angel)
        {
            if (angel > _maxRotation)
                angel = _maxRotation;
            else
                if (angel < _minRotation)
                 angel = _minRotation;
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
                    //fire (only for AI)
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
            if (moveUp)
                angel = _maxRotation;
            else
                angel = _minRotation;
            _stopMove = false;
            RotationMove(angel);
        }
        public void StopRotation()
        {
            _stopMove = true;
        }
    }
}
