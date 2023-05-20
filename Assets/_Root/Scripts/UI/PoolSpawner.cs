using UnityEngine;
using UnityEngine.Pool;

namespace ToyMatch
{
    public class PoolSpawner : MonoBehaviour
    {
        ObjectPool<TTLPool> _ttlPool;
        [SerializeField] TTLPool prefab;

        Transform _transform;

        void Awake()
        {
            _transform = transform;
            _ttlPool = new ObjectPool<TTLPool>(InstantiateTtlPool, OnTakeFromPool, OnReturnedToPool);
        }
        
        TTLPool InstantiateTtlPool()
        {
            TTLPool inst = Instantiate(prefab, _transform);
            inst.Pool = _ttlPool;
            return inst;
        }
        
        protected virtual void OnReturnedToPool(TTLPool inst)
        {
            inst.Me.SetActive(false);
        }

        void OnTakeFromPool(TTLPool inst)
        {
            inst.Me.SetActive(true);
        }

        public void Spawn()
        {
            _ttlPool.Get();
        }

        public void KillAll()
        {
            _ttlPool.Clear();
            foreach (Transform child in _transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
