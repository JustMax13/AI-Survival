using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace General.Saves
{
    public class SaveBotController : MonoBehaviour
    {
        // сделать асинхронным и добавить чтобы по центу все сохранялось
        public static void SaveBot(GameObject PlayerBot) => SaveAndLoadBotData.Save(PlayerBot);

        public static void LoadBot(GameObject PlayerBot)
        {
            {
                Transform[] child = PlayerBot.GetComponentsInChildren<Transform>();

                for (int i = 1; i < child.Length; i++)
                    Destroy(child[i].gameObject);
            }

            BotData data = SaveAndLoadBotData.Load();

            var PartOnScene = new Dictionary<int, GameObject>();
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

                PartOnScene.Add(item.ID, part);
            }


            for (int i = 0; i < PartOnScene.Count; i++)
            {
                ConnectedBody2D[] conectedBody = data.BotPartsData[i].ConnectedBodys2D;
                FixedJoint2D[] JointsOnPart = PartOnScene.ElementAt(i).Value.GetComponents<FixedJoint2D>();
                // скорее всего размерности 2х масивов не всегда совпадают... но почему?
                for (int j = 0; j < conectedBody.Length; j++)
                {
                    JointsOnPart[j].connectedBody = PartOnScene[conectedBody[j].ID]
                        .GetComponent<Rigidbody2D>();
                    JointsOnPart[j].anchor = new Vector2(conectedBody[i].XAnchor, conectedBody[i].YAnchor);
                }
            }
            // На этих частях нет DragAndDrop
            // а еще с включенным AttractionPoint и AttractionObject, в старте там написано
            // что обьект не подключен, вот оно и создает без болтика, хотя физически они 
            // между собой подключены

            // когда много деталей, то не все правильно сохранило /
            // выгрузило при присоединении детали
        }
    }
}