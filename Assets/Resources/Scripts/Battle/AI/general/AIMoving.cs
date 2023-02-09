using System.Collections;
using UnityEngine;
namespace CombatMechanics.AI
{
    public class AIMoving : MonoBehaviour
    {
        [SerializeField] private MoveAllWheels _wheel;
        [SerializeField] private GameObject _CPU;
        private bool _right;
        private bool[] _ckekLeft = new bool[3], _ckekRight = new bool[3];//0- down down, 1-down l/r, 2-up l/r
        [SerializeField] private float _xPosition, _position, stopNum = 2;
        [SerializeField] private Transform[] _pointsForCheak;// 0-Right UP 1-Right Down 2- Left Down 3-Left Up
        [SerializeField] private LayerMask _layers;
        private float timeMove;
        public void StartMove(float xPosition)
        {

            if (_CPU.gameObject.transform.position.x > xPosition)
            {
                _wheel.MoveLeftDown();
                _right = false;
            }
            else
            {
                _wheel.MoveRightDown();
                _right = true;
            }
            _xPosition = xPosition;
            timeMove = 0;
            Move();
        }
        private void Move()
        {
            timeMove++;
            if (_right)
            {
                _ckekRight[0] = Physics2D.Raycast(_pointsForCheak[1].position, Vector2.down, 1, _layers);
                _ckekRight[1] = Physics2D.Raycast(_pointsForCheak[1].position, Vector2.right, 1, _layers);
                _ckekRight[2] = Physics2D.Raycast(_pointsForCheak[0].position, Vector2.right, 1, _layers);
            }
            else
            {
                _ckekLeft[0] = Physics2D.Raycast(_pointsForCheak[2].position, Vector2.down, 1, _layers);
                _ckekLeft[1] = Physics2D.Raycast(_pointsForCheak[2].position, Vector2.left, 1, _layers);
                _ckekLeft[2] = Physics2D.Raycast(_pointsForCheak[3].position, Vector2.left, 1, _layers);
            }
            if ((_right == true && (_ckekRight[0] == false || _ckekRight[1] == true || _ckekRight[2] == true))
             || (_right == false && (_ckekLeft[0] == false || _ckekLeft[1] == true || _ckekLeft[2] == true)))
            {
                if (_right)
                {
                    _wheel.MoveRightUp();
                    _wheel.MoveLeftDown();
                }
                else
                {
                    _wheel.MoveLeftUp();
                    _wheel.MoveRightDown();
                }
                StartCoroutine(Stop(_right));
            }
            else
            {
                _position = _CPU.gameObject.transform.position.x;
                if (Mathf.Abs(_position - _xPosition) <= stopNum)
                {
                    if (_right)
                    {
                        _wheel.MoveRightUp();
                        _wheel.MoveLeftDown();
                    }
                    else
                    {
                        _wheel.MoveLeftUp();
                        _wheel.MoveRightDown();
                    }
                    StartCoroutine(Stop(_right));
                }
                else
                {
                    StartCoroutine(Waiting());
                }
            }
        }
        private IEnumerator Waiting()
        {
            yield return new WaitForSeconds(1 / 60f);
            Move();
        }
        private IEnumerator Stop(bool right)
        {
            yield return new WaitForSeconds(0.3f+(timeMove/120));
            if (right)
            {
                _wheel.MoveLeftUp();
            }
            else
            {
                _wheel.MoveRightUp();
            }
        }
    }
}

