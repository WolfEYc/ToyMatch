using TMPro;
using UnityEngine;

namespace ToyMatch
{
    [RequireComponent(typeof(Animator), typeof(TMP_Text))]
    public class ScoreNumber : MonoBehaviour
    {
        Animator _animator;
        TMP_Text _scoretext;
        static readonly int PointAdded = Animator.StringToHash("PointAdded");

        int _delayedPoints;
        
        void Awake()
        {
            _animator = GetComponent<Animator>();
            _scoretext = GetComponent<TMP_Text>();
        }

        public void DocPts(uint amt)
        {
            _delayedPoints -= (int)amt;
            UpdateText();
        }

        public void ScorePoint()
        {
            _delayedPoints++;
            _animator.SetTrigger(PointAdded);
            UpdateText();
        }

        public void NewGame()
        {
            _delayedPoints = 0;
            UpdateText();
        }

        void UpdateText()
        {
            _scoretext.SetText(_delayedPoints.ToString());
        }
    }
}
