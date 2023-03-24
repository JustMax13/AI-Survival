using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General.Pathes;

namespace General.Saves
{
    public class BotData
    {
        public List<PartData> BotPartsData { get; private set; }
        public BotData() => BotPartsData = new List<PartData>();
        public BotData(GameObject bot)
        {
            var partsPath = bot.GetComponentsInChildren<PartPath>();
            var parts = new Transform[partsPath.Length];

            for (int i = 0; i < partsPath.Length; i++)
                parts[i] = partsPath[i].PartTransform;

            BotPartsData = new List<PartData>();
            foreach (var item in parts)
                BotPartsData.Add(new PartData(item.gameObject));

            AnchoredJoint2D[] joint2DOnPart;
            for (int i = 0; i < parts.Length; i++)
            {
                joint2DOnPart = parts[i].GetComponents<AnchoredJoint2D>();

                for (int j = 0; j < joint2DOnPart.Length; j++)
                {
                    BotPartsData[i].ConnectedBodys2D[j] = new ConnectedBody2D(parts[i].gameObject, j);
                    for (int k = 0; k < parts.Length; k++)
                    {
                        if (joint2DOnPart[j].connectedBody.gameObject == parts[k].gameObject)
                            BotPartsData[i].ConnectedBodys2D[j].ID = BotPartsData[k].ID;
                    }
                }
            } // P.S. очень страшно. Если бы мы знали что это такое, но мы не знаем что это такое.
        }
    }
}