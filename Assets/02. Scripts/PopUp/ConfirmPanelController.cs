using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TMP_Text messageText;

    public void Show(string message)
    {
        messageText.text = message;
        base.Show(); // �г��� ȭ�鿡 �����ϴ� ���
    }

    public void OnClickConfirmButton()
    {
        GameManager.Instance.ChangeToMainScene();
    }

    public void OnClickCloseButton()
    {
        Hide();
    }
}
