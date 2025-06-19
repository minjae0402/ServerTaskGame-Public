using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class CounterManager : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    void Start()
    {
        StartCoroutine(FetchCounter());
    }

    public void OnIncreaseButtonClicked()
    {
        StartCoroutine(IncrementCounter());
    }

    IEnumerator FetchCounter()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/counter/");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            CounterData data = JsonUtility.FromJson<CounterData>(www.downloadHandler.text);
            counterText.text = $"현재 값: {data.value}";
        }
        else
        {
            Debug.Log("숫자 가져오기 실패");
        }
    }

    IEnumerator IncrementCounter()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/counter/increment");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            CounterData data = JsonUtility.FromJson<CounterData>(www.downloadHandler.text);
            counterText.text = $"현재 값: {data.value}";
        }
        else
        {
            Debug.Log("증가 실패");
        }
    }

    [System.Serializable]
    public class CounterData
    {
        public int value;
    }
}
