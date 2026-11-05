using UnityEngine;

public class Sfx : MonoBehaviour
{
    public static Sfx I;

    public AudioClip jump;
    public AudioClip coin;
    public AudioClip hurt;
    public AudioClip shoot;

    AudioSource src;

    void Awake()
    {
        I = this;
        src = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        if (clip != null)
            src.PlayOneShot(clip);
    }
}