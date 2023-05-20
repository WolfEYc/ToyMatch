using UnityEngine;

namespace ToyMatch
{
    [RequireComponent(typeof(Collider))]
    public class ToySpawner : MonoBehaviour
    {
        public static ToySpawner Inst { get; private set; }
        public Collider bounds;
        
        
        Toy[] _toys;
        Transform _transform;
        Bounds _bounds;
        
        void Awake()
        {
            if (!SingletonInit()) return;
            _toys = Resources.LoadAll<Toy>("Toys");
            _transform = transform;
            _bounds = GetComponent<Collider>().bounds;
        }

        bool SingletonInit()
        {
            if (Inst != null)
            {
                Destroy(this);
                return false;
            }

            Inst = this;
            return true;
        }

        public void SpawnToys(int matches)
        {
            foreach (Transform child in _transform)
            {
                Destroy(child.gameObject);
            }
            
            for(int i = 0; i < matches; i++)
            {
                int idx = i % _toys.Length;
                for (int j = 0; j < 2; j++)
                {
                    Toy toy = Instantiate(_toys[idx], RandomPointInBounds(), Random.rotation, _transform);
                    toy.comparable = idx;
                }
            }
        }

        Vector3 RandomPointInBounds() {
            return new Vector3(
                Random.Range(_bounds.min.x, _bounds.max.x),
                Random.Range(_bounds.min.y, _bounds.max.y),
                Random.Range(_bounds.min.z, _bounds.max.z)
            );
        }
    }
}
