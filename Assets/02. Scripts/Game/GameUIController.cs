using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public void OnClickBackButton()
    {
        GameManager.Instance.OpenConfirmPanel("������ �����Ͻðڽ��ϱ�?", () =>
        {
            GameManager.Instance.ChangeToMainScene(); // �÷��̾ Ȯ���� ������ ����
        });
    }
}
