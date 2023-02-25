using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Editor
{
    public class SpriteManagement : MonoBehaviour
    {
        public static Dictionary<EnumPartSortingLayer, List<SpriteRenderer>> PartsInSortingLayer = new Dictionary<EnumPartSortingLayer, List<SpriteRenderer>>();

        static SpriteManagement() => SpawnPart.SpawnPartEnd += AddPartToDictionary;

        private static void AddPartToDictionary(GameObject part)
        {
            var partSpriteRenderer = part.GetComponent<SpriteRenderer>();

            EnumPartSortingLayer findKey = EnumPartSortingLayer.Default;
            foreach (var key in PartsInSortingLayer.Keys)
                if (partSpriteRenderer.sortingLayerName == key.ToString())
                {
                    findKey = key;
                    break;
                }


            if (findKey != EnumPartSortingLayer.Default)
            {
                PartsInSortingLayer.TryGetValue(findKey, out List<SpriteRenderer> spriteRenderers);
                spriteRenderers.Add(partSpriteRenderer);
            }
            else
            {
                EnumPartSortingLayer enumPartSortingLayer = EnumPartSortingLayer.Default;
                foreach (var key in Enum.GetNames(typeof(EnumPartSortingLayer)))
                    if (partSpriteRenderer.sortingLayerName == key)
                    {
                        enumPartSortingLayer = Enum.Parse<EnumPartSortingLayer>(key);
                        break;
                    }

                if (enumPartSortingLayer == EnumPartSortingLayer.Default)
                    throw new Exception("В EnumPartSortingLayer немає вказаного на " + partSpriteRenderer + " сортувального шару");

                PartsInSortingLayer.Add(enumPartSortingLayer, new List<SpriteRenderer>());

                PartsInSortingLayer.TryGetValue(enumPartSortingLayer, out List<SpriteRenderer> spriteRenderers);
                spriteRenderers.Add(partSpriteRenderer);
            }
        }

        public static EnumPartSortingLayer FindUpperSortingLayerID(List<SpriteRenderer> spriteRenderers)
        {
            EnumPartSortingLayer upperSortingLayer = EnumPartSortingLayer.Default;

            foreach (var spriteRenderer in spriteRenderers)
            {
                EnumPartSortingLayer enumPartSortingLayer = Enum.Parse<EnumPartSortingLayer>(spriteRenderer.sortingLayerName);
                if ((int)enumPartSortingLayer > (int)upperSortingLayer)
                    upperSortingLayer = enumPartSortingLayer;
            }
                
            return upperSortingLayer;
        }
        public static List<SpriteRenderer> FindSpriteRendererWithID(List<SpriteRenderer> spriteRenderers, EnumPartSortingLayer enumPartSortingLayer)
        {
            var listSpriteRenderers = new List<SpriteRenderer>();

            foreach (var item in spriteRenderers)
            {
                if (item.sortingLayerName == enumPartSortingLayer.ToString())
                    listSpriteRenderers.Add(item);
            }

            return listSpriteRenderers;
        }
        public static SpriteRenderer FindUpperSpriteRenderer(List<SpriteRenderer> spriteRenderers, EnumPartSortingLayer enumPartSortingLayer)
        {
            if (!PartsInSortingLayer.TryGetValue(enumPartSortingLayer, out List<SpriteRenderer> spriteRenderersInDictionary))
                throw new Exception("enumPartSortingLayer не знайдено!");

            var findedIndex = new int[spriteRenderers.Count];

            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                findedIndex[i] = spriteRenderersInDictionary.IndexOf(spriteRenderers[i]);

                if (findedIndex[i] == -1)
                    throw new Exception($"({spriteRenderers[i]} відсутній у списку spriteRenderersInDictionary");
            }

            return spriteRenderersInDictionary[findedIndex.Max()];
        }
    }
}