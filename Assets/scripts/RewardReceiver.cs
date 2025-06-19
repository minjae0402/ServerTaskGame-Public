using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RewardReceiver : MonoBehaviour
{
    public GameObject Btn;
    public GameObject Reward_Item;

    public Animator feedbackAnimator;

    public void GetRewardFromServer()
    {
        StartCoroutine(CheckReward());
    }

    IEnumerator CheckReward()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/reward/");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            RewardResponse res = JsonUtility.FromJson<RewardResponse>(json);

            if (res.status == "rewarded")
            {
                Debug.Log("보상 수령 완료!");
                Btn.SetActive(false);
                Reward_Item.SetActive(true);
            }
            else if (res.status == "not_allowed")
            {
                Debug.Log("보상 권한 없음");
                feedbackAnimator.SetTrigger("RequestFailed");
            }
        }
        else
        {
            Debug.Log("요청 실패: " + www.error);
            feedbackAnimator.SetTrigger("RequestFailed");
        }
    }

    [System.Serializable]
    public class RewardResponse
    {
        public string status;
        public string message;
    }
}
