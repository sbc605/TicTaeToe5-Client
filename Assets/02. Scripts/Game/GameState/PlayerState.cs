using UnityEngine;

public class PlayerState : BasePlayerState
{
    private bool isFirst;
    private Constants.PlayerType playerType;

    public PlayerState(bool isFirstPlayer) // 1번 플레이어냐 2번 플레이어냐
    {
        isFirst = isFirstPlayer;
        playerType = isFirst ? Constants.PlayerType.PlayerA : Constants.PlayerType.PlayerB;
    }

    #region 필수메서드
    public override void Enter(GameLogic gameLogic)
    {
        // 1.First Player인지 확인해서 UI에 현재 턴 표시

        // 2.BlockController의 delegate에 해야할 일 전달(터치했을 때 실행될 결과)
        gameLogic._blockController.OnBlockClickedDelegate = (row, col) =>
        {
            // 블록이 터치 될 때까지 기다렸다가 터치 되면 처리할 일
            HandleMove(gameLogic, row, col);
        };
    }

    public override void Exit(GameLogic gameLogic)
    {
        gameLogic._blockController.OnBlockClickedDelegate = null;
    }

    public override void HandleMove(GameLogic gameLogic, int row, int col)
    {
        ProcessMove(gameLogic, playerType, row, col);
    }

    protected override void HandleNextTurn(GameLogic gameLogic)
    {
        if (isFirst)
        {
            // 게임 로직에게 Second 플레이어 활성화
        }
        else
        {
            // 게임 로직에게 First 플레이어 활성화
        }
    }
    #endregion
}
