using UnityEngine;

namespace ToyMatch
{
    public class Garbage : MonoBehaviour
    {
        [SerializeField] Animator left, right;
        static readonly int Open = Animator.StringToHash("open");
        
        public void Dump()
        {
            left.SetTrigger(Open);
            right.SetTrigger(Open);
        }
    }
}
