using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] buttons;
    public TMP_Text bestScoreText;

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            bool open = SaveSystem.IsUnlocked(i + 1);
            buttons[i].interactable = open;

            TMP_Text label = buttons[i].GetComponentInChildren<TMP_Text>();
            if (label != null)
                label.text = open ? "LEVEL " + (i + 1) : "LOCKED";
        }

        if (bestScoreText != null)
            bestScoreText.text = "BEST SCORE: " + SaveSystem.Data.bestScore;
    }

    public void Play(int index)
    {
        GameSession.StartLevel(index);
    }
}