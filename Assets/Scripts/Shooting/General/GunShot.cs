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
            public abstract float BulletDamage { get; set; }
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
            public abstract uint ProjectilesInAClip { get; set; }
            public abstract uint CurrentProjectilesInAClip { get; protected set; }
            public abstract float ReloadBetweenProjectilesInAClip { get; set; }
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