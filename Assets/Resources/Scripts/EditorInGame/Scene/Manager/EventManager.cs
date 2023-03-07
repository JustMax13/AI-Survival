using Editor.Moves;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class EventManager : MonoBehaviour
    {
        private static bool zeroTouchesInLastFrame;
        private static Dictionary<ActionType, bool> _allActions;
        public static Dictionary<ActionType, bool> AllActions { get => _allActions; }

        public static event Action<ActionType> EventStarted;
        public static event Action EventEnded;

        private void Awake()
        {
            zeroTouchesInLastFrame = true;

            _allActions = new Dictionary<ActionType, bool>
            {
                { ActionType.CameraMoveAndZoom, false },
                { ActionType.PartDrag, false },
                { ActionType.PressInteractionInterface, false }
            };

            EventEnded = null;

            EventStarted += delegate { zeroTouchesInLastFrame = false; };
            EventStarted += TurnOnAction;

            EventEnded += OffCurrentAction;
        }
        private void Update()
        {
            _allActions.TryGetValue(ActionType.CameraMoveAndZoom, out bool value);
            Debug.Log($"CameraMoveAndZoom = {value}");
            _allActions.TryGetValue(ActionType.PartDrag, out value);
            Debug.Log($"PartDrag = {value}");
            _allActions.TryGetValue(ActionType.PressInteractionInterface, out value);
            Debug.Log($"PressInteractionInterface = {value}");

            if ((!zeroTouchesInLastFrame && Input.touchCount == 0) /*2ге для ПК*/&& ClickedPart.MouseButtonUp)
            {
                EventEnded?.Invoke();
                zeroTouchesInLastFrame = true;
            }
                
        }

        private static void ChangeValueInAllActions(ActionType key, bool changeTo)
        {
            _allActions.Remove(key);
            _allActions.Add(key, changeTo);
        }
        private static bool CheckConditions(ActionType action)
        {
            switch (action)
            {
                case ActionType.CameraMoveAndZoom:
                    {
                        _allActions.TryGetValue(ActionType.PressInteractionInterface, out bool pressInteractionInterface);
                        _allActions.TryGetValue(ActionType.PartDrag, out bool partDrag);

                        if (!pressInteractionInterface && !partDrag)
                            return true;
                        else
                            return false;
                    }
                case ActionType.PressInteractionInterface:
                    {
                        _allActions.TryGetValue(ActionType.CameraMoveAndZoom, out bool cameraMoveAndZoom);
                        _allActions.TryGetValue(ActionType.PartDrag, out bool partDrag);

                        if (!cameraMoveAndZoom && !partDrag)
                            return true;
                        else
                            return false;
                    }
                case ActionType.PartDrag:
                    {
                        _allActions.TryGetValue(ActionType.CameraMoveAndZoom, out bool cameraMoveAndZoom);
                        _allActions.TryGetValue(ActionType.PressInteractionInterface, out bool pressInteractionInterface);

                        if (!cameraMoveAndZoom && !pressInteractionInterface)
                            return true;
                        else
                            return false;
                    }
                default:
                    throw new Exception($"Неможливо знайти значення {action}.");
            }
        }
        public static bool CheckConditionsAndStartEvent(ActionType action)
        {
            if (CheckConditions(action))
            {
                EventStarted?.Invoke(action);
                return true;
            }

            return false;
        }
        public static void OffCurrentAction()
        {
            foreach (var item in _allActions)
                if (item.Value == true)
                {
                    ChangeValueInAllActions(item.Key, false);
                    break;
                }
        }
        public static void OffAction(ActionType action) => ChangeValueInAllActions(action, false);
        public static bool SomeEventIsActive(List<ActionType> ignoreActions = null)
        {
            foreach (var item in _allActions)
                if (item.Value)
                    if (ignoreActions != null)
                    {
                        bool doContinue = false;

                        foreach (var action in ignoreActions)
                        {
                            if (item.Key == action)
                            {
                                doContinue = true;
                                break;
                            }   
                        }

                        if (doContinue)
                            continue;

                        return true;
                    }  
                    else
                        return true;

            return false;
        }
        private void TurnOnAction(ActionType actionType)
        {
            var changeValues = new Dictionary<ActionType, bool>();

            foreach (var item in _allActions)
                if (item.Key == actionType)
                    changeValues.Add(item.Key, true);
                else if (item.Value == true)
                    changeValues.Add(item.Key, false);

            foreach (var item in changeValues)
                ChangeValueInAllActions(item.Key, item.Value);
        }

        public enum ActionType
        {
            CameraMoveAndZoom,
            PressInteractionInterface,
            PartDrag
        }
    }
}