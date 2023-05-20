using UnityEngine;
using UnityEngine.Events;

namespace ToyMatch
{
    public class Completion : MonoBehaviour
    {
        [SerializeField] UnityEvent success;

        int _matchesLeft;

        public void SetTotalMatches(int matches)
        {
            _matchesLeft = matches;
        }
        
        public void Match()
        {
            _matchesLeft--;
            if (_matchesLeft == 0)
            {
                success.Invoke();
            }
        }
    }
}
