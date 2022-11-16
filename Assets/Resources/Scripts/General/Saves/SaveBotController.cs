using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Saves
{
    public class SaveBotController : MonoBehaviour
    {
        public void SaveBot(GameObject PlayerBot) => SaveAndLoadData.Save(PlayerBot); // сделать асинхронным
    }
}