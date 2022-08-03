using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    namespace Weapon
    {
        public abstract class GunShot : MonoBehaviour
        {
            public abstract Transform BulletSpawn { get; protected set; }
            public abstract GameObject BulletPrefab { get; protected set; }
            public abstract float BulletVelocity { get; protected set; }
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
                    _currentReloadTime = ReloadTime;
                }

            }
            abstract protected void Shot();

            private void Start()
            {
                ReloadEnd = true;
                _currentReloadTime = 0;
            }
            private void FixedUpdate()
            {
                _currentReloadTime -= Time.deltaTime;

                ReloadEnd = _currentReloadTime <= 0 ? true : false;
            }
        }
    }
}