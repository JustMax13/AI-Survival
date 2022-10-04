using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    namespace Weapon
    {
        public abstract class GunShot : MonoBehaviour
        {
            [SerializeField] [Range(1, 10000)]
            private uint _projectilesInAClip;

            [SerializeField] [Range(0, 1000000)]
            private float _bulletDamage;
            [SerializeField] [Range(_minBulletVelocity, _maxBulletVelocity)]
            private float _bulletVelocity = 50f;

            [SerializeField] [Range(0.001f, 300)]
            private float _reloadBetweenProjectilesInAClip;
            [SerializeField] [Range(0.1f, 300)]
            private float _reloadTime;

            [SerializeField] private Transform _bulletSpawn;

            [SerializeField] private GameObject _bulletPrefab;

            private const float _minBulletVelocity = 10f;
            private const float _maxBulletVelocity = 150f;

            private uint _currentProjectilesInAClip;

            bool ReloadEnd;

            private float _currentReloadTime;

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
            public float BulletVelocity
            {
                get => _bulletVelocity;
                set
                {
                    if (value < _minBulletVelocity)
                        value = _minBulletVelocity;
                    if (value > _maxBulletVelocity)
                        value = _maxBulletVelocity;

                    _bulletVelocity = value;
                }
            }

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
            public float ReloadTime
            {
                get => _reloadTime;
                set
                {
                    if (value < 0.1f)
                        _reloadTime = 0.1f;
                }
            }

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

            public Transform BulletSpawn
            {
                get => _bulletSpawn;
                private set { _bulletSpawn = value; }
            }

            public GameObject BulletPrefab
            {
                get => _bulletPrefab;
                private set { _bulletPrefab = value; }
            }

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