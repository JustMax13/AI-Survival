using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Saves
{
    public class SaveBotController : MonoBehaviour
    {
        // сделать асинхронным и добавить чтобы по центу все сохранялось
        public void SaveBot(GameObject PlayerBot) => SaveAndLoadBotData.Save(PlayerBot); 

        public void LoadBot(GameObject PlayerBot)
        {
            // прописать делегат, для очистки сцены от деталей, которые на ней находятся
            BotData data = SaveAndLoadBotData.Load();

            var PartOnScene = new Dictionary<GameObject, int>();
            GameObject part;

            foreach (var item in data.BotPartsData)
            {
                part = Resources.Load<GameObject>(item.PathToPrefab);

                part.transform.position = new Vector3(item.PartPosition.X,
                    item.PartPosition.Y, item.PartPosition.Z);
                part.transform.rotation = new Quaternion(item.PartRotation.X,
                    item.PartRotation.Y, item.PartRotation.Z, item.PartRotation.W);

                part = Instantiate(part, part.transform.position, 
                    part.transform.rotation, PlayerBot.transform);

                for (int i = 0; i < item.ConnectedBodys2D.Length; i++)
                    part.AddComponent<FixedJoint2D>();

                {
                    var Bolts = part.GetComponentsInChildren<Transform>();
                    BoltData BoltData;
                    Vector3 BoltPosition = new Vector3();
                    for (int i = 0; i < item.boltsData.Length; i++)
                    {
                        for (int j = 0; j < item.boltsData.Length; j++)
                        {
                            BoltData = item.boltsData[j];

                            BoltPosition.x = BoltData.Position.X;
                            BoltPosition.y = BoltData.Position.Y;
                            BoltPosition.z = BoltData.Position.Z;

                            if (Bolts[i + 1].transform.position == BoltPosition)
                                Bolts[i + 1].GetComponent<SpriteRenderer>().enabled = BoltData.SpriteRendererEnabled;
                        }
                    }
                }

                PartOnScene.Add(part, item.ID);
                // тут осталось сделать их физическими, если это в редакторе + нужно пофиксить там баг,
                // который выплывает
            }

            // теперь все детали записаны в PartOnScene, осталось их подключить между собой
        }
    }
}