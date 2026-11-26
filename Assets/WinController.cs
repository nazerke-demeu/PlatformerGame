using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    void Start()
    {
        if (Sfx.I != null) Sfx.I.PlayFixed(Sfx.I.win);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}