using UnityEngine;
namespace CombatMechanics.AI
{
    public class DetalScriptAdd : MonoBehaviour
    {
        [SerializeField] private GameObject[] _detaleScript;
        [SerializeField] private string[] _detaleTag;
        private void Awake()
        {
            GameObject obj;
            foreach (Transform child in gameObject.transform)
            {
                for (int i = 0; i < _detaleScript.Length; i++)
                {
                    if (child.gameObject.tag == _detaleTag[i])
                    {
                        obj = Instantiate(_detaleScript[i], child.transform.position, Quaternion.identity);
                        obj.transform.parent = child.transform;
                        break;
                    }
                }

            }
        }
    }
}
