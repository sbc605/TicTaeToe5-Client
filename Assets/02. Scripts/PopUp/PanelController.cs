using DG.Tweening;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]

public class PanelController : MonoBehaviour
{
    private CanvasGroup bg_canvasGroup;

    [SerializeField] private RectTransform panelRectTransform;

    // 패널이 Hide 될 때 해야할 동작 전달받을 delegate
    public delegate void PanelControllerHideDelegate();

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

    public void Hide(PanelControllerHideDelegate hideDelegate = null)
    {
        bg_canvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;

        bg_canvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            hideDelegate?.Invoke();
            Destroy(gameObject); // 창이 필요없을 때 파괴
        });
    }

    protected void Shake()
    {
        panelRectTransform.DOShakeAnchorPos(0.3f);
    }
}
