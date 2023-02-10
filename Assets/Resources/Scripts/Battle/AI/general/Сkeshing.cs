using UnityEngine;
namespace CombatMechanics.AI
{
    public class Сkeshing : MonoBehaviour
    {
        private bool _playerCresh, _enemyCresh;

        [SerializeField] private string _tagPlayer, _tagEnemy, _playerLayer, _enemyLayer;
        [SerializeField] private GameObject _player;
        [SerializeField] private AiBrain _enemy;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == _tagPlayer && collision.gameObject.layer == LayerMask.NameToLayer(_playerLayer))
            {
                _player = collision.gameObject;
                _playerCresh = true;
            }
            if (collision.gameObject.tag == _tagEnemy && collision.gameObject.layer == LayerMask.NameToLayer(_enemyLayer))
            {
                _enemy = collision.gameObject.transform.parent.GetComponent<AiBrain>();
                _enemyCresh = true;
            }
            if (_enemyCresh == true && _playerCresh == true)
            {
                _enemy.FoundPlayer(_player);
                Destroy(gameObject);
            }
        }
    }
}
