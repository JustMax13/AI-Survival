using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.Moves
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
        
        public static void Save2Point(Vector2 limitPoint1, Vector2 limitPoint2)
        {
            if (limitPoint1 == null || limitPoint2 == null)
                throw new Exception("limitPoint1 or / and limitpoint2 have null. Nothing save.");

            if (limitPoint1.x > limitPoint2.x)
            {
                _minX = limitPoint2.x;
                _maxX = limitPoint1.x;
            }
            else
            {
                _minX = limitPoint1.x;
                _maxX = limitPoint2.x;
            }
            if (limitPoint1.y > limitPoint2.y)
            {
                _minY = limitPoint2.y;
                _maxY = limitPoint1.y;
            }
            else
            {
                _minY = limitPoint1.y;
                _maxY = limitPoint2.y;
            }
        }
        public static Vector2 MousePositionOnDragArea(Vector2 limitPoint1, Vector2 limitPoint2)
        {
            try
            {
                Save2Point(limitPoint1, limitPoint2);

                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                return new Vector2(Math.Clamp(mousePosition.x, _minX, _maxX),
                    Math.Clamp(mousePosition.y, _minY, _maxY));
            }
            catch
            {
                throw new Exception("limitPoint1 or / and limitpoint2 have null.");
            }
            
        }
    }
}

