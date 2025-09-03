using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    #region 멤버변수
    [SerializeField] private Sprite oSprite;
    [SerializeField] private Sprite xSprite;
    [SerializeField] private SpriteRenderer markerSR; // o,x 색상 변경용

    private SpriteRenderer blockBgSr; // Block(배경)의 색상 변경용
    private Color defaultBlockColor;

    public enum MarkerType { None, O, X }

    private int _blockIndex;

    public delegate void OnBlockClicked(int index);
    private OnBlockClicked _onBlockClicked;
    #endregion

    private void Awake()
    {
        blockBgSr = GetComponent<SpriteRenderer>();
        defaultBlockColor = blockBgSr.color;
    }

    // 1.초기화
    public void InitMarker(int blockIndex, OnBlockClicked onBlockClicked)
    {
        _blockIndex = blockIndex;
        SetMarker(MarkerType.None);
        SetBlockColor(defaultBlockColor);
        _onBlockClicked = onBlockClicked;
    }

    // 2.마커 설정
    public void SetMarker(MarkerType markerType)
    {
        switch (markerType)
        {
            case MarkerType.None:
                markerSR.sprite = null;
                break;
            case MarkerType.O:
                markerSR.sprite = oSprite;
                break;
            case MarkerType.X:
                markerSR.sprite = xSprite;
                break;
        }
    }

    // 3.Block 배경 색상 변경
    public void SetBlockColor(Color color)
    {
        blockBgSr.color = color;
    }

    // 4.블럭 터치 처리
    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Debug.Log("Selected Block: " + _blockIndex);

        _onBlockClicked?.Invoke(_blockIndex); // 특정 인덱스 클릭됐다고 델리게이트에 전달
    }
}
