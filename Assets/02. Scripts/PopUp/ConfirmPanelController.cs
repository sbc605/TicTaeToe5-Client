using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TMP_Text messageText;
    public delegate void OnConfirmButtonClicked(); // Ȯ�� ��ư Ŭ���� ȣ��Ǵ� �޼���
    private OnConfirmButtonClicked _onConfirmButtonClicked;

    public void Show(string message, OnConfirmButtonClicked onConfirmButtonClicked)
    {
        messageText.text = message;
        _onConfirmButtonClicked = onConfirmButtonClicked;
        base.Show(); // �г��� ȭ�鿡 �����ϴ� ���
    }

    public void OnClickConfirmButton()
    {
        Hide(() =>
        {
            _onConfirmButtonClicked?.Invoke();
        });

        // GameManager.Instance.ChangeToMainScene();
    }

    public void OnClickCloseButton()
    {
        Hide();
    }
}
