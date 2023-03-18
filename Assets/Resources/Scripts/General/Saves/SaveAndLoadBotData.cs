using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace General.Saves
{
    public class SaveAndLoadBotData : MonoBehaviour
    {
        // скорее всего патч не находит в apk
        private static string path = Application.dataPath + "/Resources/Saves/Bot.json";
        public static void Save(GameObject PlayerBot)
        {
            var BotData = new BotData(PlayerBot);

            File.WriteAllText(
            path,
            JsonConvert.SerializeObject(BotData, Formatting.Indented,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            })
            );
        }

        public static BotData Load() => JsonConvert.DeserializeObject<BotData>(File.ReadAllText(path));
        // нужен отдельный скрипт, чтобы правильно сохранить и выгрузить данные.
    }
}