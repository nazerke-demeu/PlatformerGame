using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
        public void PlayGame()
    {
        GameSession.StartLevel(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}