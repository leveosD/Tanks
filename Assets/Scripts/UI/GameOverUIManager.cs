using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager: MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text recordText;
    [SerializeField] private Image blood;

    public static event Action<bool> GameOverSound;

    private void OnEnable()
    {
        int record = YandexNamespace.Yandex.Record;
        int score = PlayerPrefs.GetInt("score") - 1;

        if (score > record)
        {
            GameOverSound?.Invoke(true);
            recordText.text = PlayerPrefs.GetString("recordText") + ": " + score;
        }
        else
        {
            recordText.text = PlayerPrefs.GetString("recordText") + ": " + record;
            GameOverSound?.Invoke(false);
        }
        
        scoreText.text = PlayerPrefs.GetString("scoreText") + ": " + score;
        if (PlayerPrefs.GetString("lang") == "rus"){
            blood.rectTransform.anchoredPosition = new Vector2(-50, 1);
        }
    }
}
