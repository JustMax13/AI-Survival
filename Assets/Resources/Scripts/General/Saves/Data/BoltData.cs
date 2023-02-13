//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace General.Saves
//{
//    public struct BoltData
//    {
//        public bool SpriteRendererEnabled { get; set; }
//        public SimplePosition Position { get; set; }

//        public BoltData(GameObject bolt)
//        {
//            try { SpriteRendererEnabled = bolt.GetComponent<SpriteRenderer>().enabled; }
//            catch { SpriteRendererEnabled = false; }

//            Position = new SimplePosition(bolt);
//        }
//    }
//}