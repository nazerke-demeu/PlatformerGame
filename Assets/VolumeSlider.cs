using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        Refresh();
        slider.onValueChanged.AddListener(OnChanged);
    }

    public void Refresh()
    {
        if (slider == null) slider = GetComponent<Slider>();
        slider.SetValueWithoutNotify(SaveSystem.Data.musicVol);
        Music.SetVolume(SaveSystem.Data.musicVol);
    }

    void OnChanged(float v)
    {
        SaveSystem.Data.musicVol = v;
        Music.SetVolume(v);
        SaveSystem.Save();
    }
}