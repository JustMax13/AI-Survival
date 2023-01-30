using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public sealed class DragAndDrop : MonoBehaviour
    {
        private static float _minX;
        private static float _maxX;
        private static float _minY;
        private static float _maxY;
        public static float MinX { get => _minX; }
        public static float MaxX { get => _maxX; }
        public static float MinY { get => _minY; }
        public static float MaxY { get => _maxY; }         
        
        public static void Save2Point(GameObject limitPoint1, GameObject limitPoint2)
        {
            if (limitPoint1 == null || limitPoint2 == null)
            {
                Debug.Log("limitPoint1 or / and limitpoint2 have null. Nothing save.");
                return;
            }

            if (limitPoint1.transform.position.x > limitPoint2.transform.position.x)
            {
                _minX = limitPoint2.transform.position.x;
                _maxX = limitPoint1.transform.position.x;
            }
            else
            {
                _minX = limitPoint1.transform.position.x;
                _maxX = limitPoint2.transform.position.x;
            }
            if (limitPoint1.transform.position.y > limitPoint2.transform.position.y)
            {
                _minY = limitPoint2.transform.position.y;
                _maxY = limitPoint1.transform.position.y;
            }
            else
            {
                _minY = limitPoint1.transform.position.y;
                _maxY = limitPoint2.transform.position.y;
            }
        }
        public static Vector2 MousePositionOnDragArea(GameObject limitPoint1, GameObject limitPoint2)
        {
            try
            {
                Save2Point(limitPoint1, limitPoint2);

                Vector2 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                return new Vector2(Math.Clamp(mousePosition.x, _minX, _maxX),
                    Math.Clamp(mousePosition.y, _minY, _maxY));
            }
            catch
            {
                Debug.Log("limitPoint1 or / and limitpoint2 have null. Retorn new Vector2(0,0)");
                return new Vector2(0, 0);
            }
            
        }
    }
}

