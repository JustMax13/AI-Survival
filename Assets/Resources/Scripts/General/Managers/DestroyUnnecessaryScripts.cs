using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.SearchService;
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

            switch (buildIndexActiveScene)
            {
                case (int)EnumBuildIndexOfScene.Menu:
                    {
                        foreach (var item in TurnOffInMenu)
                            Destroy(item);
                        break;
                    }
                case (int)EnumBuildIndexOfScene.Editor:
                    {
                        foreach (var item in TurnOffInEditor)
                            Destroy(item);
                        break;
                    }
                case (int)EnumBuildIndexOfScene.Battle:
                    {
                        foreach (var item in TurnOffInBattle)
                            Destroy(item);
                        break;
                    }
            }
        }
    }
}