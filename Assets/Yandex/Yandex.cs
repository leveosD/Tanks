using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace YandexNamespace
{
    public struct RecordStruct
    {
        public int record;
    }
    
    public class Yandex: MonoBehaviour
    {
        private static bool _isReady;

        public static bool IsReady => _isReady;

        [DllImport("__Internal")]
        public static extern void Ready();
        
        [DllImport("__Internal")]
        public static extern void GetPlayerInfo();
    
        [DllImport("__Internal")]
        public static extern void RateGame();
    
        [DllImport("__Internal")]
        public static extern void SaveRecord(int score);
    
        [DllImport("__Internal")]
        public static extern void LoadRecord();
    
        [DllImport("__Internal")]
        public static extern void UpdateResolution();
        
        [DllImport("__Internal")]
        public static extern void ShowAdv();
        
        [DllImport("__Internal")]
        public static extern void ShowStartAdv();
        
        [DllImport("__Internal")]
        public static extern void GetLanguage();
        
        [DllImport("__Internal")]
        public static extern void GetPhoto();
        
        [DllImport("__Internal")]
        public static extern void GameplayStart();

        [DllImport("__Internal")]
        public static extern void GameplayStop();
        
        [DllImport("__Internal")] 
        public static extern void Authentication();

        private static RecordStruct _record;

        public static event Action<bool> AdvDone;
        public static event Action LoadPhoto;
        public static event Action UpdateLanguage;

        private bool isRewarded = false;

        public static int Record => _record.record;
        
        private void OnEnable()
        {
            PlayerInput.PlayerDead += UpdateRecord;
        }
        private void OnDestroy()
        {
            PlayerInput.PlayerDead -= UpdateRecord;
        }

        public void SetDeviceType(string device)
        {
            PlayerPrefs.SetString("device", device);
        }
    
        private void UpdateRecord()
        {
            int score = PlayerPrefs.GetInt("score") - 1;
            if (score > Record)
            {
                SaveRecord(score);
                _record.record = score;
                Debug.Log("Record is saved: " + _record.record);
            }
        }
    
        public void SetRecord(string value)
        {
            _record = JsonUtility.FromJson<RecordStruct>(value);
            Debug.Log("Loaded Record: " + Record);
        }

        public void IsRewarded(int rewarded)
        {
            isRewarded = rewarded == 1;
        }

        public void GetReward()
        {
            AdvDone?.Invoke(isRewarded);
        }

        public void SetLanguage(string lang)
        {
            PlayerPrefs.SetString("lang", lang);
            UpdateLanguage?.Invoke();
        }

        public void SetPhoto(string url)
        {
            PlayerPrefs.SetString("imageUrl", url);
            LoadPhoto?.Invoke();
        }

        public void SetReady()
        {
            _isReady = true;
            GetPlayerInfo();
            LoadRecord();
            GetLanguage();
            GetPhoto();
            ShowStartAdv();
            GameplayStop();
        }
    }
}