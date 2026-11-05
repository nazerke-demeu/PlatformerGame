using UnityEngine;

public class SafeArea : MonoBehaviour
{
    RectTransform rt;
    Rect last;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        Apply();
    }

    void Update()
    {
        if (Screen.safeArea != last)
            Apply();
    }

    void Apply()
    {
        Rect s = Screen.safeArea;
        last = s;

        Vector2 min = s.position;
        Vector2 max = s.position + s.size;

        min.x /= Screen.width;
        min.y /= Screen.height;
        max.x /= Screen.width;
        max.y /= Screen.height;

        rt.anchorMin = min;
        rt.anchorMax = max;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
}