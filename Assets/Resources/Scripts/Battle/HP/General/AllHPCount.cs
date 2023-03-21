using System.Collections;
using UnityEngine;
namespace CombatMechanics
{
    using HP;
    public class AllHPCount : MonoBehaviour
    {
        [SerializeField] private float _HPStart, _HPInTime, _CPUHPStart, _CPUHPInTime;

        public float HPStart { get { return _HPStart; } }
        public float HPInTime { get { return _HPInTime; } set { _HPInTime = value; } }
        public float CPUHPStart { get { return _CPUHPStart; } }
        public float CPUHPInTime
        {
            get { return _CPUHPInTime; }
            set
            {
                _CPUHPInTime = value; 
                if (value == 0)
                {
                    Dest();
                }
            }
        }

        private void Start()
        {
            _HPStart = HPCalcut(false);
            _CPUHPStart = HPCalcut(true);
            StartCoroutine(Check());
        }
        private float HPCalcut(bool CPU, float i = 0, float j = 0)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.TryGetComponent<IHaveHP>(out IHaveHP hinge))
                    i += child.GetComponent<IHaveHP>().HP;
                if (child.gameObject.GetComponent<CentralBlockHP>())
                    j += child.GetComponent<IHaveHP>().HP;
            }
            if (CPU)
                return (j);
            else
                return (i);
        }
        private IEnumerator Check()
        {
            yield return new WaitForSeconds(1 / 60f);
            float i;
            i = HPCalcut(false);
            if (i / _HPStart <= 0.1)
            {
                Dest();
            }
            else
            {
                if (_HPInTime != i)
                {
                    _HPInTime = i;
                    _CPUHPInTime = HPCalcut(true);
                    if (_CPUHPInTime == 0)
                        _HPInTime = 0;
                }
                StartCoroutine(Check());
            }
        }
        private void Dest()
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.GetComponent<CentralBlockHP>())
                    child.GetComponent<IHaveHP>().HP = 0;
            }
            _HPInTime = 0;
            _CPUHPInTime = 0;
        }
    }
}