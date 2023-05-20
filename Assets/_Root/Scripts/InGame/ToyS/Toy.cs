using System.Collections;
using UnityEngine;

namespace ToyMatch
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Toy : MonoBehaviour
    {
        Rigidbody _rb;
        Collider _collider;
        GameObject _gameObject;
        [HideInInspector] public int comparable;

        const int DefaultLayer = 9, SelectedLayer = 10, CandidateLayer = 14;
        const float SelectedHeight = 1.5f;
        const float Speed = 10f;
        const float ThreshHold = 1f;
        const float MoveForce = 4f;

        bool _selected;
        bool _frozen;
        
        Vector3 _selectPos;

        static readonly WaitForFixedUpdate WaitForFixedUpdate = new();
        WaitUntil _waitUntilSpeedUnderThreshold;
        IEnumerator _turnOffCollisionDetection;
        [SerializeField] Animator animator;
        static readonly int Dead = Animator.StringToHash("dead");

        bool SpeedUnderThreshold()
        {
            return _rb.velocity.sqrMagnitude < ThreshHold;
        }
        
        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _gameObject = gameObject;
            _selectPos.y = SelectedHeight;
            _waitUntilSpeedUnderThreshold = new WaitUntil(SpeedUnderThreshold);
            _turnOffCollisionDetection = ToggleCollisionDetection();
            StartCoroutine(_turnOffCollisionDetection);
        }

        public void Deselect()
        {
            if (!_selected) return;
            _selected = false;
            _rb.mass = 1f;
            _turnOffCollisionDetection = ToggleCollisionDetection();
            _gameObject.layer = CandidateLayer;
            StartCoroutine(_turnOffCollisionDetection);
        }

        public void Select()
        {
            if (_selected) return;

            _selected = true;

            _rb.mass = 5f;
            
            UnFreeze();
            
            if (_rb.collisionDetectionMode == CollisionDetectionMode.Continuous)
            {
                StopCoroutine(_waitUntilSpeedUnderThreshold);
            }
            
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            _gameObject.layer = SelectedLayer;
            
            SelectMaster.OnToySelected(this);
            StartCoroutine(MoveToSelectedPos());
        }

        public void Freeze()
        {
            if (_frozen) return;
            
            _frozen = true;
            _gameObject.layer = DefaultLayer;
            _rb.isKinematic = true;
            _collider.isTrigger = true;
            _rb.angularVelocity = Vector3.zero;
            _rb.rotation = Quaternion.identity;
            _rb.velocity = Vector3.zero;
            StartCoroutine(MoveToFrozenPos());
        }

        void UnFreeze()
        {
            if (!_frozen) return;
            _frozen = false;
            _collider.isTrigger = false;
            _rb.isKinematic = false;
            _selectPos.y = SelectedHeight;
        }

        public void SetSelectPos(Vector3 selectedPos)
        {
            _selectPos = selectedPos;
        }

        public bool IsMatch(Toy other)
        {
            return other.comparable == comparable;
        }

        //only implement as backup if still peepee poopoo
        bool InBounds()
        {
            return ToySpawner.Inst.bounds.ClosestPoint(_rb.position) == _rb.position;
        }

        public void HandleMatched()
        {
            _gameObject.layer = SelectedLayer;
            animator.SetBool(Dead, true);
            StartCoroutine(MoveToFrozenPos());
        }

        IEnumerator ToggleCollisionDetection()
        {
            yield return _waitUntilSpeedUnderThreshold;
            _rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            _gameObject.layer = DefaultLayer;
        }

        IEnumerator MoveToSelectedPos()
        {
            while (_selected)
            {
                Vector3 mousepos = Pointer.Inst.mousePointer.ReadValue<Vector2>();
                mousepos = Pointer.Main.ScreenToWorldPoint(mousepos);
                //DebugGraph.Log(mousepos);
                
                _selectPos.x = mousepos.x;
                _selectPos.z = mousepos.z;
                
                _rb.velocity = (_selectPos - _rb.position) * Speed;
                yield return WaitForFixedUpdate;
            }
        }

        IEnumerator MoveToFrozenPos()
        {
            while (_frozen && _rb.position != _selectPos)
            {
                yield return WaitForFixedUpdate;
                _rb.position = Vector3.MoveTowards(_rb.position, _selectPos, Time.fixedDeltaTime * MoveForce);
            }
        }
        
        
    }
}
