using TMPro;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public TMP_Text coinText;
    public int coins;

    void Start()
    {
        coinText.text = "Coins: " + coins;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
                        Sfx.I.Play(Sfx.I.coin);
            coinText.text = "Coins: " + coins;
            Destroy(other.gameObject);
        }
    }
}