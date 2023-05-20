using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace ToyMatch
{
    public class Difficulty : MonoBehaviour
    {
        [SerializeField] Completion completion;
        [SerializeField] Timer timer;
        [SerializeField] ToySpawner spawner;
        [SerializeField] TMP_Text levelText;

        public UnityEvent gameStart;
        public UnityEvent gameStop;
        
        float _scale;
        uint _level;
        
        const float TimeMult = 5f;
        const float Min2Sec = 60f;
        const float MatchesPerMinute = 20f;

        void Awake()
        {
            LoadDifficultyFromDisk();
            InitGame();
        }

        public void InitGame()
        {
            _scale = Random.Range(0.2f, 0.8f);
            float minutes = _scale * TimeMult;
            int matches = (int)(minutes * (MatchesPerMinute + _level));
            timer.SetTimer(minutes * Min2Sec);
            spawner.SpawnToys(matches);
            completion.SetTotalMatches(matches);
            levelText.SetText(_level.ToString());
            
            gameStart.Invoke();
        }

        public void StopGame()
        {
            gameStop.Invoke();
        }

        void LoadDifficultyFromDisk()
        {
            _level = (uint)PlayerPrefs.GetInt("level", 1);
        }

        public void AdvanceLevel()
        {
            _level++;
        }

        void OnDestroy()
        {
            PlayerPrefs.SetInt("level", (int)_level);
        }
    }
}
