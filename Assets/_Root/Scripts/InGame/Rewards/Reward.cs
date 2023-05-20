using TMPro;
using UnityEngine;

namespace ToyMatch
{
    public abstract class Reward : MonoBehaviour
    {
        [SerializeField] TMP_Text amtIndicator;
        [SerializeField] uint cost;

        protected abstract void Use();

        void Awake()
        {
            amtIndicator.SetText(cost.ToString());
        }

        public void TryUse()
        {
            if (PointMaster.Inst.Points < cost) return;
            PointMaster.Inst.Expense(cost);
            Use();
        }
    }
}
