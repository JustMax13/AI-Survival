using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    using Weapon;
    public class ShotGun : GunShot
    {
        [SerializeField]
        private Transform _bulletSpawn;
        public override Transform BulletSpawn
        {
            get
            {
                return _bulletSpawn;
            }
            protected set
            {
                _bulletSpawn = value;
            }
        }
        [SerializeField]
        private GameObject _bulletPrefab;
        public override GameObject BulletPrefab
        {
            get
            {
                return _bulletPrefab;
            }
            protected set
            {
                _bulletPrefab = value;
            }
        }

        private const float _minBulletVelocity = 10f;
        private const float _maxBulletVelocity = 500f;
        [SerializeField]
        [Range(_minBulletVelocity, _maxBulletVelocity)]
        private float _bulletVelocity = 50f;

        public override float BulletVelocity
        {
            get
            {
                return _bulletVelocity;
            }
            protected set
            {
                if (value < _minBulletVelocity)
                    value = _minBulletVelocity;
                if (value > _maxBulletVelocity)
                    value = _maxBulletVelocity;

                _bulletVelocity = value;
            }
        }
        [SerializeField]
        [Range(0,1000000)]
        private float _bulletDamage;
        public override float BulletDamage 
        {
            get => _bulletDamage;
            set 
            {
                if (value < 0)
                    value = 0;
                _bulletDamage = value;
            }
        }
        [SerializeField]
        [Range(1,10000)]
        private uint _projectilesInAClip;
        public override uint ProjectilesInAClip
        {
            get => _projectilesInAClip;
            set 
            {
                if (value < 0)
                    value = 0;
                else if (value > 10000)
                    value = 10000;

                _projectilesInAClip = value;
            }
        }
        private uint _currentProjectilesInAClip;
        public override uint CurrentProjectilesInAClip
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
        public override float ReloadBetweenProjectilesInAClip 
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

        protected override void Shot()
        {
            GameObject newBullet = Instantiate(BulletPrefab, BulletSpawn.transform.position, BulletSpawn.transform.rotation);

            try
            {
                newBullet.GetComponent<Rigidbody2D>().velocity = BulletVelocity * BulletSpawn.right;
                newBullet.GetComponent<ClassicBullet>().Damage = BulletDamage;
            }
            catch
            {
                Destroy(newBullet);
                Debug.Log("The generated bullet does not have a Rigidbody 2d or ClassicBullet component");
            }
            finally { CurrentProjectilesInAClip -= 1; }
                
            
        }
    }
}