using UnityEngine;
using UnityEngine.Audio;

public static class VolumeControl
{
    public static float ToDb(float v)
    {
        return Mathf.Log10(Mathf.Max(v, 0.0001f)) * 20f;
    }

    public static float Get(string param)
    {
        return param == "MusicVol" ? SaveSystem.Data.musicVol : SaveSystem.Data.sfxVol;
    }

    public static void Set(string param, float v)
    {
        if (param == "MusicVol") SaveSystem.Data.musicVol = v;
        else SaveSystem.Data.sfxVol = v;
    }

    public static void ApplyAll(AudioMixer mixer)
    {
        mixer.SetFloat("MusicVol", ToDb(SaveSystem.Data.musicVol));
        mixer.SetFloat("SFXVol", ToDb(SaveSystem.Data.sfxVol));
    }
}