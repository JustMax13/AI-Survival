using UnityEngine;
using UnityEngine.SceneManagement;

using General.EnumNamespace;

namespace General.Managers
{
    public class DestroyUnnecessaryScripts : MonoBehaviour
    {
        [SerializeField] private Component[] TurnOffInMenu;
        [SerializeField] private Component[] TurnOffInEditor;
        [SerializeField] private Component[] TurnOffInBattle;

        private void Start()
        {
            int buildIndexActiveScene = SceneManager.GetActiveScene().buildIndex;
            string type;

            switch (buildIndexActiveScene)
            {
                case (int)EnumBuildIndexOfScene.Menu:
                    {
                        foreach (var item in TurnOffInMenu)
                        {
                            type = item.GetType().ToString();
                            Destroy(gameObject.GetComponent(type));
                        }
                            
                        break;
                    }
                case (int)EnumBuildIndexOfScene.Editor:
                    {
                        foreach (var item in TurnOffInEditor)
                        {
                            type = item.GetType().ToString();
                            Destroy(gameObject.GetComponent(type));
                        }

                        break;
                    }
                case (int)EnumBuildIndexOfScene.Battle:
                    {
                        foreach (var item in TurnOffInBattle)
                        {
                            type = item.GetType().ToString();
                            Destroy(gameObject.GetComponent(type));
                        }

                        break;
                    }
            }
        }
    }
}