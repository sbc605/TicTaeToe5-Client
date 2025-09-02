using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public void OnClickBackButton()
    {
        // GameManager.Instance.ChangeToMainScene(); // 팝업이 뜨자마자 즉시 씬이 이동되기 때문에 의미가 없어짐

        GameManager.Instance.OpenConfirmPanel("게임을 종료하시겠습니까?");
    }
}
