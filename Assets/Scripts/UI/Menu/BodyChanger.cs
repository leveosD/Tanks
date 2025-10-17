using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YandexNamespace;

public class BodyChanger : MonoBehaviour
{
    private List<Button> buttons = new List<Button>();
    private Text text;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>().ToList();
        text = GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {
        text.text = PlayerPrefs.GetString("authText");
        buttons[2].gameObject.SetActive(false);
        buttons[0].gameObject.SetActive(true);
        buttons[1].gameObject.SetActive(true);
        Yandex.LoadPhoto += Change;
        buttons[0].onClick.AddListener(Change);
    }

    private void OnDisable()
    {
        Yandex.LoadPhoto -= Change;
        buttons[0].onClick.RemoveListener(Change);
    }

    private void Change()
    {
        string url = PlayerPrefs.GetString("imageUrl");

        if (url == "" && url == "error")
        {
            text.text = PlayerPrefs.GetString("error");
            buttons[0].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(true);
        }
        else
        {
            buttons[1].onClick?.Invoke();
        }
    }
}
