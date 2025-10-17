using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class ResolutionController : MonoBehaviour
{
    private RectTransform canvasTransform;

    private void Awake()
    {
        canvasTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        bool isDesktop = PlayerPrefs.GetString("device") == "desktop";
        if (!isDesktop)
        {
            StartCoroutine(WaitRotation());
        }
    }
    private IEnumerator WaitRotation()
    {
        yield return new WaitUntil(() => Screen.orientation == ScreenOrientation.LandscapeLeft ||
                                         Screen.orientation == ScreenOrientation.LandscapeRight);
        YandexNamespace.Yandex.UpdateResolution();
    }
}
