using System.Collections;
using UnityEngine;
namespace CombatMechanics
{
    public class HPBars : MonoBehaviour
    {
        [SerializeField] private GameObject _HPBar, _CPUHPBar,_HPImage,_CPUHPImage;
        [SerializeField] private bool _canChangeY;
        [SerializeField] private AllHPCount _count;

        public AllHPCount Count 
        {
            set
            {
                _count = value;
                StartCoroutine(Check());
            }
            get => _count;
        }
        private IEnumerator Check()
        {
            yield return new WaitForSeconds(1 / 60f);
            if (_count.HPInTime / _count.HPStart != _HPBar.transform.localScale.x)
            {
                if (_canChangeY)
                {
                    _HPBar.transform.localScale = new Vector3(_count.HPInTime / _count.HPStart, _count.HPInTime / _count.HPStart, 1);
                    _CPUHPBar.transform.localScale = new Vector3(_count.CPUHPInTime / _count.CPUHPStart, _count.CPUHPInTime / _count.CPUHPStart, 1);
                    if (_count.HPInTime != 0)
                    {
                        _HPImage.transform.localScale = new Vector3(0.5f / _HPBar.transform.localScale.x,1 / _HPBar.transform.localScale.y, 1);
                        _CPUHPImage.transform.localScale = new Vector3(0.5f / _CPUHPBar.transform.localScale.x, 1 / _CPUHPBar.transform.localScale.y, 1);
                    }
                }
                else
                {
                    _HPBar.transform.localScale = new Vector3(_count.HPInTime / _count.HPStart, _HPBar.transform.localScale.y, 1);
                    _CPUHPBar.transform.localScale = new Vector3(_count.CPUHPInTime / _count.CPUHPStart, _CPUHPBar.transform.localScale.y, 1);
                    if (_count.HPInTime != 0)
                    {
                        _HPImage.transform.localScale = new Vector3(0.5f / _HPBar.transform.localScale.x, _HPBar.transform.localScale.y, 1);
                        _CPUHPImage.transform.localScale = new Vector3(0.5f / _CPUHPBar.transform.localScale.x, _CPUHPBar.transform.localScale.y, 1);
                    }
                }

            }
            StartCoroutine(Check());
        }


    }
}