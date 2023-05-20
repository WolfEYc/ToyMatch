using System;
using UnityEngine;

namespace ToyMatch
{
    [RequireComponent(typeof(Collider))]
    public class Pedestal : MonoBehaviour
    {
        [SerializeField] Transform leftpos, rightpos;
        [SerializeField] Garbage garbage;

        [HideInInspector] public Toy left, right;

        Transform _transform;

        void Awake()
        {
            _transform = transform;
        }

        bool _right;

        void OnEnable()
        {
            SelectMaster.ToySelected += OnToySelected;
        }

        public void Clean()
        {
            left = null;
            right = null;
            _right = false;
        }

        void OnTriggerEnter(Collider c)
        {
            Toy toy = c.GetComponent<Toy>();
            
            if(!MatchLeft(toy)) return;
            
            PutOn(toy);
        }

        bool MatchLeft(Toy toy)
        {
            return !_right || toy.IsMatch(left);
        }

        void OnToySelected()
        {
            if (SelectMaster.Selected != left) return;

            left = null;
            _right = false;
        }
        
        void PutOn(Toy toy)
        {
            if (_right)
            {
                right = toy;
                
                toy.SetSelectPos(rightpos.position);
                
                
                toy.Freeze();
                //toy.transform.parent = transform;
                Match();
            }
            else
            {
                left = toy;
                toy.SetSelectPos(leftpos.position);
                _right = true;
                
                //for now
                toy.Freeze();
                //toy.transform.parent = transform;
            }
        }

        void Match()
        {
            var position = _transform.position;
            left.SetSelectPos(position);
            right.SetSelectPos(position);
            
            left.HandleMatched();
            right.HandleMatched();
            
            _right = false;
            
            garbage.Dump();
            PointMaster.Inst.Match();
        }

        void OnDisable()
        {
            SelectMaster.ToySelected -= OnToySelected;
        }
    }
}
