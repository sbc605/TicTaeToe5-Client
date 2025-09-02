using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{   
    private Constants.GameType _gameType;


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

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {

    }
}
