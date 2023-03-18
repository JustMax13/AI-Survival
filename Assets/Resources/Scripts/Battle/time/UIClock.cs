using System.Collections;
using TMPro;
using UnityEngine;
namespace CombatMechanics
{
    public class UIClock : MonoBehaviour
    {
        private int _time;
        [SerializeField] private TextMeshProUGUI _clock;
        public int Time
        {
            set
            {
                _time = value;
                timeCalcut();
            }
        }
        private void timeCalcut()
        {
            int minute, second;
            minute = _time / 60;
            second = _time % 60;
            if (second > 9)
            {
                _clock.text = minute + ":" + second;
            }
            else
            {
                _clock.text = minute + ":0" + second;
            }
            if (_time > 0)
            {
                StartCoroutine(Check());
            }
        }
        private IEnumerator Check()
        {
            yield return new WaitForSeconds(1);
            _time--;
            timeCalcut();
        }
    }
}
