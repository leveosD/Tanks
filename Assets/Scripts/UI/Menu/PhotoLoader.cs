using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using YandexNamespace;

public class PhotoLoader : MonoBehaviour
{
    private Image photo;
    private void OnEnable()
    {
        photo = GetComponent<Image>();
        LoadPhoto();
        Yandex.LoadPhoto += LoadPhoto;
    }

    private void OnDisable()
    {
        Yandex.LoadPhoto -= LoadPhoto;
    }

    private void LoadPhoto()
    {
        string url = PlayerPrefs.GetString("imageUrl");
        Debug.Log("Photo Loasder. LoadPhoto. URL: " + url);
        if (url == "" || url == "error")
        {
            return;
        }
        StartCoroutine(DownloadImage(url));
        Destroy(GetComponentInParent<Button>());
    }
    
    private IEnumerator DownloadImage(string imageUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Image downloading error");
        }
        else
        {
            Debug.Log(imageUrl);
            var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            photo.sprite = Sprite.Create(texture, new Rect(0, 0, 80, 80), Vector2.zero);
        }
    }
}
