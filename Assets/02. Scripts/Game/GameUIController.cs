using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public void OnClickBackButton()
    {
        // GameManager.Instance.ChangeToMainScene(); // �˾��� ���ڸ��� ��� ���� �̵��Ǳ� ������ �ǹ̰� ������

        GameManager.Instance.OpenConfirmPanel("������ �����Ͻðڽ��ϱ�?");
    }
}
