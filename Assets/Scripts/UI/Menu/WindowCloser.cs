using UnityEngine;
using UnityEngine.UI;

public class WindowCloser : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject window;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    
    private void OnEnable()
    {
        button.onClick.AddListener(Close);
    }
    
    private void OnDisable()
    {
        button.onClick.RemoveListener(Close);
    }

    private void Close()
    {
        window.SetActive(false);
    }
}
