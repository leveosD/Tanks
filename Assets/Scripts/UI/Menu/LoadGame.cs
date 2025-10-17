using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YandexNamespace;

public class LoadGame : MonoBehaviour
{
    private Button button;
    private AsyncOperation asyncLoad; 

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnStartButtonCLicked);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnStartButtonCLicked);
    }

    private void OnStartButtonCLicked()
    {
        Yandex.GameplayStart();
        
        SceneManager.LoadScene(1);
    }
}
