using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject playerA_Panel;
    [SerializeField] private GameObject playerB_Panel;

    public enum GameTurnPanelType { None, ATurn, BTurn }

    public void OnClickBackButton()
    {
        GameManager.Instance.OpenConfirmPanel("������ �����Ͻðڽ��ϱ�?", () =>
        {
            GameManager.Instance.ChangeToMainScene(); // �÷��̾ Ȯ���� ������ ����
        });
    }

    public void SetGameTurnPanel(GameTurnPanelType turnPanel)
    {
        switch (turnPanel)
        {
            case GameTurnPanelType.None:
                playerA_Panel.SetActive(false);
                playerB_Panel.SetActive(false);
                break;

            case GameTurnPanelType.ATurn:
                playerA_Panel.SetActive(true);
                playerB_Panel.SetActive(false);
                break;

            case GameTurnPanelType.BTurn:
                playerA_Panel.SetActive(false);
                playerB_Panel.SetActive(true);
                break;
        }
    }
}
