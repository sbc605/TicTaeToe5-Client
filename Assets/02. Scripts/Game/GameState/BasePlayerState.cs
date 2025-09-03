using UnityEngine;

public abstract class BasePlayerState
{
    public abstract void Enter(GameLogic gameLogic); // ���� ����
    public abstract void Exit(GameLogic gameLogic); //���� ����
    public abstract void HandleMove(GameLogic gameLogic, int row, int col); // ��Ŀ ǥ��
    protected abstract void HandleNextTurn(GameLogic gameLogic); // �� ��ȯ

    // ���� ��� ó��
    protected void ProcessMove(GameLogic gameLogic, Constants.PlayerType playerType, int row, int col) // ��Ŀ ǥ���� �� ȣ��Ǵ� �Լ�
    {
        if (gameLogic.SetNewBoardValue(playerType, row, col))
        {
            // ���Ӱ� ���� ��Ŀ ������� ���� ��� �Ǵ�
            var gameResult = gameLogic.CheckGameResult();

            if (gameResult == GameLogic.GameResult.None)
            {
                HandleNextTurn(gameLogic);
            }
            else
            {
                gameLogic.EndGame(gameResult);
            }
        }
    }
}
