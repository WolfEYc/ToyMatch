using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyMatch
{
    public class GameMaster : MonoBehaviour
    {
        [SerializeField] SceneAsset gameScene, homeScene;
        
        public void Quit()
        {
            Application.Quit();
        }

        public void LaunchGame()
        {
            SceneManager.LoadScene(gameScene.name);
        }
        public void ExitGame()
        {
            SceneManager.LoadScene(homeScene.name);
        }

        public void Pause()
        {
            Time.timeScale = 0f;
        }

        public void Play()
        {
            Time.timeScale = 1f;
        }
    }
}
