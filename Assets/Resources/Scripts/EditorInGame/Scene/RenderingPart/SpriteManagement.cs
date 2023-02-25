using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Editor
{
    public class SpriteManagement : MonoBehaviour
    {
        public static Dictionary<int, List<SpriteRenderer>> PartsInSortingLayer = new Dictionary<int, List<SpriteRenderer>>();

        static SpriteManagement() => SpawnPart.SpawnPartEnd += AddPartToDictionary;

        private static void AddPartToDictionary(GameObject part)
        {
            var partSpriteRenderer = part.GetComponent<SpriteRenderer>();

            bool keyInDictionary = false;
            foreach (var key in PartsInSortingLayer.Keys)
                if (partSpriteRenderer.sortingLayerID == key)
                    keyInDictionary = true;

            if (keyInDictionary)
            {
                PartsInSortingLayer.TryGetValue(partSpriteRenderer.sortingLayerID,out List<SpriteRenderer> spriteRenderers);
                spriteRenderers.Add(partSpriteRenderer);
            }
            else
            {
                PartsInSortingLayer.Add(partSpriteRenderer.sortingLayerID, new List<SpriteRenderer>());

                PartsInSortingLayer.TryGetValue(partSpriteRenderer.sortingLayerID, out List<SpriteRenderer> spriteRenderers);
                spriteRenderers.Add(partSpriteRenderer);
            }
        }

        public static int FindUpperSortingLayerID(List<SpriteRenderer> spriteRenderers)
        {
            int maxID = 0;

            foreach (var spriteRenderer in spriteRenderers)
                if (spriteRenderer.sortingLayerID > maxID)
                    maxID = spriteRenderer.sortingLayerID;

            return maxID;
        }
        public static List<SpriteRenderer> FindSpriteRendererWithID(List<SpriteRenderer> spriteRenderers, int ID)
        {
            var listSpriteRenderers = new List<SpriteRenderer>();

            foreach (var item in spriteRenderers)
            {
                if (item.sortingLayerID == ID)
                    listSpriteRenderers.Add(item);
            }

            return listSpriteRenderers;
        }
        public static SpriteRenderer FindUpperSpriteRenderer(SpriteRenderer[] spriteRenderers, int sortingLayerID)
        {
            if (!PartsInSortingLayer.TryGetValue(sortingLayerID, out List<SpriteRenderer> spriteRenderersInDictionary))
                throw new Exception("sortingLayerID не знайдено!");

            var findedIndex = new int[spriteRenderers.Length];

            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                findedIndex[i] = spriteRenderersInDictionary.IndexOf(spriteRenderers[i]);

                if (findedIndex[i] == -1)
                    throw new Exception($"({spriteRenderers[i]} відсутній у списку spriteRenderersInDictionary");
            }

            return spriteRenderersInDictionary[findedIndex.Max()];
        }
        public static SpriteRenderer FindUpperSpriteRenderer(List<SpriteRenderer> spriteRenderers, int sortingLayerID)
        {
            if (!PartsInSortingLayer.TryGetValue(sortingLayerID, out List<SpriteRenderer> spriteRenderersInDictionary))
                throw new Exception("sortingLayerID не знайдено!");

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