using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{   
    private Constants.GameType _gameType;
    [SerializeField] private GameObject confirmPanel;

    // Panel을 띄우기 위한 Canavas 정보
    private Canvas canvas;

    private GameLogic gameLogic;


    /// <summary>
    /// Main에서 Game Scene으로 전환시 호출될 메서드
    /// </summary>
    public void ChangeToGameScene(Constants.GameType gameType)
    {
        _gameType = gameType;
        // 0: single, 1: Dual, 2: Multi
        SceneManager.LoadScene("Game");
    }

    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked)
    {
        if (canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>().Show(message, onConfirmButtonClicked);
        }
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game")
        {
            // Block 초기화
            var blockConteroller = FindFirstObjectByType<BlockController>();
            blockConteroller.InitBlocks();

            // GameLogic 생성
            if (gameLogic != null)
            {
                // 기존 게임로직 소멸 -> 새로 생성
            }

            gameLogic = new GameLogic(blockConteroller, _gameType);
        }
    }
}
