using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YandexNamespace;

public class UIController : MonoBehaviour
{
    public int score;
    [SerializeField] private Text text;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private GameObject scoreLabel;
    [SerializeField] private Text timerLabel;
    [SerializeField] private GameObject adsButton;
    [SerializeField] private Text stageText;
    
    [SerializeField] private GameObject movingJoystick;
    [SerializeField] private GameObject aimJoystick;
    
    [SerializeField] private GameObject _player;
    
    public static event Action Restart;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private float volume = 0f;

    private bool isDesktop;

    private bool isAdvShown;
    
    public int Score
    {
        get
        {
            return score;
        }
        private set
        {
            score = value;
            text.text = PlayerPrefs.GetString("scoreText") + ": " + score;
            PlayerPrefs.SetInt("score", score);
        }
    }
    
    private void OnEnable()
    {
        CharacterActions.ScoreUp += UpdateScore;
        PlayerInput.PlayerDead += ShowGameOverWindow;
        Yandex.AdvDone += Revive;
        Spawner.NewStage += SpawnerOnNewStage;
    }

    private void Start()
    {
        isAdvShown = false;
        Score = 0;
        isDesktop = PlayerPrefs.GetString("device") == "desktop";
        if (isDesktop)
        {
            movingJoystick.SetActive(false);
            aimJoystick.SetActive(false);
        }

        volume = soundSlider.value;
    }

    private void OnDestroy()
    {
        CharacterActions.ScoreUp -= UpdateScore;
        PlayerInput.PlayerDead -= ShowGameOverWindow;
        Yandex.AdvDone -= Revive;
        Spawner.NewStage -= SpawnerOnNewStage;
    }

    private void UpdateScore()
    {
        Score++;
    }

    public void OnPlayButtonClicked()
    {
        Yandex.GameplayStart();
        
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseButton.SetActive(true);
        
        scoreLabel.SetActive(true);
        Time.timeScale = 1f;

        if (isDesktop) return;
        movingJoystick.SetActive(true);
        aimJoystick.SetActive(true);
    }

    public void OnRestartButtonClicked()
    {
        Restart?.Invoke();
        Score = 0;
        OnPlayButtonClicked();
    }
    
    public void OnAdsButtonClicked()
    {
        mixer.SetFloat("MasterVolume", -80);
        Yandex.ShowAdv();
        Time.timeScale = 0f;
        isAdvShown = true;
    }

    private void Revive(bool isRewarded)
    {
        if (isRewarded)
        {
            _player.gameObject.SetActive(true);
            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            pauseButton.SetActive(true);
            scoreLabel.SetActive(true);
            if (!isDesktop)
            {
                movingJoystick.SetActive(true);
                aimJoystick.SetActive(true);
            }

            StartCoroutine(ReviveDelay());
        }
        else
        {
            Time.timeScale = 1f;
            mixer.SetFloat("MasterVolume", volume);
        }
    }

    private IEnumerator ReviveDelay()
    {
        mixer.SetFloat("MasterVolume", volume);
        timerLabel.text = "3";
        timerLabel.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        timerLabel.text = "2";
        yield return new WaitForSecondsRealtime(1);
        timerLabel.text = "1";
        yield return new WaitForSecondsRealtime(1);
        timerLabel.gameObject.SetActive(false);
        
        Time.timeScale = 1f;
        Yandex.GameplayStart();
    }

    public void OnPauseButtonClicked()
    {
        Yandex.GameplayStop();
        
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);

        if (isDesktop) return;
        movingJoystick.SetActive(false);
        aimJoystick.SetActive(false);
    }

    public void OnMenuButtonClicked()
    {
        Yandex.GameplayStop();
        
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ShowGameOverWindow()
    {
        Yandex.GameplayStop();
        
        pauseButton.SetActive(false);
        scoreLabel.SetActive(false);
        gameOverMenu.SetActive(true);
        if (isAdvShown)
        {
            adsButton.SetActive(false);
            isAdvShown = false;
        }
        else
            adsButton.SetActive(true);

        if (isDesktop) return;
        movingJoystick.SetActive(false);
        aimJoystick.SetActive(false);
    }

    public void OnSoundToggleClicked(bool state)
    {
        if(state)
        {
            soundSlider.value = -80f;
        }
        else
        {
            if ((int)soundSlider.value == -40) soundSlider.value = volume;
        }
    }

    public void OnSoundSliderChanged(System.Single value)
    {
        mixer.SetFloat("MasterVolume", value);
        if ((int)value <= -40)
        {
            mixer.SetFloat("MasterVolume", -80);
            soundToggle.isOn = true;
        }
        else
        {
            volume = value;
            soundToggle.isOn = false;
        }
    }
    
    private void SpawnerOnNewStage(int stage)
    {
        stageText.gameObject.SetActive(true);
        stageText.text = PlayerPrefs.GetString("stage") + ": " + stage;
        StartCoroutine(StageLabelOff());
    }

    private IEnumerator StageLabelOff()
    {
        yield return new WaitForSeconds(1.3f);
        stageText.gameObject.SetActive(false);
    }
}
