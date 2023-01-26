using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatMechanics.AI
{
    public class ckeshing : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private string _tagPlayer, _tagEnemy,_playerLayer,_enemyLayer;
        [SerializeField] private AIShooting _enemy;
        private bool _playerCresh, _enemyCresh;
        private void Start()
        {
            
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
           if (collision.gameObject.tag == _tagPlayer & collision.gameObject.layer == LayerMask.NameToLayer(_playerLayer))
           {
               
               
                 _player = collision.gameObject;
                _playerCresh = true;
                
               
            }
            if (collision.gameObject.tag == _tagEnemy & collision.gameObject.layer == LayerMask.NameToLayer(_enemyLayer))
            {
                _enemy = collision.gameObject.GetComponent<AIShooting>();
                _enemyCresh = true;
            }
            if (_enemyCresh == true & _playerCresh == true)
            {
                _enemy.FoundPlayer(_player);
                Destroy(gameObject);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
         
        }
    }  
}
