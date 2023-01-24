using UnityEngine;
using System.Collections;
namespace CombatMechanics
{
    public class GunTraectory : MonoBehaviour
    {
        private Vector3 _point;
        private float _pointY, _pointX;
        public void ShowTrajectory(Vector3 origin, Vector3 speed, float radius, GameObject prisel, LayerMask layers )
        {
            
            
            bool _working = false;
            int i = 0;
            while (_working ==false)
            {
                i++;
                float _time = i * 0.001f*radius;
                _point= origin + speed * _time + Physics.gravity * _time * _time / 2f;
                _pointX = _point.x;
                _pointY = _point.y;
                _working = Physics2D.OverlapCircle(new Vector2(_pointX, _pointY), radius, layers, -10, 10);
            }
            prisel.transform.position = _point;
            prisel.SetActive(true);
        }
    }
}