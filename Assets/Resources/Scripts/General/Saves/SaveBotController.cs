using Editor;
using Editor.Moves;
using General.EnumNamespace;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using General.PartOfBots;
using System;

namespace General.Saves
{
    public class SaveBotController : MonoBehaviour
    {
        public static event Action<GameObject> LoadIsEnd;
        public static void SaveBot(GameObject PlayerBot) => SaveAndLoadBotData.Save(PlayerBot);

        public static void LoadBot(GameObject PlayerBot)
        {
            {
                Transform[] child = PlayerBot.GetComponentsInChildren<Transform>();

                for (int i = 1; i < child.Length; i++)
                    Destroy(child[i].gameObject);
            }

            BotData data = SaveAndLoadBotData.Load();

            var partOnScene = new Dictionary<int, GameObject>();
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

                if(part.GetComponent<PluggableObject>().PartType == TypeOfPart.Wheel)
                {
                    for (int i = 0; i < item.ConnectedBodys2D.Length; i++)
                        part.AddComponent<WheelJoint2D>();
                }
                else
                {
                    for (int i = 0; i < item.ConnectedBodys2D.Length; i++)
                        part.AddComponent<FixedJoint2D>();
                }   

                if (SceneManager.GetActiveScene().buildIndex == (int)EnumBuildIndexOfScene.Editor)
                    part.AddComponent<DragAndDropPart>();

                partOnScene.Add(item.ID, part);
            }

            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Editor"))
            {
                for (int i = 0; i < partOnScene.Count; i++)
                {
                    ConnectedBody2D[] conectedBody = data.BotPartsData[i].ConnectedBodys2D;
                    AnchoredJoint2D[] JointsOnPart = partOnScene.ElementAt(i).Value.GetComponents<AnchoredJoint2D>();

                    for (int j = 0; j < JointsOnPart.Length; j++)
                    {
                        JointsOnPart[j].connectedBody = partOnScene[conectedBody[j].ID].GetComponent<Rigidbody2D>();
                        JointsOnPart[j].anchor = new Vector2(conectedBody[j].XAnchor, conectedBody[j].YAnchor);
                        JointsOnPart[j].connectedAnchor = new Vector2(conectedBody[j].XConnectedAnchor, conectedBody[j].YConnectedAnchor);
                    }
                }
            }
            else
            {
                var partCounters = new List<PartCounter>();

                for (int i = 0; i < partOnScene.Count; i++)
                {
                    ConnectedBody2D[] conectedBody = data.BotPartsData[i].ConnectedBodys2D;
                    AnchoredJoint2D[] JointsOnPart = partOnScene.ElementAt(i).Value.GetComponents<AnchoredJoint2D>();
                    PluggableObject pluggableObject = partOnScene.ElementAt(i).Value.GetComponent<PluggableObject>();

                    for (int j = 0; j < JointsOnPart.Length; j++)
                    {
                        JointsOnPart[j].connectedBody = partOnScene[conectedBody[j].ID].GetComponent<Rigidbody2D>();
                        JointsOnPart[j].anchor = new Vector2(conectedBody[j].XAnchor, conectedBody[j].YAnchor);
                        JointsOnPart[j].connectedAnchor = new Vector2(conectedBody[j].XConnectedAnchor, conectedBody[j].YConnectedAnchor);

                        ConnectPoint connectPoint = null;
                        foreach (var item in pluggableObject.ConnectPointsOnPart)
                        {
                            if (JointsOnPart[j].anchor == (Vector2)item.transform.localPosition)
                            {
                                connectPoint = item;
                                break;
                            }
                        }
                        // нам нужна точка как текущего обьекта, так и установившегось
                        //ConnectPoint[] connectPoints = JointsOnPart[j].connectedBody.GetComponent<PluggableObject>().ConnectPointsOnPart;
                        //foreach (var item in connectPoints)
                        //{
                        //    if (JointsOnPart[j].connectedAnchor == (Vector2)item.transform.localPosition)
                        //    {
                        //        // 1) Записать положение
                        //        // думаю, можно сохранять сразу несколько точек: которая на этом обьекте и которая на 
                        //        connectPoint = item;
                        //        break;
                        //    }
                        //}
                        bool partCountersExists = false;
                        PartCounter partCounter = null;
                        foreach (var item in partCounters)
                        {
                            if (item.transform.position == connectPoint.transform.position)
                            {
                                partCountersExists = true;
                                partCounter = item;
                                break;
                            }
                        }

                        if (partCountersExists)
                        {
                            if (!partCounter.IsItemOnList(connectPoint))
                                partCounter.AddPointForLoad(connectPoint);
                        }
                        else
                        {
                            partCounter = PartCounter.CreatePartCounterObject(connectPoint);
                            partCounters.Add(partCounter);
                            partCounter.AddPointForLoad(connectPoint);
                        }
                    }
                }
            }

            LoadIsEnd?.Invoke(PlayerBot);
        }
    }
}