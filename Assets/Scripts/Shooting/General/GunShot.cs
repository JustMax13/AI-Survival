using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    namespace Weapon
    {
        public abstract class GunShot : MonoBehaviour
        {
            [SerializeField]
            private Transform _bulletSpawn;
            public Transform BulletSpawn
            {
                get
                {
                    return _bulletSpawn;
                }
                private set
                {
                    _bulletSpawn = value;
                }
            }
            [SerializeField]
            private GameObject _bulletPrefab;
            public GameObject BulletPrefab
            {
                get
                {
                    return _bulletPrefab;
                }
                private set
                {
                    _bulletPrefab = value;
                }
            }

            private const float _minBulletVelocity = 10f;
            private const float _maxBulletVelocity = 150f;
            [SerializeField]
            [Range(_minBulletVelocity, _maxBulletVelocity)]
            private float _bulletVelocity = 50f;

            public float BulletVelocity
            {
                get
                {
                    return _bulletVelocity;
                }
                set
                {
                    if (value < _minBulletVelocity)
                        value = _minBulletVelocity;
                    if (value > _maxBulletVelocity)
                        value = _maxBulletVelocity;

                    _bulletVelocity = value;
                }
            }
            [SerializeField]
            [Range(0, 1000000)]
            private float _bulletDamage;
            public float BulletDamage
            {
                get => _bulletDamage;
                set
                {
                    if (value < 0)
                        value = 0;
                    else if (value > 1000000)
                        value = 1000000;

                    _bulletDamage = value;
                }
            }
            [SerializeField]
            [Range(1, 10000)]
            private uint _projectilesInAClip;
            public uint ProjectilesInAClip
            {
                get => _projectilesInAClip;
                private set
                {
                    if (value < 1)
                        value = 1;
                    else if (value > 10000)
                        value = 10000;

                    _projectilesInAClip = value;
                }
            }
            private uint _currentProjectilesInAClip;
            public uint CurrentProjectilesInAClip
            {
                get => _currentProjectilesInAClip;
                protected set
                {
                    if (value < 0)
                        value = 0;
                    else if (value > 10000)
                        value = 10000;

                    _currentProjectilesInAClip = value;
                }
            }

            [SerializeField]
            [Range(0.001f, 300)]
            private float _reloadBetweenProjectilesInAClip;
            public float ReloadBetweenProjectilesInAClip
            {
                get => _reloadBetweenProjectilesInAClip;
                set
                {
                    if (value > 0.001f)
                        value = 0.001f;
                    else if (value > 300)
                        value = 300;

                    _reloadBetweenProjectilesInAClip = 300;
                }
            }
            [SerializeField]
            [Range(0.1f, 300)]
            private float _reloadTime;
            public float ReloadTime
            {
                get => _reloadTime;
                set
                {
                    if (value < 0.1f)
                        _reloadTime = 0.1f;
                }
            }
            private float _currentReloadTime;
            bool ReloadEnd;
            public void CheckReloadAndShot()
            {
                if (ReloadEnd)
                {
                    Shot();
                    if (CurrentProjectilesInAClip <= 0)
                    {
                        _currentReloadTime = ReloadTime;
                        CurrentProjectilesInAClip = ProjectilesInAClip;
                    }
                    else _currentReloadTime = ReloadBetweenProjectilesInAClip;
                }

            }
            abstract protected void Shot();

            private void Start()
            {
                ReloadEnd = true;
                _currentReloadTime = 0;
                CurrentProjectilesInAClip = ProjectilesInAClip;
            }
            private void FixedUpdate()
            {
                ReloadEnd = _currentReloadTime <= 0 ? true : false;
                if (!ReloadEnd)
                    _currentReloadTime -= Time.deltaTime;
            }
        }
    }
}