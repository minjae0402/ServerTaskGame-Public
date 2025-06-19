using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Reset : MonoBehaviour
{
    public void OnResetButtonClicked()
    {
        StartCoroutine(ResetReward());
    }

    IEnumerator ResetReward()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/reset/");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("초기화 완료 : " + www.downloadHandler.text);
        }
        else
        {
            Debug.Log("초기화 실패 : " + www.error);
        }
    }
}
