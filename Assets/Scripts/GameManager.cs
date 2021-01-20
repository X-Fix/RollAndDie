using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;
    public GameMode gameMode;

    private PlatformSpawner platformSpawner;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        ResetGame();
    }

    private void Start()
    {
        platformSpawner = GameObject.Find("PlatformSpawner").GetComponent<PlatformSpawner>();
    }

    public void ResetGame()
    {
        gameState = GameState.Idle;
        gameMode = GameMode.WarmUp;
    }

    public void StartGame()
    {
        gameState = GameState.Started;

        UiManager.instance.GameStart();
        platformSpawner.StartSpawningPlatforms();
    }

    public void EndGame()
    {
        gameState = GameState.End;

        platformSpawner.StopSpawningPlatforms();
        ScoreManager.instance.SaveHighScore();
        UiManager.instance.GameOver();
    }

    public void SpeedUp()
    {
        if (gameMode == GameMode.WarmUp)
            gameMode = GameMode.Normal;
        else if (gameMode == GameMode.Normal)
            gameMode = GameMode.Hard;
    }
}
