using System;
using System.Collections;
using UnityEngine;
using YandexNamespace;

public class StartLoader : MonoBehaviour
{
    [SerializeField] private TextManager textManager;
    
    private void Awake()
    {
        var otherObject = FindObjectOfType<StartLoader>();
        if (otherObject != null)
        {
            if(otherObject != this)
                Destroy(this.gameObject);
            else
            {
                StartCoroutine(WaitLoading());
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
    
    private IEnumerator WaitLoading()
    {
        while (Yandex.IsReady != true)
        {
            Yandex.Ready();
            yield return new WaitForSeconds(0.5f);
        }
    }

    /*private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            
        }
    }*/
}
