using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Weapon;

public class Howitzer : MonoBehaviour, IGunShot
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
    [SerializeField]
    private float _bulletVelocity = 50f;
    private const float _minBulletVelocity = 3f;
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
            _bulletVelocity = value;
        }
    }

    public void Shot()
    {
        GameObject newBullet = Instantiate(BulletPrefab, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = BulletVelocity * Vector2.right;
    }
}
