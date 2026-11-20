using UnityEngine;

public class Music : MonoBehaviour
{
    static Music instance;
    AudioSource src;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        src = GetComponent<AudioSource>();
        src.volume = SaveSystem.Data.musicVol;
    }

    public static void SetVolume(float v)
    {
        if (instance != null && instance.src != null)
            instance.src.volume = v;
    }

    void OnApplicationPause(bool paused)
    {
        if (paused) SaveSystem.Save();
    }

    void OnApplicationQuit()
    {
        SaveSystem.Save();
    }
}