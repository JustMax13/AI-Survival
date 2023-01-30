using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics.AI
{
    public class AIShooting : MonoBehaviour
    {
        //private bool _can;
        private int _auditStepsMinCount, _auditStepsMaxCount;

        [SerializeField] private Vector3 _rotationMin, _rotationMax;
        [SerializeField] private Vector3 _rotationSpeedMin, _rotationSpeedMax, _rotationSpeedInMoment;

        [SerializeField] private float _bulletSpeed = 6, _radius = 0.25f;

        [SerializeField] private GunTraectory _traectory;
        [SerializeField] private PaintTraectory[] _traectoryPoint;
        [SerializeField] private GameObject[] mm; // переіменувати, бо не зрозуміло, що то таке

        [SerializeField] private GameObject _gun, _prisel, _player, ckesh;
        [SerializeField] private LayerMask /*_playerLayer,*/ _layerPlayerDetal, _layersAll;

        [SerializeField] private List<float> _auditStepsMin, _auditStepsMax, _auditXMin, _auditXMax;

        void Start()
        {
            _rotationSpeedMin = new Vector3(_bulletSpeed * Mathf.Cos(_rotationMin.z * Mathf.Deg2Rad),
                _bulletSpeed * Mathf.Sin(_rotationMin.z * Mathf.Deg2Rad), 0);
            _rotationSpeedMax = new Vector3(_bulletSpeed * Mathf.Cos(_rotationMax.z * Mathf.Deg2Rad),
                _bulletSpeed * Mathf.Sin(_rotationMax.z * Mathf.Deg2Rad), 0);

            Instantiate(ckesh, new Vector3(0, 0, 0), Quaternion.identity);
        }
        public void FoundPlayer(GameObject player)
        {
            _player = player.gameObject;
            StartCoroutine(StartWaiting());
        }
        private IEnumerator StartWaiting()
        {
            yield return new WaitForSeconds(1f);

            _rotationSpeedMin = new Vector3(_bulletSpeed * Mathf.Cos(_rotationMin.z * Mathf.Deg2Rad),
                _bulletSpeed * Mathf.Sin(_rotationMin.z * Mathf.Deg2Rad), 0);

            _traectoryPoint[0].ShowTrajectory(_gun.transform.position, _rotationSpeedMin, _radius, _layersAll);

            _traectoryPoint[2].ShowTrajectory(_gun.transform.position, _rotationSpeedMax, _radius, _layersAll);
            FirstAuditCalcut();
            
        }


        private void FirstAuditCalcut()
        {
            float descriminant;

            descriminant = _rotationSpeedMax.y * _rotationSpeedMax.y - 2 * Physics.gravity.y * (_gun.transform.position.y - _player.transform.position.y);
            
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

                descriminant = _rotationSpeedMin.y * _rotationSpeedMin.y - 2 * Physics.gravity.y * (_gun.transform.position.y - _player.transform.position.y);
                
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
            _auditXMin[0] = _gun.transform.position.x - _rotationSpeedMin.x * _auditStepsMin[0];
            _auditXMin[1] = _gun.transform.position.x - _rotationSpeedMin.x * _auditStepsMin[1];

            _auditXMax[0] = _gun.transform.position.x - _rotationSpeedMax.x * _auditStepsMax[0];
            _auditXMax[1] = _gun.transform.position.x - _rotationSpeedMax.x * _auditStepsMax[1];

            mm[0].transform.position = new Vector3(_auditXMin[0], _player.transform.position.y, 0);
            mm[1].transform.position = new Vector3(_auditXMin[1], _player.transform.position.y, 0);
            mm[2].transform.position = new Vector3(_auditXMax[0], _player.transform.position.y, 0);
            mm[3].transform.position = new Vector3(_auditXMax[1], _player.transform.position.y, 0);

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
                                if (Mathf.Abs(_gun.transform.position.x - ((_auditXMax[0] + _auditXMax[1]) / 2)) < Mathf.Abs(_gun.transform.position.x - _player.transform.position.x))
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

            if (_rotationMax.z - _rotationMin.z < 0)
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
                        for (float i = _rotationMin.z; i < _rotationMax.z; i += 0.01f)
                        {
                            descriminant = Mathf.Pow(_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad), 2)
                                - 2 * Physics.gravity.y * (_gun.transform.position.y - _player.transform.position.y);

                            if (descriminant >= 0)
                            {
                                time = (-_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad) - Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                                xx = (_gun.transform.position.x + _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad) * time);

                                if (Physics2D.OverlapCircle(new Vector2(xx, _player.transform.position.y), _radius, _layerPlayerDetal, -10, 10))
                                {
                                    speedX = _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad);
                                    speedY = _bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad);

                                    break;
                                }
                            }
                        }

                        break;
                    }
                case 1:
                    {
                        for (float i = _rotationMin.z; i < _rotationMax.z; i += 0.01f)
                        {
                            descriminant = Mathf.Pow(_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad), 2)
                                - 2 * Physics.gravity.y * (_gun.transform.position.y - _player.transform.position.y);

                            if (descriminant >= 0)
                            {
                                time = (-_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad) + Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                                xx = (_gun.transform.position.x + _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad) * time);

                                if (Physics2D.OverlapCircle(new Vector2(xx, _player.transform.position.y), _radius, _layerPlayerDetal, -10, 10))
                                {
                                    speedX = _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad);
                                    speedY = _bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad);

                                    break;
                                }
                            }
                        }

                        break;
                    }
                case 2:
                    {
                        for (float i = _rotationMin.z; i > _rotationMax.z; i -= 0.01f)
                        {
                            descriminant = Mathf.Pow(_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad), 2)
                                - 2 * Physics.gravity.y * (_gun.transform.position.y - _player.transform.position.y);

                            if (descriminant >= 0)
                            {
                                time = (-_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad) - Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                                xx = (_gun.transform.position.x + _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad) * time);

                                if (Physics2D.OverlapCircle(new Vector2(xx, _player.transform.position.y), _radius, _layerPlayerDetal, -10, 10))
                                {
                                    speedX = _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad);
                                    speedY = _bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad);

                                    break;
                                }
                            }
                        }

                        break;
                    }
                case 3:
                    {
                        for (float i = _rotationMin.z; i > _rotationMax.z; i -= 0.01f)
                        {
                            descriminant = Mathf.Pow(_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad), 2)
                                - 2 * Physics.gravity.y * (_gun.transform.position.y - _player.transform.position.y);

                            if (descriminant >= 0)
                            {
                                time = (-_bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad) + Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                                xx = (_gun.transform.position.x + _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad) * time);

                                if (Physics2D.OverlapCircle(new Vector2(xx, _player.transform.position.y), _radius, _layerPlayerDetal, -10, 10))
                                {
                                    speedX = _bulletSpeed * Mathf.Cos(i * Mathf.Deg2Rad);
                                    speedY = _bulletSpeed * Mathf.Sin(i * Mathf.Deg2Rad);

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
            _traectory.ShowTrajectory(_gun.transform.position, _rotationSpeedInMoment, _radius, _prisel, _layersAll);

            StartCoroutine(PrisellingAudit());
        }
        private IEnumerator PrisellingAudit()
        {
            yield return new WaitForSeconds(0.5f);
            if (Physics2D.OverlapCircle(_prisel.transform.position, _radius, _layerPlayerDetal, -10, 10))
                _traectoryPoint[1].ShowTrajectory(_gun.transform.position, _rotationSpeedInMoment, _radius, _layersAll); //shooting
                                                                                                                         //else// ������� �� ����������
                                                                                                                         //_prisel.SetActive(false);                                                                                                             

            StartCoroutine(StartWaiting());
        }
    }
}
