using UnityEngine;

public class PlayerState : BasePlayerState
{
    private bool isFirst;
    private Constants.PlayerType playerType;
    private MultiplayController _multiplayController;
    private string _roomId; // roomID 저장
    private bool _isMultiplay; // 멀티플레이 상태인지

    public PlayerState(bool isFirstPlayer) // 1번 플레이어냐 2번 플레이어냐
    {
        isFirst = isFirstPlayer;
        playerType = isFirst ? Constants.PlayerType.PlayerA : Constants.PlayerType.PlayerB;
        _isMultiplay = false;
    }

    public PlayerState(bool isFirstPlayer, MultiplayController multiplayController, string roomId) : this(isFirstPlayer)
    {       
        _multiplayController = multiplayController;
        _roomId = roomId;
        _isMultiplay = true;
    }

    #region 필수메서드
    public override void Enter(GameLogic gameLogic)
    {
        // 1.First Player인지 확인해서 UI에 현재 턴 표시
        if (isFirst)
        {
            GameManager.Instance.SetGameTurnPanel(GameUIController.GameTurnPanelType.ATurn);
        }
        else
        {
            GameManager.Instance.SetGameTurnPanel(GameUIController.GameTurnPanelType.BTurn);
        }
        // 2.BlockController의 delegate에 해야할 일 전달(터치했을 때 실행될 결과)
        gameLogic.blockController.OnBlockClickedDelegate = (row, col) =>
        {
            // 블록이 터치 될 때까지 기다렸다가 터치 되면 처리할 일
            HandleMove(gameLogic, row, col);
        };
    }

    public override void Exit(GameLogic gameLogic)
    {
        gameLogic.blockController.OnBlockClickedDelegate = null;
    }

    public override void HandleMove(GameLogic gameLogic, int row, int col)
    {
        ProcessMove(gameLogic, playerType, row, col);

        if (_isMultiplay) // 서버에 마커 정보 전달
        {
            _multiplayController.DoPlayer(_roomId, row * Constants.BlockColumnCount + col);
        }
    }

    protected override void HandleNextTurn(GameLogic gameLogic)
    {
        if (isFirst)
        {
            gameLogic.SetState(gameLogic.secondPlayerState);
        }
        else
        {
            gameLogic.SetState(gameLogic.firstPlayerState);
        }
    }
    #endregion
}
