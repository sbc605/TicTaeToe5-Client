using UnityEngine;

public class GameLogic
{
    public BlockController blockController; // Block�� ó���� ��ü
    private Constants.PlayerType[,] _board; // ������ ���� ����

    public BasePlayerState firstPlayerState; // Player A
    public BasePlayerState secondPlayerState; // Player B
    public BasePlayerState currentPlayer; // ���� ���� �÷��̾�

    public enum GameResult { None, Win, Lose, Draw }

    public Constants.PlayerType[,] GetBoard()
    {
        return _board;
    }

    public GameLogic(BlockController blockControl, Constants.GameType gameType)
    {
        blockController = blockControl;

        // ������ ���� ���� �ʱ�ȭ
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount]; // �迭 ũ�� �ʱ�ȭ(3x3)

        // GameType �ʱ�ȭ
        switch (gameType)
        {
            case Constants.GameType.SinglePlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new AIState();

                SetState(firstPlayerState);
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
        if (TicTacToeAI.CheckGameWin(Constants.PlayerType.PlayerA, _board)) { return GameResult.Win; }
        if (TicTacToeAI.CheckGameWin(Constants.PlayerType.PlayerB, _board)) { return GameResult.Lose; }
        if (TicTacToeAI.CheckGameDraw(_board)) { return GameResult.Draw; }
        return GameResult.None;
    }

    public void EndGame(GameResult gameResult)
    {
        SetState(null);
        firstPlayerState = null;
        secondPlayerState = null;

        GameManager.Instance.OpenConfirmPanel("���ӿ���", () =>
        {
            GameManager.Instance.ChangeToMainScene();
        });
    }
}

