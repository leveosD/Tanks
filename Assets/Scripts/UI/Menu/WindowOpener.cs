using System;
using UnityEngine;
using UnityEngine.UI;

public class WindowOpener : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject window;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(Open);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(Open);
    }
    
    private void Open()
    {
        window.SetActive(true);
    }
}