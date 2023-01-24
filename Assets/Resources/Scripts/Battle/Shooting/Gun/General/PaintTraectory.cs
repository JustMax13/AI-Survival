using UnityEngine;
namespace CombatMechanics
{
    public class PaintTraectory : MonoBehaviour
    {
        private LineRenderer lineRendererComponent;
       [SerializeField] private LayerMask _layer;
        private float _pointY, _pointX;
        private void Start()
        {
            lineRendererComponent = GetComponent<LineRenderer>();
        }

        public void ShowTrajectory(Vector3 origin, Vector3 speed)
        {
            Vector3[] points = new Vector3[20];
            lineRendererComponent.positionCount = points.Length;

            bool _working = false;
            int i = 0;
            while (_working == false)
            {
                
                float time = i * 0.1f;

                points[i] = origin + speed * time + Physics.gravity * time * time / 2f;
                _pointX = points[i].x;
                _pointY = points[i].y;
                if (i==19)
                {
                    break;
                }i++;
                _working = Physics2D.OverlapCircle(new Vector2(_pointX, _pointY), 0.5f, _layer,-10,10);
            } lineRendererComponent.SetPositions(points);
            lineRendererComponent.positionCount = i;
        }
    }
}