using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace General.Saves
{
    public class SaveAndLoadBotData : MonoBehaviour
    {
        private static string path;

        static SaveAndLoadBotData()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            path = Path.Combine(Application.persistentDataPath, "Bot.json");
#else
            path = Path.Combine(Application.dataPath, "Resources/Saves/Bot.json");
#endif
        }
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