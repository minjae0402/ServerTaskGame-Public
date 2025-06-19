using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class ImageDisplay : MonoBehaviour
{
    public RawImage imageDisplay;

    void Update()
    {
        StartCoroutine(GetImageFromServer());
    }

    IEnumerator GetImageFromServer()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/latest-image/");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            ImageURL imageInfo = JsonUtility.FromJson<ImageURL>(json);

            if (!string.IsNullOrEmpty(imageInfo.url))
            {
                StartCoroutine(DownloadImage(imageInfo.url));
            }
            else
            {
                Debug.Log("업로드된 이미지가 없음.");
            }
        }
        else
        {
            Debug.Log("이미지 URL 가져오기 실패");
        }
    }

    IEnumerator DownloadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            imageDisplay.texture = tex;
        }
        else
        {
            Debug.Log("이미지 다운로드 실패");
        }
    }

    [System.Serializable]
    public class ImageURL
    {
        public string url;
    }
}
