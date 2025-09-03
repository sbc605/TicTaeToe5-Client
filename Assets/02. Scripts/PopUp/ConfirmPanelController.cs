using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TMP_Text messageText;
    public delegate void OnConfirmButtonClicked(); // 확인 버튼 클릭시 호출되는 메서드
    private OnConfirmButtonClicked _onConfirmButtonClicked;

    public void Show(string message, OnConfirmButtonClicked onConfirmButtonClicked)
    {
        messageText.text = message;
        _onConfirmButtonClicked = onConfirmButtonClicked;
        base.Show(); // 패널이 화면에 등장하는 기능
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
