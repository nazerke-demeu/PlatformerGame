using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject levelPanel;
    public GameObject confirmPanel;

    public void PlayGame()
    {
        GameSession.StartLevel(0);
    }

    public void ShowLevels()
    {
        levelPanel.SetActive(true);
    }

    public void HideLevels()
    {
        levelPanel.SetActive(false);
    }

    public void ShowConfirm()
    {
        confirmPanel.SetActive(true);
    }

    public void HideConfirm()
    {
        confirmPanel.SetActive(false);
    }

            public void ConfirmReset()
    {
        SaveSystem.ResetAll();
        confirmPanel.SetActive(false);

        VolumeSlider[] sliders = FindObjectsByType<VolumeSlider>(FindObjectsSortMode.None);
        foreach (VolumeSlider s in sliders)
            s.Refresh();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}