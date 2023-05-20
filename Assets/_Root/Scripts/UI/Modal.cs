using UnityEngine;

namespace ToyMatch
{
    [RequireComponent(typeof(Animator))]
    public class Modal : MonoBehaviour
    {
        Animator _animator;
        [SerializeField] GameObject menu;
        static readonly int Open = Animator.StringToHash("open");

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Toggle(bool on)
        {
            _animator.SetBool(Open, on);
        }

        public void ToggleMenu()
        {
            menu.SetActive(_animator.GetBool(Open));
        }
        
    }
}
