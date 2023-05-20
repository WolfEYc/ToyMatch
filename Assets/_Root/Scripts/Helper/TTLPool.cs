using UnityEngine;
using UnityEngine.Pool;

namespace ToyMatch
{
    public class TTLPool : MonoBehaviour
    {
        public GameObject Me { get; private set; }
        public ObjectPool<TTLPool> Pool;

        void Awake()
        {
            Me = gameObject;
        }

        public void Return()
        {
            Pool.Release(this);
        }
    }
}
