using UnityEngine;

public class MultiplayState : BasePlayerState
{
    private Constants.PlayerType playerType;
    private bool isFirstPlayer;
    public MultiplayController multiplayController;

    public MultiplayState(bool isFirst, MultiplayController multiController)
    {
        isFirstPlayer = isFirst;
        multiplayController = multiController;
        playerType = isFirstPlayer ? Constants.PlayerType.PlayerA : Constants.PlayerType.PlayerB;
    }

    public override void Enter(GameLogic gameLogic)
    {
        multiplayController.blockDataChanged = blockIndex =>
        {
            var row = blockIndex / Constants.BlockColumnCount;
            var col = blockIndex % Constants.BlockColumnCount;
            UnityThread.executeInUpdate(() =>
            {
                HandleMove(gameLogic, row, col);
            });
        };
    }

    public override void Exit(GameLogic gameLogic)
    {
        multiplayController.blockDataChanged = null;
    }

    public override void HandleMove(GameLogic gameLogic, int row, int col)
    {
        ProcessMove(gameLogic, playerType, row, col);
    }

    protected override void HandleNextTurn(GameLogic gameLogic)
    {
        if (isFirstPlayer)
            gameLogic.SetState(gameLogic.secondPlayerState);

        else
            gameLogic.SetState(gameLogic.firstPlayerState);

    }
}
