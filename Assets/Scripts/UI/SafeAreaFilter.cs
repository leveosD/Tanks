using UnityEngine;

public class SafeAreaFilter : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        var safeArea = Screen.safeArea;
        var screenSize = new Vector2(Screen.width, Screen.height);
        rectTransform.offsetMin = safeArea.position;
        rectTransform.offsetMax = screenSize - safeArea.max;

    }
}
