using UnityEngine;

public abstract class BasePlayerState
{
    public abstract void Enter(GameLogic gameLogic); // 상태 시작
    public abstract void Exit(GameLogic gameLogic); //상태 종료
    public abstract void HandleMove(GameLogic gameLogic, int row, int col); // 마커 표시
    protected abstract void HandleNextTurn(GameLogic gameLogic); // 턴 전환

    // 게임 결과 처리
    protected void ProcessMove(GameLogic gameLogic, Constants.PlayerType playerType, int row, int col) // 마커 표시할 때 호출되는 함수
    {
        if (gameLogic.SetNewBoardValue(playerType, row, col))
        {
            // 새롭게 놓인 마커 기반으로 게임 결과 판단
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
