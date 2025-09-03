using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    #region �������
    [SerializeField] private Sprite oSprite;
    [SerializeField] private Sprite xSprite;
    [SerializeField] private SpriteRenderer markerSR; // o,x ���� �����

    private SpriteRenderer blockBgSr; // Block(���)�� ���� �����
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

    // 1.�ʱ�ȭ
    public void InitMarker(int blockIndex, OnBlockClicked onBlockClicked)
    {
        _blockIndex = blockIndex;
        SetMarker(MarkerType.None);
        SetBlockColor(defaultBlockColor);
        _onBlockClicked = onBlockClicked;
    }

    // 2.��Ŀ ����
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

    // 3.Block ��� ���� ����
    public void SetBlockColor(Color color)
    {
        blockBgSr.color = color;
    }

    // 4.�� ��ġ ó��
    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Debug.Log("Selected Block: " + _blockIndex);

        _onBlockClicked?.Invoke(_blockIndex); // Ư�� �ε��� Ŭ���ƴٰ� ��������Ʈ�� ����
    }
}
