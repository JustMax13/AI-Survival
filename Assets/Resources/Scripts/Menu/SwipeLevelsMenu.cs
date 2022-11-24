using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Menu.ChoseLevel
{
    public class SwipeLevelsMenu : MonoBehaviour
    {
        private GameObject LevelsButton;
        private Vector2 _start, _end;
        private float _levelsPositionX,_min,_max,_step=10;
     
        private void Start()
        {
            LevelsButton = gameObject;
            _max = LevelsButton.transform.position.x;
            if(LevelsButton.transform.childCount>5)_min = _max - (LevelsButton.transform.GetChild(LevelsButton.transform.childCount-5).transform.position.x+_step)/1.5f;
        }

        private void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _start = Input.GetTouch(0).position;
                _levelsPositionX = LevelsButton.transform.position.x;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                _end = Input.GetTouch(0).position;
                LevelsButton.transform.position = new Vector2(_levelsPositionX + (_end.x-_start.x), LevelsButton.transform.position.y);
                if (LevelsButton.transform.position.x > _max)
                {
                    LevelsButton.transform.position = new Vector2(_max, LevelsButton.transform.position.y);
                }
                if (LevelsButton.transform.position.x <_min)
                {
                    LevelsButton.transform.position = new Vector2(_min, LevelsButton.transform.position.y);
                }
            }
        }
    }
}