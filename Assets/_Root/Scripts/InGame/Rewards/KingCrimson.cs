using UnityEngine;

namespace ToyMatch
{
    public class KingCrimson : Reward
    {
        [SerializeField] Timer timer;
        [SerializeField] float extension;
        
        protected override void Use()
        {
            timer.ExtendTime(extension);
        }
    }
}
