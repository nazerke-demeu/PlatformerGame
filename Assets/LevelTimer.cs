using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    public TMP_Text timerText;

    float timeLeft;

    void Start()
    {
        timeLeft = GameSession.timeLimit;
        if (timeLeft <= 0f)
            timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (timeLeft <= 0f) return;

        timeLeft -= Time.deltaTime;
        timerText.text = "TIME: " + Mathf.CeilToInt(Mathf.Max(timeLeft, 0f));

        if (timeLeft <= 0f)
        {
            float delay = 0f;
            if (Sfx.I != null)
            {
                Sfx.I.PlayFixed(Sfx.I.lose);
                delay = Sfx.I.loseDelay;
            }
            Invoke(nameof(Reload), delay);
        }
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}