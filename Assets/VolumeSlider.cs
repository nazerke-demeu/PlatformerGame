using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour, IPointerUpHandler
{
    public AudioMixer mixer;
    public string param = "MusicVol";

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0.0001f;
        slider.maxValue = 1f;
        Refresh();
        slider.onValueChanged.AddListener(OnChanged);
    }

    public void Refresh()
    {
        if (slider == null) slider = GetComponent<Slider>();
        float v = VolumeControl.Get(param);
        slider.SetValueWithoutNotify(v);
        mixer.SetFloat(param, VolumeControl.ToDb(v));
    }

    void OnChanged(float v)
    {
        VolumeControl.Set(param, v);
        mixer.SetFloat(param, VolumeControl.ToDb(v));
    }

    public void OnPointerUp(PointerEventData e)
    {
        SaveSystem.Save();
    }
}