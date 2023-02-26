using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatMechanics.AI
{
    public class AIShooting : MonoBehaviour
    {
        //private bool _can;
        private int _auditStepsMinCount, _auditStepsMaxCount;
        private Vector3 _rotationSpeedMin, _rotationSpeedMax, _rotationSpeedInMoment;
        private float _rotationInMoment, _rotationMin, _rotationMax;
        [SerializeField] private float _bulletSpeed = 6, _radius = 0.25f;

        [SerializeField] private GunTraectory _traectory;
        [SerializeField] private PaintTraectory[] _traectoryPoint;
        private GunRotation _gunRotation;


        [SerializeField] private GameObject _gun, _prisel, _player,_gunEnd;
        [SerializeField] private LayerMask /*_playerLayer,*/ _layerPlayerDetal, _layersAll;

        [SerializeField] private List<float> _auditStepsMin, _auditStepsMax, _auditXMin, _auditXMax;

        void Start()
        {
            
            _traectory = gameObject.transform.parent.GetComponent<GunTraectory>();
            _gun = gameObject.transform.parent.gameObject;
            _gunRotation = _gun.GetComponent<GunRotation>();
            //foreach (Transform child in gameObject.transform.parent.transform.parent.transform)
            //{
            //    if (child.gameObject.tag == "sight")
            //    {
            //        _prisel = child.gameObject;
            //    }
            //}
            _prisel= Instantiate(_prisel, gameObject.transform.position, Quaternion.identity);
            _prisel.transform.parent = gameObject.transform.parent.transform.parent;
            foreach (Transform child in gameObject.transform.parent.transform)
            {
                if (child.gameObject.tag == "gunRotation")
                {
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
        public void FoundPlayer(GameObject player)
        {
            _player = player.gameObject;

        }
        public void StartProtses()
        {
            StartCoroutine(StartWaiting());
        }
        private IEnumerator StartWaiting()
        {
            yield return new WaitForSeconds(1f);

            _rotationMin = 2 * Mathf.Asin(_gun.transform.rotation.z) * Mathf.Rad2Deg + _gunRotation.MinRotationAngel;
            _rotationMax = 2 * Mathf.Asin(_gun.transform.rotation.z) * Mathf.Rad2Deg + _gunRotation.MaxRotationAngel;
            _rotationSpeedMin = new Vector3(_bulletSpeed * Mathf.Cos(_rotationMin * Mathf.Deg2Rad),
                _bulletSpeed * Mathf.Sin(_rotationMin * Mathf.Deg2Rad), 0);
            _rotationSpeedMax = new Vector3(_bulletSpeed * Mathf.Cos(_rotationMax * Mathf.Deg2Rad),
                _bulletSpeed * Mathf.Sin(_rotationMax * Mathf.Deg2Rad), 0);

            _traectoryPoint[0].ShowTrajectory(_gunEnd.transform.position, _rotationSpeedMin, _radius, _layersAll);

            _traectoryPoint[2].ShowTrajectory(_gunEnd.transform.position, _rotationSpeedMax, _radius, _layersAll);
            FirstAuditCalcut();

        }


        private void FirstAuditCalcut()
        {
            float descriminant;

            descriminant = _rotationSpeedMax.y * _rotationSpeedMax.y - 2 * Physics.gravity.y * (_gunEnd.transform.position.y - _player.transform.position.y);

            if (descriminant > 0)
            {
                _auditStepsMax[0] = (_rotationSpeedMax.y + Mathf.Sqrt(descriminant)) / Physics.gravity.y;

                if (descriminant > 0)
                {
                    _auditStepsMax[1] = (_rotationSpeedMax.y - Mathf.Sqrt(descriminant)) / Physics.gravity.y;

                    if (_auditStepsMax[1] > 0)
                        _auditStepsMaxCount = 2;
                    else
                        _auditStepsMaxCount = 1;
                }
                else _auditStepsMaxCount = 1;

                descriminant = _rotationSpeedMin.y * _rotationSpeedMin.y - 2 * Physics.gravity.y * (_gunEnd.transform.position.y - _player.transform.position.y);

                if (descriminant > 0)
                {
                    _auditStepsMin[0] = (_rotationSpeedMin.y + Mathf.Sqrt(descriminant)) / Physics.gravity.y;

                    if (descriminant > 0)
                    {
                        _auditStepsMin[1] = (_rotationSpeedMin.y - Mathf.Sqrt(descriminant)) / Physics.gravity.y;

                        if (_auditStepsMin[1] > 0)
                            _auditStepsMinCount = 2;
                        else
                            _auditStepsMinCount = 1;
                    }
                    else _auditStepsMinCount = 1;
                }
                else _auditStepsMinCount = 0;
            }
            else _auditStepsMaxCount = 0;

            FirstAuditXPositionCalcut();
        }
        private void FirstAuditXPositionCalcut()
        {
            _auditXMin[0] = _gunEnd.transform.position.x - _rotationSpeedMin.x * _auditStepsMin[0];
            _auditXMin[1] = _gunEnd.transform.position.x - _rotationSpeedMin.x * _auditStepsMin[1];

            _auditXMax[0] = _gunEnd.transform.position.x - _rotationSpeedMax.x * _auditStepsMax[0];
            _auditXMax[1] = _gunEnd.transform.position.x - _rotationSpeedMax.x * _auditStepsMax[1];



            FirstAudit();
        }
        private void FirstAudit()
        {

            if (_auditStepsMaxCount > 0)
            {
                if (_auditStepsMaxCount == 1)
                {

                    if (_player.transform.position.x != _auditXMin[0] && _player.transform.position.x != _auditXMax[0])
                    {
                        if ((_player.transform.position.x > _auditXMin[0] && _player.transform.position.x < _auditXMax[0]) || (_player.transform.position.x < _auditXMin[0] && _player.transform.position.x > _auditXMax[0]))
                            SecondAuditRotatoionCalcut(0);
                        else
                        {
                            _prisel.SetActive(false);
                            StartCoroutine(StartWaiting());
                        }// ���� ����� ��� ������ ����� ��� ����� 
                    }
                    else
                    {
                        if (_player.transform.position.x == _auditXMin[0])
                            SecondAuditMaxMinRotation(false);
                        else
                            SecondAuditMaxMinRotation(true);
                    }
                }
                else
                {
                    if (_auditStepsMinCount == 0)
                    {
                        if (_player.transform.position.x != _auditXMax[0] && _player.transform.position.x != _auditXMax[1])
                        {
                            if ((_player.transform.position.x > _auditXMax[0] && _player.transform.position.x < _auditXMax[1]) || (_player.transform.position.x < _auditXMax[0] && _player.transform.position.x > _auditXMax[1]))
                            {
                                if (Mathf.Abs(_gunEnd.transform.position.x - ((_auditXMax[0] + _auditXMax[1]) / 2)) < Mathf.Abs(_gunEnd.transform.position.x - _player.transform.position.x))
                                    SecondAuditRotatoionCalcut(0);
                                else
                                    SecondAuditRotatoionCalcut(1);
                            }
                            else
                            {
                                _prisel.SetActive(false);
                                StartCoroutine(StartWaiting());
                            }// ���� ����� ��� ������ ����� ��� ����� 
                        }
                        else SecondAuditMaxMinRotation(true);
                    }
                    else
                    {
                        if (_player.transform.position.x != _auditXMin[0] && _player.transform.position.x != _auditXMax[0])
                        {
                            if ((_player.transform.position.x > _auditXMin[0] && _player.transform.position.x < _auditXMax[0]) || (_player.transform.position.x < _auditXMin[0] && _player.transform.position.x > _auditXMax[0]))
                                SecondAuditRotatoionCalcut(0);
                            else
                            {
                                if (_player.transform.position.x != _auditXMin[1] && _player.transform.position.x != _auditXMax[1])
                                {
                                    if ((_player.transform.position.x > _auditXMin[1] && _player.transform.position.x < _auditXMax[1]) || (_player.transform.position.x < _auditXMin[1] && _player.transform.position.x > _auditXMax[1]))
                                        SecondAuditRotatoionCalcut(1);
                                    else
                                    {
                                        _prisel.SetActive(false);
                                        StartCoroutine(StartWaiting());
                                    }// ���� ����� ��� ������ ����� ��� �����, ������ �� ������� �������(���������)
                                }
                                else
                                {
                                    if (_player.transform.position.x == _auditXMin[1])
                                        SecondAuditMaxMinRotation(false);
                                    else
                                        SecondAuditMaxMinRotation(true);
                                }
                            }
                        }
                        else
                        {
                            if (_player.transform.position.x == _auditXMin[0])
                                SecondAuditMaxMinRotation(false);
                            else
                                SecondAuditMaxMinRotation(true);
                        }
                    }
                }
            }
            else StartCoroutine(StartWaiting());
        }
        private void SecondAuditMaxMinRotation(bool max)
        {
            if (max)
                _rotationSpeedInMoment = _rotationSpeedMax;
            else
                _rotationSpeedInMoment = _rotationSpeedMax;

            Priselling();
        }
        private void SecondAuditRotatoionCalcut(int step)
        {
            float speedX = _rotationSpeedMin.x, speedY = _rotationSpeedMin.y, time, descriminant, xx;

            if (_rotationMax - _rotationMin < 0)
            {
                if (step == 0)
                    step = 2;
                else
                    step = 3;
            }

            switch (step)
            {
                case 0:
                    {
                        for (float i = _rotationMin; i < _rotationMax; i += 0.01f)
                        {
                            descriminant = Mathf.Pow(_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad), 2)
                                - 2 * Physics.gravity.y * (_gunEnd.transform.position.y - _player.transform.position.y);

                            if (descriminant >= 0)
                            {
                                time = (-_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad) - Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                                xx = (_gunEnd.transform.position.x + _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad) * time);

                                if (Physics2D.OverlapCircle(new Vector2(xx, _player.transform.position.y), _radius, _layerPlayerDetal, -10, 10))
                                {
                                    speedX = _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad);
                                    speedY = _bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad);
                                    _rotationInMoment = i;
                                    break;
                                }
                            }
                        }

                        break;
                    }
                case 1:
                    {
                        for (float i = _rotationMin; i < _rotationMax; i += 0.01f)
                        {
                            descriminant = Mathf.Pow(_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad), 2)
                                - 2 * Physics.gravity.y * (_gunEnd.transform.position.y - _player.transform.position.y);

                            if (descriminant >= 0)
                            {
                                time = (-_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad) + Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                                xx = (_gunEnd.transform.position.x + _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad) * time);

                                if (Physics2D.OverlapCircle(new Vector2(xx, _player.transform.position.y), _radius, _layerPlayerDetal, -10, 10))
                                {
                                    speedX = _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad);
                                    speedY = _bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad);
                                    _rotationInMoment = i;
                                    break;
                                }
                            }
                        }

                        break;
                    }
                case 2:
                    {
                        for (float i = _rotationMin; i > _rotationMax; i -= 0.01f)
                        {
                            descriminant = Mathf.Pow(_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad), 2)
                                - 2 * Physics.gravity.y * (_gunEnd.transform.position.y - _player.transform.position.y);

                            if (descriminant >= 0)
                            {
                                time = (-_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad) - Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                                xx = (_gunEnd.transform.position.x + _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad) * time);

                                if (Physics2D.OverlapCircle(new Vector2(xx, _player.transform.position.y), _radius, _layerPlayerDetal, -10, 10))
                                {
                                    speedX = _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad);
                                    speedY = _bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad);
                                    _rotationInMoment = i;
                                    break;
                                }
                            }
                        }

                        break;
                    }
                case 3:
                    {
                        for (float i = _rotationMin; i > _rotationMax; i -= 0.01f)
                        {
                            descriminant = Mathf.Pow(_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad), 2)
                                - 2 * Physics.gravity.y * (_gunEnd.transform.position.y - _player.transform.position.y);

                            if (descriminant >= 0)
                            {
                                time = (-_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad) + Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                                xx = (_gunEnd.transform.position.x + _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad) * time);

                                if (Physics2D.OverlapCircle(new Vector2(xx, _player.transform.position.y), _radius, _layerPlayerDetal, -10, 10))
                                {
                                    speedX = _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad);
                                    speedY = _bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad);
                                    _rotationInMoment = i;
                                    break;
                                }
                            }
                        }

                        break;
                    }
            }

            _rotationSpeedInMoment = new Vector3(speedX, speedY, 0);
            Priselling();
        }
        private void Priselling()
        {
            _traectory.ShowTrajectory(_gunEnd.transform.position, _rotationSpeedInMoment, _radius, _prisel, _layersAll);

            StartCoroutine(PrisellingAudit());
        }
        private IEnumerator PrisellingAudit()
        {
            yield return new WaitForSeconds(0.5f);
            if (Physics2D.OverlapCircle(_prisel.transform.position, _radius, _layerPlayerDetal, -10, 10))
            {
                _traectoryPoint[1].ShowTrajectory(_gunEnd.transform.position, _rotationSpeedInMoment, _radius, _layersAll);
                _gunRotation.RotationMoveCulcut(_rotationInMoment);
            }
            //shooting
            //else// ������� �� ����������
            //_prisel.SetActive(false);                                                                                                             

            //StartCoroutine(StartWaiting());
        }
    }
}
