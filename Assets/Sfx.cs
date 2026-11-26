using UnityEngine;

public class Sfx : MonoBehaviour
{
    public static Sfx I;

    [Header("Sources (Output = mixer group)")]
    public AudioSource sfxSource;
    public AudioSource uiSource;

    [Header("Clips")]
    public AudioClip jump;
    public AudioClip coin;
    public AudioClip shoot;
    public AudioClip[] hits;
    public AudioClip enemyWindup;
    public AudioClip death;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip click;

    [Header("Variation and timing")]
    public float pitchMin = 0.92f;
    public float pitchMax = 1.08f;
    public float deathDelay = 0.9f;
    public float loseDelay = 1f;
    int lastHit = -1;

    void Awake()
    {
        I = this;
    }

    public void Play(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.pitch = Random.Range(pitchMin, pitchMax);
        sfxSource.PlayOneShot(clip);
    }

    public void PlayFixed(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayUI(AudioClip clip)
    {
        if (clip == null) return;
        uiSource.PlayOneShot(clip);
    }

    public void PlayHit()
    {
        if (hits == null || hits.Length == 0) return;
        int i = Random.Range(0, hits.Length);
        if (i == lastHit && hits.Length > 1) i = (i + 1) % hits.Length;
        lastHit = i;
        Play(hits[i]);
    }
}