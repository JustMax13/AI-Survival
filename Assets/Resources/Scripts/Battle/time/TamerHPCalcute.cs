using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    public class TamerHPCalcute : MonoBehaviour
    {
        public AllHPCount PlayerHPCount { get; set; }
        public AllHPCount EnemyHPCount { get; set; }
        [SerializeField] private int _time;
        [SerializeField] private UIClock _timer;
        void Start()
        {
            _timer.Time = _time;
            StartCoroutine(Check(0));
        }
        private IEnumerator Check(int now)
        {
            yield return new WaitForSeconds(1);
            if (now >= _time)
            {
                if(PlayerHPCount.HPInTime> EnemyHPCount.HPInTime)//PlayerHPCount.CPUHPInTime> EnemyHPCount.CPUHPInTime
                {
                    EnemyHPCount.CPUHPInTime = 0;
                }
                else
                {
                    PlayerHPCount.CPUHPInTime = 0;
                }
                
                //PlayerHPCount.CPUHPInTime = 0;
            }
            else
            {
                StartCoroutine(Check(now+1));
            }
        }
    }
}
