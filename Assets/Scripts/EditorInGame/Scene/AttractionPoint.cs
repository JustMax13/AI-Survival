using UnityEngine;

namespace Editor
{
    public class AttractionPoint : MonoBehaviour
    {
        [SerializeField] private AttractionObject _attractionObj;

        private bool _isConected;
        private bool _onTrigger;
        private const int MaxCountConnectedObject = 2;
        private int _currentCountConnectedObject;
        private Collider2D _findedCollider;

        public int CurrentCountConnectedObject
        {
            get => _currentCountConnectedObject;
            set
            {
                if (value < 0) value = 0;
                _currentCountConnectedObject = value;
            }
        }
        public AttractionObject AttractionObj { get => _attractionObj; }
        private void Start()
        {
            if (_attractionObj == null)
            {
                _attractionObj = new AttractionObject();
                Debug.Log($"AttractionObject is null. Pls add AttractionObject on {gameObject.name}");
            }

            _isConected = false;
            _onTrigger = false;
            _currentCountConnectedObject = 0;
        }
        private void OnTriggerEnter2D(Collider2D collision) => _onTrigger = true;
        private void OnTriggerExit2D(Collider2D collision) => _onTrigger = false;
        private void OnTriggerStay2D(Collider2D collision)
        {
            _findedCollider = collision;

            if (!collision.GetComponent<AttractionPoint>()) return;

            if (_attractionObj.WasMouseDown && !_attractionObj.IsDrag && collision.GetComponent<AttractionPoint>()
                .CurrentCountConnectedObject < MaxCountConnectedObject)
            {
                MovingObject(collision);
                Connect(collision);

                _isConected = true;
            }
        }
        private void MovingObject(Collider2D collision)
        {
            GameObject magnet = new GameObject();

            magnet.transform.SetParent(_attractionObj.transform);
            magnet.transform.localPosition = gameObject.transform.localPosition;
            magnet.transform.parent = null;

            _attractionObj.gameObject.transform.SetParent(magnet.transform);
            magnet.transform.position = collision.transform.position;

            _attractionObj.transform.parent = null;
            Destroy(magnet);
        }
        private void Connect(Collider2D collision)
        {
            if (collision.GetComponent<AttractionPoint>().AttractionObj != _attractionObj)
            {
                FixedJoint2D jointOnObject = _attractionObj.gameObject.AddComponent<FixedJoint2D>();
                jointOnObject.anchor = gameObject.transform.localPosition;

                jointOnObject.connectedBody = collision.GetComponent<AttractionPoint>().AttractionObj
                    .gameObject.GetComponent<Rigidbody2D>();
                

                //_connectedFixedJoint2D[_currentCountConnectedObject++] = jointOnObject;
                //collision.GetComponent<AttractionPoint>().CurrentCountConnectedObject = _currentCountConnectedObject;
            }
            // прописати силу розриву деталей
        }
        private void Disconnect()
        {
            if (_attractionObj.GetComponent<FixedJoint2D>())
            {
                foreach (var item in _attractionObj.GetComponents<FixedJoint2D>()) Destroy(item);  
            }
            _isConected = false;
            _currentCountConnectedObject = 0;
        }
        private void Update()
        {
            if (_attractionObj.IsDrag) Disconnect();
            else if (_attractionObj.WasMouseDown && _findedCollider != null && _onTrigger)
                OnTriggerStay2D(_findedCollider);
        }
        private void FixedUpdate()
        {
            int count = 0;
            foreach (var item in Physics2D.OverlapPointAll(gameObject.transform.position))
            {
                if (item.GetComponent<AttractionPoint>() && item.GetComponent<AttractionPoint>()._isConected) count++;
            }
            _currentCountConnectedObject = count;
        }
    }
}