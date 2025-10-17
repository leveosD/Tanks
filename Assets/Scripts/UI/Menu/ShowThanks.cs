using UnityEngine;
using UnityEngine.UI;

public class ShowThanks : MonoBehaviour
{
    [SerializeField] private Canvas menu;
    [SerializeField] private Canvas thanks;

    private Button button;
    
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnThanksButtonCLicked);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnThanksButtonCLicked);
    }

    private void OnThanksButtonCLicked()
    {
        if (menu.isActiveAndEnabled)
        {
            menu.gameObject.SetActive(false);
            thanks.gameObject.SetActive(true);
        }
        else
        {
            menu.gameObject.SetActive(true);
            thanks.gameObject.SetActive(false);
        }
    }
}
