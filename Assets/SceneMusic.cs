using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public AudioClip track;

    void Start()
    {
        if (Music.Instance != null)
            Music.Instance.Play(track);
    }
}