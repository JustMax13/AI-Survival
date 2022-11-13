using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Saves
{
    public struct BoltData 
    {
        public bool SpriteRendererEnabled { get; private set; }
        public SimplePosition Position { get; private set; }

        public BoltData(GameObject bolt)
        {
            SpriteRendererEnabled = bolt.GetComponent<SpriteRenderer>().enabled;
            Position = new SimplePosition(bolt);
        }
    }
}