using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    public Health player;
    public Image[] hearts;

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
            hearts[i].enabled = i < player.hp;
    }
}