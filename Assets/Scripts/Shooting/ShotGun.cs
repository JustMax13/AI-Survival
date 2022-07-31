using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Weapon;

public class ShotGun : MonoBehaviour, IGunShot
{
    [SerializeField]
    private Transform _bulletSpawn;
    public Transform BulletSpawn 
    {
        get 
        {
            return _bulletSpawn;
        }
        set
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
        set
        {
            _bulletPrefab = value;
        }
    }
    
    private const float _minBulletVelocity = 10f;
    private const float _maxBulletVelocity = 500f;
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

    public void Shot()
    {
        GameObject newBullet = Instantiate(BulletPrefab, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = BulletVelocity * BulletSpawn.right;
    }
}
