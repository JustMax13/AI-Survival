using UnityEngine;

namespace Editor
{
    public class AttractionPoint : MonoBehaviour
    {
        [SerializeField] private AttractionObject _attractionObj;

        private bool _isConected;
        private bool _onTrigger;
        private const int MaxCountConnectedObject = 3;
        private int _currentCountConnectedObject;
        private Collider2D _findedCollider;
        private SpriteRenderer _pointSprite;
        private FixedJoint2D[] _connectedFixedJoints;

        public FixedJoint2D[] ConnectedFixedJoints
        {
            get => _connectedFixedJoints;
            set { _connectedFixedJoints = value; }
        }
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

                for (int i = 0; i < _connectedFixedJoints.Length; i++)
                {
                    if (_connectedFixedJoints[i] == null)
                    {
                        _connectedFixedJoints[i] = jointOnObject;
                        break;
                    }

                }
            }
            // прописати силу розриву деталей
        }
        private void Disconnect()
        {
            if (_attractionObj.GetComponent<FixedJoint2D>())
            {
                foreach (var item in _attractionObj.GetComponents<FixedJoint2D>()) Destroy(item);
            }
            for (int i = 0; i < _connectedFixedJoints.Length; i++)
                _connectedFixedJoints[i] = null;
            _isConected = false;
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
            _pointSprite = gameObject.GetComponent<SpriteRenderer>() != null ? gameObject.GetComponent<SpriteRenderer>() : null;
            _connectedFixedJoints = new FixedJoint2D[MaxCountConnectedObject];
        }
        private void Update()
        {
            if (_attractionObj.IsDrag) Disconnect();
            else if (_attractionObj.WasMouseDown && _findedCollider != null && _onTrigger)
                OnTriggerStay2D(_findedCollider);
        }
        private void LateUpdate()
        {
            if (_isConected)
            {
                int count = 0;
                foreach (var item in _connectedFixedJoints)
                {
                    if (item != null) count++;
                }
                bool oneBoltON = false;

                if (count == 1) 
                    oneBoltON = true;
                
                Debug.Log(oneBoltON);

                if (_pointSprite && !_pointSprite.enabled && oneBoltON)
                    _pointSprite.enabled = true;
            }
            else if (_pointSprite && _pointSprite.enabled)
                _pointSprite.enabled = false;
        }
        private void FixedUpdate()
        {
            bool haveConnectedObject = false;
            foreach (var item in _connectedFixedJoints)
            {
                if (item != null) haveConnectedObject = true;
            }
            _isConected = haveConnectedObject;

            int count = 0;
            foreach (var item in Physics2D.OverlapPointAll(gameObject.transform.position))
            {
                if (item.GetComponent<AttractionPoint>() && item.GetComponent<AttractionPoint>()._isConected) count++;
            }
            _currentCountConnectedObject = count;
        }
    }
}