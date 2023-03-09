using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatMechanics
{
    public class HPBars : MonoBehaviour
    {
        [SerializeField] private GameObject _HPBar,_CPUHPBar;
        [SerializeField] private bool canChangeY;
        [SerializeField] private AllHPCount count;
        
        public void getCount(AllHPCount c)
        {
            count = c;
            StartCoroutine(Check());
        }
        private IEnumerator Check()
        {
            yield return new WaitForSeconds(1/60);
            if (count.HPInTime / count.HPStart != _HPBar.transform.localScale.x)
            {
                if (canChangeY)
                {
                    _HPBar.transform.localScale = new Vector3(count.HPInTime / count.HPStart, count.HPInTime / count.HPStart, 1);
                    _CPUHPBar.transform.localScale = new Vector3(count.CPUHPInTime / count.CPUHPStart, count.CPUHPInTime / count.CPUHPStart, 1);
                }
                else
                {
                _HPBar.transform.localScale = new Vector3(count.HPInTime / count.HPStart, _HPBar.transform.localScale.y, 1);
                _CPUHPBar.transform.localScale = new Vector3(count.CPUHPInTime / count.CPUHPStart, _CPUHPBar.transform.localScale.y, 1);
                }
                
                

            }
            StartCoroutine(Check());
        }


    }
}