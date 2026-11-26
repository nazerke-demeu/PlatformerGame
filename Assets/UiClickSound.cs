using UnityEngine;
using UnityEngine.UI;

public class UiClickSound : MonoBehaviour
{
    void Start()
    {
        foreach (Button b in GetComponentsInChildren<Button>(true))
        {
            b.onClick.AddListener(() =>
            {
                if (Sfx.I != null) Sfx.I.PlayUI(Sfx.I.click);
            });
        }
    }
}