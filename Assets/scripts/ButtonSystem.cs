using UnityEngine;

public class ButtonSystem : MonoBehaviour
{
    public GameObject Btn;

    public void BtnClick()
    {
        Btn.SetActive(false);
    }
}
