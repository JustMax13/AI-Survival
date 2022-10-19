using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class LoadingScene : MonoBehaviour
    {
        public static event Action LoadedSomeScene;

        public static void OnLoadedSomeScene() => LoadedSomeScene?.Invoke();
    }
}