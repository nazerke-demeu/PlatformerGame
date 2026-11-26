using System.Collections;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance;

    public AudioSource a;
    public AudioSource b;
    public float fadeTime = 1f;

    AudioSource current;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        current = a;
        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip clip)
    {
        if (clip == null) return;
        if (current.clip == clip && current.isPlaying) return;

        AudioSource next = (current == a) ? b : a;

        StopAllCoroutines();
        next.Stop();
        next.clip = clip;
        next.volume = 0f;
        next.Play();

        StartCoroutine(Crossfade(current, next));
        current = next;
    }

    IEnumerator Crossfade(AudioSource from, AudioSource to)
    {
        for (float t = 0f; t < 1f; t += Time.unscaledDeltaTime / fadeTime)
        {
            from.volume = 1f - t;
            to.volume = t;
            yield return null;
        }
        from.Stop();
        to.volume = 1f;
    }

    public static void SetVolume(float v)
    {
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