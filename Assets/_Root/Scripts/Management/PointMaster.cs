using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ToyMatch
{
    public class PointMaster : MonoBehaviour
    {
        public static PointMaster Inst { get; private set; }
        
        
        [SerializeField] UnityEvent<string> multiplierUpdated;
        [SerializeField] UnityEvent<float> fillUpdated;
        [SerializeField] UnityEvent<uint> pointsAdded;
        public ScoreNumber scoreNumber;

        void Awake()
        {
            if (Inst != null)
            {
                Destroy(this);
                return;
            }

            Inst = this;
        }

        const float CountDownMult = 20, CountDownConst = 1;

        float _countDownTime;
        float _endTime;

        void Start()
        {
            Init();
        }

        public uint Points { get; private set; }

        void Init()
        {
            Multiplier = 1;
            fillUpdated.Invoke(0);
        }
        
        uint _multiplier;

        public uint Multiplier
        {
            get => _multiplier;
            set
            {
                _multiplier = value;
                multiplierUpdated.Invoke(_multiplier.ToString());   
            }
        }

        IEnumerator _countDown;

        IEnumerator MultiplierCountDown()
        {
            uint tempMult = Multiplier;
            _countDownTime = CountDownMult / Multiplier + CountDownConst;
            _endTime = Time.time + _countDownTime;

            yield return new WaitForSeconds(_countDownTime);
            if (tempMult != Multiplier) yield break;
            
            Multiplier = 1;
            fillUpdated.Invoke(0);
        }

        IEnumerator FillUpdate()
        {
            do
            {
                yield return null;
                fillUpdated.Invoke(Fill());
            } while (Multiplier != 1);
        }

        float Fill()
        {
            if (Multiplier == 1)
            {
                return 0f;
            }
            
            return (_endTime - Time.time) / _countDownTime;
        }

        public void Match()
        {
            if (Multiplier > 1)
            {
                StopCoroutine(_countDown);
            }
            else
            {
                StartCoroutine(FillUpdate());
            }
            
            Points += Multiplier;
            
            pointsAdded.Invoke(Multiplier);

            Multiplier++;
            
            _countDown = MultiplierCountDown();
            StartCoroutine(_countDown);
        }

        public void Clean()
        {
            Multiplier = 1;
            Points = 0;
        }

        public void Expense(uint amt)
        {
            Points -= amt;
            scoreNumber.DocPts(amt);
        }
    }
}
