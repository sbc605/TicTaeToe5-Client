using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{   
    private Constants.GameType _gameType;
    [SerializeField] private GameObject confirmPanel;

    // Panel�� ���� ���� Canavas ����
    private Canvas canvas;



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

    public void OpenConfirmPanel(string message)
    {
        if (canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>().Show(message);
        }
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        canvas = FindFirstObjectByType<Canvas>();
    }
}
