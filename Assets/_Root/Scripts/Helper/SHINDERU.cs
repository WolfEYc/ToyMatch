using UnityEngine;

namespace ToyMatch
{
    public class SHINDERU : MonoBehaviour
    {
        [SerializeField] GameObject OmaeWa;

        public void MoShindEru()
        {
            Destroy(OmaeWa); //NANI?!?!?!?
        }
    }
}
