using General.Saves;
using System;
using System.Collections;
using UnityEngine;

namespace Menu
{
    public class SpawnPlayerInMenu : MonoBehaviour
    {
        [SerializeField] private float _playerBotScale;
        public static event Action PlayerSpawnEnd;

        private void Start()
        {
            SaveBotController.LoadBot(gameObject);
            StartCoroutine(SpawnSetting());
        }
        private IEnumerator SpawnSetting()
        {
            yield return new WaitForEndOfFrame();

            foreach (var item in gameObject.GetComponentsInChildren<Transform>())
                item.gameObject.layer = LayerMask.NameToLayer("Player");
            gameObject.transform.localScale = new Vector3(_playerBotScale, _playerBotScale, _playerBotScale);

            PlayerSpawnEnd?.Invoke();
        }
    }
}