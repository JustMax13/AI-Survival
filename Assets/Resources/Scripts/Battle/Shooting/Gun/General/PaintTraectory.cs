using UnityEngine;
namespace CombatMechanics
{
    public class PaintTraectory : MonoBehaviour
    {
        private LineRenderer lineRendererComponent;
       //[SerializeField] private LayerMask _layer;
        private float _pointY, _pointX;
        private void Start()
        {
            lineRendererComponent = GetComponent<LineRenderer>();
        }

        public void ShowTrajectory(Vector3 origin, Vector3 speed, float radius, LayerMask layers)
        {
            Vector3[] points = new Vector3[30];
            lineRendererComponent.positionCount = points.Length;

            bool _working = false;
            int i = 0;
            while (_working == false)
            {
                
                float time = i * 0.1f;

                points[i].y = origin.y + speed.y * time + Physics.gravity.y * time * time / 2f;
                points[i].x = origin.x + speed.x * time;
                _pointY = points[i].y;
                if (i==29)
                {
                    break;
                }i++;
                _working = Physics2D.OverlapCircle(new Vector2(_pointX, _pointY), radius, layers, -10,10);
            } lineRendererComponent.SetPositions(points);
            lineRendererComponent.positionCount = i;
        }
    }
}