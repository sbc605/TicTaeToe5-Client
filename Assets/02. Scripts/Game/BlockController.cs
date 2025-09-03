using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Block[] blocks;

    public delegate void OnBlockClicked(int row, int col);
    public OnBlockClicked OnBlockClickedDelegate;

    // 1.모든 Block 초기화
    public void InitBlocks()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].InitMarker(i, blockIndex =>
            {
                // 클릭된 블럭에 대한 처리
                var row = blockIndex / Constants.BlockColumnCount;
                var col = blockIndex % Constants.BlockColumnCount;

                OnBlockClickedDelegate?.Invoke(row, col);
            });
        }
    }

    // 2.특정 Block에 마커 표시
    public void PlaceMarker(Block.MarkerType markerType, int row, int col)
    {
        // row, col >> index 변환
        var blockIndex = row * Constants.BlockColumnCount + col;
        blocks[blockIndex].SetMarker(markerType);
    }

    // 3.승패에 따라 Block의 배경 색 변경
    public void ChangeBlockColor()
    {
        // TODO : 게임 로직이 완성되면 구현
    }
}
