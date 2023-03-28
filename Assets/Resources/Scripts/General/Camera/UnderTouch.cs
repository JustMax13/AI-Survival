using Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class UnderTouch : MonoBehaviour
    {
        private static PluggableObject _underTouch = null;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] collider2D = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                foreach (var item in collider2D)
                {
                    try { _underTouch = item.GetComponent<PluggableObject>(); }
                    catch { continue; }

                    if(_underTouch != null)
                    {
                        if (!_underTouch.IsSelected)
                            _underTouch = null;
                        else
                        {
                            _underTouch.MouseDown();
                            break;
                        }
                    }
                }
            }
                
            if (Input.GetMouseButtonUp(0))
                if(_underTouch != null)
                {
                    _underTouch.MouseUp();
                    _underTouch = null;
                }
        }
    }
}