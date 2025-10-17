using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YandexNamespace;

public class LanguageManager : MonoBehaviour
{
    [SerializeField] private FlagButton frontButton;
    
    [SerializeField] private FlagInfo _flagInfo;
    public FlagInfo FlagInfo => _flagInfo;

    private List<FlagInfo> flags;

    [SerializeField] private TextManager textManager;

    private void Start()
    {
        FirstSetting();
    }

    private void FirstSetting()
    {
        frontButton = GetComponentsInChildren<FlagButton>()[0];
        flags = Resources.LoadAll<FlagInfo>("Flags").ToList();
        
        string lang = PlayerPrefs.GetString("lang");
        Debug.Log(lang);
        FlagButton target = GetComponentsInChildren<FlagButton>().First(button => button.Id == lang);
        if (lang != "" && lang != "en")
        {
            OnFlagButtonClicked(target);
        }
    }
    
    private void OnEnable()
    {
        FlagButton.LanguageUpdate += OnFlagButtonClicked;
        Yandex.UpdateLanguage += FirstSetting;
    }

    private void OnDisable()
    {
        FlagButton.LanguageUpdate -= OnFlagButtonClicked;
        Yandex.UpdateLanguage -= FirstSetting;
    }

    private void SetPrefs()
    {
        PlayerPrefs.SetString("lang", _flagInfo.id);
        PlayerPrefs.SetString("title", _flagInfo.title);
        PlayerPrefs.SetString("start", _flagInfo.start);
        PlayerPrefs.SetString("scoreText", _flagInfo.score);
        PlayerPrefs.SetString("recordText", _flagInfo.record);
        PlayerPrefs.SetString("gameover", _flagInfo.gameover);
        PlayerPrefs.SetString("thanks", _flagInfo.thanks);
        PlayerPrefs.SetString("graphics", _flagInfo.graphics);
        PlayerPrefs.SetString("music", _flagInfo.music);
        PlayerPrefs.SetString("code", _flagInfo.code);
        PlayerPrefs.SetString("tap", _flagInfo.tap);
        PlayerPrefs.SetString("stage", _flagInfo.stage);
        PlayerPrefs.SetString("authText", _flagInfo.authText);
        PlayerPrefs.SetString("yes", _flagInfo.yes);
        PlayerPrefs.SetString("no", _flagInfo.no);
        PlayerPrefs.SetString("error", _flagInfo.error);
        textManager.UpdateUI(frontButton);
    }
    
    private void OnFlagButtonClicked(FlagButton button)
    {
        if(frontButton.Id == button.Id)
            return;
        
        foreach (var flag in flags)
        {
            if (flag.id == button.Id)
            {
                _flagInfo = flag;
            }
        }
        
        button.ButtonImage = frontButton.ButtonImage;
        button.Id = frontButton.Id;
        
        frontButton.ButtonImage = _flagInfo.flagSprite;
        frontButton.Id = _flagInfo.id;
        SetPrefs();
    }
}
