using System.Collections;
using TMPro;
using UnityEngine;

namespace CombatMechanics
{
    public class UIClock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _clock;

        private int _time;
        
        public int Time
        {
            set
            {
                _time = value;
                TimeCalcut();
            }
        }

        private void TimeCalcut()
        {
            int minute, second;
            minute = _time / 60;
            second = _time % 60;

            if(_clock != null)
            {
                if (second > 9)
                    _clock.text = minute + ":" + second;
                else
                    _clock.text = minute + ":0" + second;
                if (_time > 0)
                    StartCoroutine(Check());
            }
        }
        private IEnumerator Check()
        {
            yield return new WaitForSeconds(1);
            _time--;
            TimeCalcut();
        }
    }
}
