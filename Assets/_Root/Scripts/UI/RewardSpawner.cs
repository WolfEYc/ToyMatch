using System.Collections;
using UnityEngine;

namespace ToyMatch
{
    public class RewardSpawner : PoolSpawner
    {
        const float Interval = 0.2f;

        public void SpawnBunch(uint amt)
        {
            if (amt == 0) return;
            StartCoroutine(SpawnBunchRoutine(amt));
        }


        IEnumerator SpawnBunchRoutine(uint amt)
        {
            WaitForSeconds interval = new WaitForSeconds(Interval / amt);

            for (int i = 0; i < amt; i++)
            {
                Spawn();
                yield return interval;
            }
        }

        protected override void OnReturnedToPool(TTLPool inst)
        {
            base.OnReturnedToPool(inst);
            PointMaster.Inst.scoreNumber.ScorePoint();
        }
    }
}
