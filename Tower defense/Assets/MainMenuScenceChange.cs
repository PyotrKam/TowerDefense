
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefence
{
    public class MainMenuScenceChange : MonoBehaviour
    {
        public void Change()
        {
            SceneManager.LoadScene(0);
        }
    }
}

