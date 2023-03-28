using Editor.Interface;
using General.Pathes;
using General.Saves;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Editor
{
    public class SpriteManagement : MonoBehaviour
    {
        private static Dictionary<EnumPartSortingLayer, List<SpriteRenderer>> PartsInSortingLayer = new Dictionary<EnumPartSortingLayer, List<SpriteRenderer>>();

        private void Awake()
        {
            SpawnPart.SpawnPartEnd += AddPart;
            ButtonForDestroyObject.BeforeRemovingPart += RemotePart;
            SaveBotController.LoadIsEnd += WhenLoadEnd;
        }

        private static void AddPart(GameObject part)
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
        private static void AddPart(SpriteRenderer part)
        {
            EnumPartSortingLayer findKey = EnumPartSortingLayer.Default;
            foreach (var key in PartsInSortingLayer.Keys)
                if (part.sortingLayerName == key.ToString())
                {
                    findKey = key;
                    break;
                }


            if (findKey != EnumPartSortingLayer.Default)
            {
                PartsInSortingLayer.TryGetValue(findKey, out List<SpriteRenderer> spriteRenderers);
                spriteRenderers.Add(part);
            }
            else
            {
                EnumPartSortingLayer enumPartSortingLayer = EnumPartSortingLayer.Default;
                foreach (var key in Enum.GetNames(typeof(EnumPartSortingLayer)))
                    if (part.sortingLayerName == key)
                    {
                        enumPartSortingLayer = Enum.Parse<EnumPartSortingLayer>(key);
                        break;
                    }

                if (enumPartSortingLayer == EnumPartSortingLayer.Default)
                    throw new Exception("В EnumPartSortingLayer немає вказаного на " + part + " сортувального шару");

                PartsInSortingLayer.Add(enumPartSortingLayer, new List<SpriteRenderer>());

                PartsInSortingLayer.TryGetValue(enumPartSortingLayer, out List<SpriteRenderer> spriteRenderers);
                spriteRenderers.Add(part);
            }
        }
        private static void RemotePart(GameObject part)
        {
            var partSpriteRenderer = part.GetComponent<SpriteRenderer>();

            EnumPartSortingLayer findKey = EnumPartSortingLayer.Default;
            foreach (var key in PartsInSortingLayer.Keys)
                if (partSpriteRenderer.sortingLayerName == key.ToString())
                {
                    findKey = key;
                    break;
                }
            if (findKey == EnumPartSortingLayer.Default)
                throw new Exception($"Немає відповідного списку до деталі {part}");

            PartsInSortingLayer.TryGetValue(findKey, out List<SpriteRenderer> partsByKey);

            foreach (var item in partsByKey)
                if (item == partSpriteRenderer)
                {
                    partsByKey.Remove(item);
                    return;
                }

            throw new Exception($"У списку деталей не було знайдено деталі {part}");
        }
        private static void RemotePart(SpriteRenderer part)
        {
            EnumPartSortingLayer findKey = EnumPartSortingLayer.Default;
            foreach (var key in PartsInSortingLayer.Keys)
                if (part.sortingLayerName == key.ToString())
                {
                    findKey = key;
                    break;
                }
            if (findKey == EnumPartSortingLayer.Default)
                throw new Exception($"Немає відповідного списку до деталі {part}");

            PartsInSortingLayer.TryGetValue(findKey, out List<SpriteRenderer> partsByKey);

            foreach (var item in partsByKey)
                if (item == part)
                {
                    partsByKey.Remove(item);
                    return;
                }

            throw new Exception($"У списку деталей не було знайдено деталі {part}");
        }
        private static void RewriteSpritesDictionary(List<SpriteRenderer> botParts)
        {
            PartsInSortingLayer = new Dictionary<EnumPartSortingLayer, List<SpriteRenderer>>();

            foreach (var part in botParts)
                AddPart(part);
        }
        private static void WhenLoadEnd(GameObject bot)
        {
            PartPath[] partsPaths = bot.GetComponentsInChildren<PartPath>();

            var spriteRendererOnParts = new List<SpriteRenderer>();
            foreach (var item in partsPaths)
                spriteRendererOnParts.Add(item.GetComponent<SpriteRenderer>());

            RewriteSpritesDictionary(spriteRendererOnParts);
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
        public static List<SpriteRenderer> FindSpriteRendererWithOneSortingLayer(List<SpriteRenderer> spriteRenderers, EnumPartSortingLayer enumPartSortingLayer)
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
                throw new Exception("enumPartSortingLayer не знайдено!"); // тут выдается ошибка, если словарь пустой

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