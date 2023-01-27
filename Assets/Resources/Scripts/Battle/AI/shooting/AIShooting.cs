using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatMechanics.AI
{
    public class AIShooting : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationMin, _rotationMax;
        [SerializeField]private Vector3 _rotationSpeedMin, _rotationSpeedMax, _rotationSpeedInMoment;

        [SerializeField] private float _bulletSpeed = 6, _radius = 0.25f;

        [SerializeField] private GunTraectory _traectory;
        [SerializeField] private PaintTraectory[] _traectoryPoint;

        [SerializeField] private GameObject _gun,_prisel, _player,ckesh;
        [SerializeField] private LayerMask /*_playerLayer,*/ _layerPlayerDetal, _layersAll;

        [SerializeField] private List<float> _auditStepsMin, _auditStepsMax,_auditXMin , _auditXMax;
        private int _auditStepsMinCount, _auditStepsMaxCount;

        private bool _can;
        void Start()
        {
            // _rotationMin = new Vector3(0, 0, 45);
            _rotationSpeedMin = new Vector3(_bulletSpeed * Mathf.Cos(_rotationMin.z), _bulletSpeed * Mathf.Sin(_rotationMin.z), 0);
            _rotationSpeedMax = new Vector3(_bulletSpeed * Mathf.Cos(_rotationMax.z), _bulletSpeed * Mathf.Sin(_rotationMax.z), 0);
            Instantiate(ckesh,new Vector3(0,0,0), Quaternion.identity);
            // _player = Physics2D.OverlapBox(new Vector2(0, 0), new Vector2(20, 20), 0, _playerLayer, -7, 7).gameObject;
            //_auditStepsMax.Add(0f);
            //_auditStepsMax.Add(0f);
            //_auditStepsMin.Add(0f);
            //_auditStepsMin.Add(0f);
            //_auditXMin.Add(0f);
            //_auditXMin.Add(0f);
            //_auditXMax.Add(0f);
            //_auditXMax.Add(0f);

        }
        public void FoundPlayer(GameObject player)
        {
            _player = player.gameObject;
            StartCoroutine(startWaiting());
        }
        private IEnumerator startWaiting()
        {
            yield return new WaitForSeconds(1f);
            _rotationSpeedMin = new Vector3(_bulletSpeed * Mathf.Cos(_rotationMin.z), _bulletSpeed * Mathf.Sin(_rotationMin.z), 0);
           _traectoryPoint[0].ShowTrajectory(_gun.transform.position, _rotationSpeedMin, _radius, _layersAll);
            
           _traectoryPoint[2].ShowTrajectory(_gun.transform.position, _rotationSpeedMax, _radius, _layersAll);
            firstAuditCalcut();
            //secondAuditRotatoionCalcut(0);
        }


        private void firstAuditCalcut()
        {
            float descriminant;


            descriminant = _rotationSpeedMax.y * _rotationSpeedMax.y - 2 * Physics.gravity.y*(_gun.transform.position.y - _player.transform.position.y);
            if (descriminant > 0)
            {
                _auditStepsMax[0] = (_rotationSpeedMax.y + Mathf.Sqrt(descriminant))/Physics.gravity.y ;
                if (descriminant > 0)
                {
                    _auditStepsMax[1] = (_rotationSpeedMax.y - Mathf.Sqrt(descriminant)) / Physics.gravity.y;

                    if (_auditStepsMax[1] > 0) _auditStepsMaxCount = 2;
                    else _auditStepsMaxCount = 1;
                }
                else
                {
                    _auditStepsMaxCount = 1;

                }



                descriminant = _rotationSpeedMin.y * _rotationSpeedMin.y - 2 * Physics.gravity.y * (_gun.transform.position.y- _player.transform.position.y);
                if (descriminant > 0)
                {
                    _auditStepsMin[0] = (-_rotationSpeedMin.y + Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                    if (descriminant > 0)
                    {
                        _auditStepsMin[1] = (_rotationSpeedMin.y - Mathf.Sqrt(descriminant)) / Physics.gravity.y;

                        if (_auditStepsMin[1] > 0) _auditStepsMinCount = 2;
                        else _auditStepsMinCount = 1;
                    }
                    else
                    {
                        _auditStepsMinCount = 1;

                    }

                }
                else _auditStepsMinCount = 0;
              
            }
            else
            {
                _auditStepsMaxCount = 0;
            }
            firstAuditXPositionCalcut();
        }
        private void firstAuditXPositionCalcut()
        {
            _auditXMin[0] = -_gun.transform.position.x+ _rotationSpeedMin.x* _auditStepsMin[0];
            _auditXMin[1] = -_gun.transform.position.x+ _rotationSpeedMin.x * _auditStepsMin[1];
            _auditXMax[0] = -_gun.transform.position.x+ _rotationSpeedMax.x * _auditStepsMax[0];
            _auditXMax[1] = -_gun.transform.position.x + _rotationSpeedMax.x * _auditStepsMax[1];
            firstAudit();
        }
        private void firstAudit()
        {
          //  float znak;
            if (_auditStepsMaxCount > 0)
            {
                if (_auditStepsMaxCount == 1)
                {
                    ///znak = (_auditXMax[0] - _auditXMin[0]) / Mathf.Abs(_auditXMax[0] - _auditXMin[0]);
                    if (_player.transform.position.x != _auditXMin[0] & _player.transform.position.x != _auditXMax[0])
                    {
                        if ((_player.transform.position.x > _auditXMin[0] & _player.transform.position.x < _auditXMax[0]) || (_player.transform.position.x < _auditXMin[0] & _player.transform.position.x > _auditXMax[0]))
                        {
                            secondAuditRotatoionCalcut(0);
                        }
                        else
                        {
                            _prisel.SetActive(false);
                            StartCoroutine(startWaiting());
                        }// якщо більше рух вперед менше рух назад 
                    }
                    else
                    {
                        if (_player.transform.position.x == _auditXMin[0])
                        {
                            secondAuditMaxMinRotation(false);
                        }
                        else
                        {
                            secondAuditMaxMinRotation(true);
                        }
                    }
                }
                else
                {
                    if (_auditStepsMinCount == 0)
                    {
                        if (_player.transform.position.x != _auditXMax[0] & _player.transform.position.x != _auditXMax[1])
                        {
                            if ((_player.transform.position.x > _auditXMax[0] & _player.transform.position.x < _auditXMax[1]) || (_player.transform.position.x < _auditXMax[0] & _player.transform.position.x > _auditXMax[1]))
                            {
                                if(Mathf.Abs(_gun.transform.position.x-((_auditXMax[0]+ _auditXMax[1])/2))< Mathf.Abs(_gun.transform.position.x - _player.transform.position.x))
                                {
                                     secondAuditRotatoionCalcut(0);
                                }
                                else
                                {
                                    secondAuditRotatoionCalcut(1);
                                }
                               
                            }
                            else
                            {
                                _prisel.SetActive(false);
                                StartCoroutine(startWaiting());
                            }// якщо більше рух вперед менше рух назад 
                        }
                        else
                        {
                            secondAuditMaxMinRotation(true);
                        }
                    }
                    else
                    {
                        if (_player.transform.position.x != _auditXMin[0] & _player.transform.position.x != _auditXMax[0])
                        {
                            if ((_player.transform.position.x > _auditXMin[0] & _player.transform.position.x < _auditXMax[0]) || (_player.transform.position.x < _auditXMin[0] & _player.transform.position.x > _auditXMax[0]))
                            {
                                secondAuditRotatoionCalcut(0);
                            }
                            else
                            {
                                if (_player.transform.position.x != _auditXMin[1] & _player.transform.position.x != _auditXMax[1])
                                {
                                    if ((_player.transform.position.x > _auditXMin[1] & _player.transform.position.x < _auditXMax[1]) || (_player.transform.position.x < _auditXMin[1] & _player.transform.position.x > _auditXMax[1]))
                                    {
                                        secondAuditRotatoionCalcut(1);
                                    }
                                    else {
                                        _prisel.SetActive(false);
                                        StartCoroutine(startWaiting());
                                    }// якщо більше рух вперед менше рух назад, рахуємо за першими точками(нульовими)
                                }
                                else
                                {
                                    if (_player.transform.position.x == _auditXMin[1])
                                    {
                                        secondAuditMaxMinRotation(false);
                                    }
                                    else
                                    {
                                        secondAuditMaxMinRotation(true);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (_player.transform.position.x == _auditXMin[0])
                            {
                                secondAuditMaxMinRotation(false);
                            }
                            else
                            {
                                secondAuditMaxMinRotation(true);
                            }
                        }
                    }
                }
            }else StartCoroutine(startWaiting());
            
        }
        private void secondAuditMaxMinRotation(bool max)
        {
            if (max)_rotationSpeedInMoment = _rotationSpeedMax;
            else _rotationSpeedInMoment = _rotationSpeedMax;
           Priselling();
        }
        private void secondAuditRotatoionCalcut(int step)
        {
            float speedX = _rotationSpeedMin.x, speedY = _rotationSpeedMin.y, time, descriminant,xx;
           
            if(_rotationMax.z<0)
            {
                if (step == 0) step = 2;
                else step = 3;
            }
            if (step == 0)
            {
                for (float i =  _rotationMin.z;  i < _rotationMax.z; i +=  0.01f)
                {
                    descriminant = Mathf.Pow(_bulletSpeed * Mathf.Sin(i), 2) - 2 * Physics.gravity.y * (_gun.transform.position.y - _player.transform.position.y);
                    if (descriminant >= 0)
                    {
                        time = (-_bulletSpeed * Mathf.Sin(i) + Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                        xx = (_gun.transform.position.x + _bulletSpeed * Mathf.Cos(i) * time);
                        if (Physics2D.OverlapCircle(new Vector2(xx, _player.transform.position.y), _radius * 3, _layerPlayerDetal, -10, 10))
                        {
                            speedX = _bulletSpeed * Mathf.Cos(i);
                            speedY = _bulletSpeed * Mathf.Sin(i);
                            
                            break;
                        }
                    }
                
                }

            }
            if (step == 1)
            {
                for (float i = _rotationMin.z;  i <  _rotationMax.z; i += 0.01f)
                {
                    descriminant = Mathf.Pow(_bulletSpeed * Mathf.Sin(i), 2) - 2 * Physics.gravity.y*(_gun.transform.position.y - _player.transform.position.y);
                    if (descriminant >= 0)
                    { 
                    time = (-_bulletSpeed * Mathf.Sin(i) - Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                    xx = (_gun.transform.position.x + _bulletSpeed * Mathf.Cos(i) * time);
                    if (Physics2D.OverlapCircle(new Vector2(xx, _player.transform.position.y), _radius * 3, _layerPlayerDetal, -10, 10)) 
                    {
                            speedX = _bulletSpeed * Mathf.Cos(i);
                        speedY = _bulletSpeed * Mathf.Sin(i);
                           
                        break;
                    }
                       
                    }
                  
                }

            }
            if (step == 2)
            {
                for (float i = _rotationMin.z; i > _rotationMax.z; i -= 0.01f)
                {
                    descriminant = Mathf.Pow(_bulletSpeed * Mathf.Sin(i), 2) - 2 * Physics.gravity.y * (_gun.transform.position.y - _player.transform.position.y);
                    if (descriminant >= 0)
                    {
                        time = (-_bulletSpeed * Mathf.Sin(i) + Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                        xx = (_gun.transform.position.x + _bulletSpeed * Mathf.Cos(i) * time);
                        if (Physics2D.OverlapCircle(new Vector2(xx, _player.transform.position.y), _radius * 3, _layerPlayerDetal, -10, 10))
                        {
                            speedX = _bulletSpeed * Mathf.Cos(i);
                            speedY = _bulletSpeed * Mathf.Sin(i);

                            break;
                        }
                    }

                }

            }
            if (step == 3)
            {
                for (float i = _rotationMin.z; i > _rotationMax.z; i -= 0.01f)
                {
                    descriminant = Mathf.Pow(_bulletSpeed * Mathf.Sin(i), 2) - 2 * Physics.gravity.y * (_gun.transform.position.y - _player.transform.position.y);
                    if (descriminant >= 0)
                    {
                        time = (-_bulletSpeed * Mathf.Sin(i) - Mathf.Sqrt(descriminant)) / Physics.gravity.y;
                        xx = (_gun.transform.position.x + _bulletSpeed * Mathf.Cos(i) * time);
                        if (Physics2D.OverlapCircle(new Vector2(xx, _player.transform.position.y), _radius, _layerPlayerDetal, -10, 10))
                        {
                            speedX = _bulletSpeed * Mathf.Cos(i);
                            speedY = _bulletSpeed * Mathf.Sin(i);

                            break;
                        }

                    }

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
            {
               _traectoryPoint[1].ShowTrajectory(_gun.transform.position, _rotationSpeedInMoment, _radius, _layersAll); //shooting
            }//else// гравець за перещкодою
            //_prisel.SetActive(false);
          
            StartCoroutine(startWaiting());
        }


    }
}
