using UnityEngine;

public class GameLogic
{
    public BlockController blockController; // Block을 처리할 객체
    private Constants.PlayerType[,] _board; // 보드의 상태 정보

    public BasePlayerState firstPlayerState; // Player A
    public BasePlayerState secondPlayerState; // Player B
    public BasePlayerState currentPlayer; // 현재 턴의 플레이어

    public enum GameResult { None, Win, Lose, Draw }

    public Constants.PlayerType[,] GetBoard()
    {
        return _board;
    }

    public GameLogic(BlockController blockControl, Constants.GameType gameType)
    {
        blockController = blockControl;

        // 보드의 상태 정보 초기화
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount]; // 배열 크기 초기화(3x3)

        // GameType 초기화
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

                // 게임 시작
                SetState(firstPlayerState);
                break;

            case Constants.GameType.MultiPlay:
                break;
        }
    }

    public void SetState(BasePlayerState state)
    {
        // 턴이 바뀔 때 기존 State는 Exit-> 이번 턴의 상태를 할당-> 바뀐 턴의 State로 Enter
        currentPlayer?.Exit(this);
        currentPlayer = state;
        currentPlayer?.Enter(this);
    }


    // board 배열에 새로운 Marker 값을 할당
    public bool SetNewBoardValue(Constants.PlayerType playerType, int row, int col)
    {
        // Board 안에 이미 Marker가 있으면 false
        if (_board[row, col] != Constants.PlayerType.None) return false;

        if (playerType == Constants.PlayerType.PlayerA)
        {
            _board[row, col] = playerType; // 해당 위치에 O 마커 표시
            blockController.PlaceMarker(Block.MarkerType.O, row, col);
            return true;
        }
        else if (playerType == Constants.PlayerType.PlayerB)
        {
            _board[row, col] = playerType; // 해당 위치에 X 마커 표시
            blockController.PlaceMarker(Block.MarkerType.X, row, col);
            return true;
        }

        return false;
    }

    // 게임의 결과 확인
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

        GameManager.Instance.OpenConfirmPanel("게임오버", () =>
        {
            GameManager.Instance.ChangeToMainScene();
        });
    }
}

