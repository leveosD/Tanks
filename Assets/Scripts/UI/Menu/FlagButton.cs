using System;
using UnityEngine;
using UnityEngine.UI;

public class FlagButton : MonoBehaviour
{
    private Button button;

    public Sprite ButtonImage
    {
        get => button.image.sprite;
        set
        {
            if(button)
                button.image.sprite = value;
        }
    }

    [SerializeField] private string id;
    public string Id
    {
        get => id;
        set => id = value;
    }

    public static event Action<FlagButton> LanguageUpdate;
    
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        LanguageUpdate?.Invoke(this);
    }
}
