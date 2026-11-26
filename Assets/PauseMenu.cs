using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    [Header("Audio")]
    public AudioMixer mixer;
    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot pausedSnapshot;
    public float transitionTime = 0.3f;

    void Start()
    {
        Time.timeScale = 1f;
        mixer.updateMode = AudioMixerUpdateMode.UnscaledTime;
        normalSnapshot.TransitionTo(0f);
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        pausedSnapshot.TransitionTo(transitionTime);
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        normalSnapshot.TransitionTo(transitionTime);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        normalSnapshot.TransitionTo(0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoMenu()
    {
        Time.timeScale = 1f;
        normalSnapshot.TransitionTo(0f);
        SceneManager.LoadScene("MainMenu");
    }
}