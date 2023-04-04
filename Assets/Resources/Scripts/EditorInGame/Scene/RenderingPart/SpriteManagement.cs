using Editor.Interface;
using General.PartOfBots;
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
        private static Dictionary<TypeOfPart, List<SpriteRenderer>> PartsInSortingLayer = new Dictionary<TypeOfPart, List<SpriteRenderer>>();

        private void Awake()
        {
            SpawnPart.SpawnPartEnd += AddPart;
            ButtonForDestroyObject.BeforeDestroyPart += RemotePart;
            SaveBotController.LoadIsEnd += WhenLoadEnd;
        }

        private static void AddPart(GameObject part)
        {
            var partSpriteRenderer = part.GetComponent<SpriteRenderer>();

            TypeOfPart findKey = TypeOfPart.Default;
            foreach (var key in PartsInSortingLayer.Keys)
                if (partSpriteRenderer.sortingLayerName == key.ToString())
                {
                    findKey = key;
                    break;
                }


            if (findKey != TypeOfPart.Default)
            {
                PartsInSortingLayer.TryGetValue(findKey, out List<SpriteRenderer> spriteRenderers);
                spriteRenderers.Add(partSpriteRenderer);
            }
            else
            {
                TypeOfPart TypeOfPart = TypeOfPart.Default;
                foreach (var key in Enum.GetNames(typeof(TypeOfPart)))
                    if (partSpriteRenderer.sortingLayerName == key)
                    {
                        TypeOfPart = Enum.Parse<TypeOfPart>(key);
                        break;
                    }

                if (TypeOfPart == TypeOfPart.Default)
                    throw new Exception("В TypeOfPart немає вказаного на " + partSpriteRenderer + " сортувального шару");

                PartsInSortingLayer.Add(TypeOfPart, new List<SpriteRenderer>());

                PartsInSortingLayer.TryGetValue(TypeOfPart, out List<SpriteRenderer> spriteRenderers);
                spriteRenderers.Add(partSpriteRenderer);
            }
        }
        private static void AddPart(SpriteRenderer part)
        {
            TypeOfPart findKey = TypeOfPart.Default;
            foreach (var key in PartsInSortingLayer.Keys)
                if (part.sortingLayerName == key.ToString())
                {
                    findKey = key;
                    break;
                }


            if (findKey != TypeOfPart.Default)
            {
                PartsInSortingLayer.TryGetValue(findKey, out List<SpriteRenderer> spriteRenderers);
                spriteRenderers.Add(part);
            }
            else
            {
                TypeOfPart TypeOfPart = TypeOfPart.Default;
                foreach (var key in Enum.GetNames(typeof(TypeOfPart)))
                    if (part.sortingLayerName == key)
                    {
                        TypeOfPart = Enum.Parse<TypeOfPart>(key);
                        break;
                    }

                if (TypeOfPart == TypeOfPart.Default)
                    throw new Exception("В TypeOfPart немає вказаного на " + part + " сортувального шару");

                PartsInSortingLayer.Add(TypeOfPart, new List<SpriteRenderer>());

                PartsInSortingLayer.TryGetValue(TypeOfPart, out List<SpriteRenderer> spriteRenderers);
                spriteRenderers.Add(part);
            }
        }
        private static void RemotePart(GameObject part)
        {
            var partSpriteRenderer = part.GetComponent<SpriteRenderer>();

            TypeOfPart findKey = TypeOfPart.Default;
            foreach (var key in PartsInSortingLayer.Keys)
                if (partSpriteRenderer.sortingLayerName == key.ToString())
                {
                    findKey = key;
                    break;
                }
            if (findKey == TypeOfPart.Default)
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
            TypeOfPart findKey = TypeOfPart.Default;
            foreach (var key in PartsInSortingLayer.Keys)
                if (part.sortingLayerName == key.ToString())
                {
                    findKey = key;
                    break;
                }
            if (findKey == TypeOfPart.Default)
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
            PartsInSortingLayer = new Dictionary<TypeOfPart, List<SpriteRenderer>>();

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
        public static TypeOfPart FindUpperSortingLayerID(List<SpriteRenderer> spriteRenderers)
        {
            TypeOfPart upperSortingLayer = TypeOfPart.Default;

            foreach (var spriteRenderer in spriteRenderers)
            {
                TypeOfPart TypeOfPart = Enum.Parse<TypeOfPart>(spriteRenderer.sortingLayerName);
                if ((int)TypeOfPart > (int)upperSortingLayer)
                    upperSortingLayer = TypeOfPart;
            }

            return upperSortingLayer;
        }
        public static List<SpriteRenderer> FindSpriteRendererWithOneSortingLayer(List<SpriteRenderer> spriteRenderers, TypeOfPart typeOfPart)
        {
            var listSpriteRenderers = new List<SpriteRenderer>();

            foreach (var item in spriteRenderers)
            {
                if (item.sortingLayerName == typeOfPart.ToString())
                    listSpriteRenderers.Add(item);
            }

            return listSpriteRenderers;
        }
        public static SpriteRenderer FindUpperSpriteRenderer(List<SpriteRenderer> spriteRenderers, TypeOfPart typeOfPart)
        {
            if (!PartsInSortingLayer.TryGetValue(typeOfPart, out List<SpriteRenderer> spriteRenderersInDictionary))
                throw new Exception("TypeOfPart не знайдено!"); // тут выдается ошибка, если словарь пустой

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