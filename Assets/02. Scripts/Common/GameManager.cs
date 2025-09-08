using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private Constants.GameType _gameType;
    [SerializeField] private GameObject confirmPanel;
    [SerializeField] private GameObject signinPanel;
    [SerializeField] private GameObject signupPanel;

    // Panel을 띄우기 위한 Canavas 정보
    private Canvas canvas;

    private GameLogic gameLogic;
    private GameUIController gameUIController;


    private void Start()
    {
        var sid = PlayerPrefs.GetString("sid");
        if (string.IsNullOrEmpty(sid))
        {
            OpenSigninPanel();
        }
    }

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

    public void OpenSigninPanel() // 로그인 팝업 표시
    {
        if (canvas != null)
        {
            var signinPanelObject = Instantiate(signinPanel, canvas.transform);
            signinPanelObject.GetComponent<SigninPanelController>().Show();
        }
    }

    public void OpenSignupPanel() // 회원가입 팝업 표시
    {
        if (canvas != null)
        {
            var signupPanelObject = Instantiate(signupPanel, canvas.transform);
            signupPanelObject.GetComponent<SignupPanelController>().Show();
        }
    }

    // 게임 씬에서 턴 표시하는 UI 제어
    public void SetGameTurnPanel(GameUIController.GameTurnPanelType turnPanelType)
    {
        gameUIController.SetGameTurnPanel(turnPanelType);
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game")
        {
            // Block 초기화
            var blockConteroller = FindFirstObjectByType<BlockController>();
            if (blockConteroller != null)
            {
                blockConteroller.InitBlocks();
            }

            // GameUIController 할당 및 초기화
            gameUIController = FindFirstObjectByType<GameUIController>();
            if (gameUIController != null)
            {
                gameUIController.SetGameTurnPanel(GameUIController.GameTurnPanelType.None);
            }

            // GameLogic 생성
            if (gameLogic != null)
            {
                // 기존 게임로직 소멸 -> 새로 생성
            }

            gameLogic = new GameLogic(blockConteroller, _gameType);
        }
    }
}
