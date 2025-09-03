using UnityEngine;

public class PlayerState : BasePlayerState
{
    private bool isFirst;
    private Constants.PlayerType playerType;

    public PlayerState(bool isFirstPlayer) // 1�� �÷��̾�� 2�� �÷��̾��
    {
        isFirst = isFirstPlayer;
        playerType = isFirst ? Constants.PlayerType.PlayerA : Constants.PlayerType.PlayerB;
    }

    #region �ʼ��޼���
    public override void Enter(GameLogic gameLogic)
    {
        // 1.First Player���� Ȯ���ؼ� UI�� ���� �� ǥ��

        // 2.BlockController�� delegate�� �ؾ��� �� ����(��ġ���� �� ����� ���)
        gameLogic._blockController.OnBlockClickedDelegate = (row, col) =>
        {
            // ����� ��ġ �� ������ ��ٷȴٰ� ��ġ �Ǹ� ó���� ��
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
            // ���� �������� Second �÷��̾� Ȱ��ȭ
        }
        else
        {
            // ���� �������� First �÷��̾� Ȱ��ȭ
        }
    }
    #endregion
}
