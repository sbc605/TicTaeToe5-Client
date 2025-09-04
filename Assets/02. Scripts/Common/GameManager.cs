using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private Constants.GameType _gameType;
    [SerializeField] private GameObject confirmPanel;

    // Panel�� ���� ���� Canavas ����
    private Canvas canvas;

    private GameLogic gameLogic;
    private GameUIController gameUIController;


    /// <summary>
    /// Main���� Game Scene���� ��ȯ�� ȣ��� �޼���
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

    // ���� ������ �� ǥ���ϴ� UI ����
    public void SetGameTurnPanel(GameUIController.GameTurnPanelType turnPanelType)
    {
        gameUIController.SetGameTurnPanel(turnPanelType);
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game")
        {
            // Block �ʱ�ȭ
            var blockConteroller = FindFirstObjectByType<BlockController>();
            if (blockConteroller != null)
            {
                blockConteroller.InitBlocks();
            }

            // GameUIController �Ҵ� �� �ʱ�ȭ
            gameUIController = FindFirstObjectByType<GameUIController>();
            if (gameUIController != null)
            {
                gameUIController.SetGameTurnPanel(GameUIController.GameTurnPanelType.None);
            }

            // GameLogic ����
            if (gameLogic != null)
            {
                // ���� ���ӷ��� �Ҹ� -> ���� ����
            }

            gameLogic = new GameLogic(blockConteroller, _gameType);
        }
    }
}
