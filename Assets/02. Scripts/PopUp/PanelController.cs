using DG.Tweening;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]

public class PanelController : MonoBehaviour
{
    private CanvasGroup bg_canvasGroup;

    [SerializeField] private RectTransform panelRectTransform; // �˾�â RectTransform

    private void Awake()
    {
        bg_canvasGroup = GetComponent<CanvasGroup> ();
    }

    public void Show()
    {
        bg_canvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;

        bg_canvasGroup.DOFade(1, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(1, 0.3f).SetEase (Ease.OutBack);
    }

    public void Hide()
    {
        bg_canvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;

        bg_canvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(0, 0.3f).SetEase(Ease.InBack);
    }
}
