using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private Vector2 _min, _max;
        [SerializeField] private Transform _player, _camera;
        

        public void SetPlayer(GameObject player)
        {
            foreach(Transform child in player.transform)
            {
                if (child.gameObject.tag == "CPU")
                {
                    _player = child.transform;
                    _camera.position = new Vector3(_player.position.x , _player.position.y, _camera.position.z);
                    break;
                }
                
            }
            //щоб переписати потім на івент
        }
        private void Update()
        {
            Moving();
        }
        private void Moving()
        {
            if (_player == null)
            {
                return;
            }
            if (_player.position.y > _camera.position.y+_max.y )
            {
                _camera.position = new Vector3(_camera.position.x, _player.position.y- _max.y, _camera.position.z);
            }
            else
            {
                if(_camera.position.y + _player.position.y < _min.y)
                {
                    _camera.position = new Vector3(_camera.position.x, _player.position.y - _min.y, _camera.position.z);
                }
            }
            if (_player.position.x > _camera.position.x+_max.x )
            {
                _camera.position = new Vector3(_player.position.x - _max.x, _camera.position.y, _camera.position.z);
            }
            else
            {
                if(_player.position.x < _camera.position.x + _min.x)
                {
                    _camera.position = new Vector3(_player.position.x - _min.x, _camera.position.y, _camera.position.z);
                }
            }
        }
    }
}
