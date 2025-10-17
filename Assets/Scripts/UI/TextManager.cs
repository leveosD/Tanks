using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] private List<Text> labels;
    [SerializeField] private List<string> tags;
    [SerializeField] private List<Font> fonts;

    private Font font;
    private int fontSize;

    private void Start()
    {
        UpdateUI(null);
    }

    public void UpdateUI(FlagButton flagButton)
    {
        string lang = PlayerPrefs.GetString("lang");
        Debug.Log(lang);
        switch (lang)
        {
            case "en":
                font = fonts[0];
                fontSize = 36;
                break;
            case "ru":
                font = fonts[1];
                fontSize = 48;
                break;
            case "tr":
                font = fonts[2];
                fontSize = 60;
                break;
            default:
                font = fonts[0];
                fontSize = 36;
                break;
        }
        
        for(int i = 0; i < labels.Count; i++)
        {
            labels[i].text = PlayerPrefs.GetString(tags[i]);
            if (tags[i] == "scoreText")
                labels[i].text += ": 0";
            labels[i].font = font;
            if(labels[i].fontSize == 30)
                labels[i].fontSize = fontSize;
            if (tags[i] == "gameover" && lang != "tr")
                labels[i].fontSize = 85;
            else if (tags[i] == "authText")
                labels[i].fontSize = 50;
        }
    }
}
