using UnityEngine;

public class GameLogic
{
    public BlockController _blockController; // Block�� ó���� ��ü
    private Constants.PlayerType[,] _board; // ������ ���� ����

    public BasePlayerState firstPlayerState; // Player A
    public BasePlayerState secondPlayerState; // Player B
    public BasePlayerState currentPlayer; // ���� ���� �÷��̾�

    public enum GameResult { None, Win, Lose, Draw }

    public BlockController blockController;

    public GameLogic(BlockController blockController, Constants.GameType gameType)
    {
        _blockController = blockController;

        // ������ ���� ���� �ʱ�ȭ
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount]; // �迭 ũ�� �ʱ�ȭ(3x3)

        // GameType �ʱ�ȭ
        switch (gameType)
        {
            case Constants.GameType.SinglePlay:
                break;

            case Constants.GameType.DualPlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new PlayerState(false);

                // ���� ����
                SetState(firstPlayerState);
                break;

            case Constants.GameType.MultiPlay:
                break;
        }
    }

    public void SetState(BasePlayerState state)
    {
        // ���� �ٲ� �� ���� State�� Exit-> �̹� ���� ���¸� �Ҵ�-> �ٲ� ���� State�� Enter
        currentPlayer?.Exit(this);
        currentPlayer = state;
        currentPlayer?.Enter(this);
    }

    // board �迭�� ���ο� Marker ���� �Ҵ�
    public bool SetNewBoardValue(Constants.PlayerType playerType, int row, int col)
    {
        // Board �ȿ� �̹� Marker�� ������ false
        if (_board[row, col] != Constants.PlayerType.None) return false;

        if (playerType == Constants.PlayerType.PlayerA)
        {
            _board[row, col] = playerType; // �ش� ��ġ�� O ��Ŀ ǥ��
            blockController.PlaceMarker(Block.MarkerType.O, row, col);
            return true;
        }
        else if (playerType == Constants.PlayerType.PlayerB)
        {
            _board[row, col] = playerType; // �ش� ��ġ�� X ��Ŀ ǥ��
            blockController.PlaceMarker(Block.MarkerType.X, row, col);
            return true;
        }

        return false;
    }

    // ������ ��� Ȯ��
    public GameResult CheckGameResult()
    {
        if (CheckGameWin(Constants.PlayerType.PlayerA, _board)) { return GameResult.Win; }
        if (CheckGameWin(Constants.PlayerType.PlayerB, _board)) { return GameResult.Lose; }
        if (CheckGameDraw(_board)) { return GameResult.Draw; }
        return GameResult.None;
    }

    // ������ Ȯ��
    public bool CheckGameDraw(Constants.PlayerType[,] board)
    {
        for (var row = 0; row < board.GetLength(0); row++)
        {
            for (var col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == Constants.PlayerType.None) return false;
            }
        }

        return true;
    }

    // ���� �¸� Ȯ��
    private bool CheckGameWin(Constants.PlayerType playerType, Constants.PlayerType[,] board)
    {
        // Col üũ �� ���ڸ� True
        for (var row = 0; row < board.GetLength(0); row++)
        {
            if (board[row, 0] == playerType &&
                board[row, 1] == playerType &&
                board[row, 2] == playerType)
            {
                return true;
            }
        }
        // Row üũ �� ���ڸ� True
        for (var col = 0; col < board.GetLength(1); col++)
        {
            if (board[0, col] == playerType &&
                board[0, col] == playerType &&
                board[0, col] == playerType)
            {
                return true;
            }
        }

        // �밢�� ���ڸ� True
        if ((board[0, 0] == playerType && board[1, 1] == playerType && board[2, 2] == playerType) ||
            board[0, 2] == playerType && board[1, 1] == playerType && board[2, 0] == playerType)
        {
            return true;
        }

        return false;
    }

    public void EndGame(GameResult gameResult)
    {
        SetState(null);
        firstPlayerState = null;
        secondPlayerState = null;

        // �������� Game Over ǥ��
        Debug.Log("Game Over");
    }
}

