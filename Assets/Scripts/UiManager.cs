using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject tapText;
    public GameObject newGame;
    public Text score;
    public Text highScoreStart;
    public Text highScoreEnd;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        highScoreStart.text = $"High Score: {ScoreManager.instance.highScore}";
    }

    public void GameStart()
    {
        tapText.SetActive(false);
        startPanel.GetComponent<Animator>().Play("PanelUp");
    }

    public void GameOver()
    {
        score.text = $"Score: {ScoreManager.instance.score}";
        highScoreEnd.text = $"High Score: {ScoreManager.instance.highScore}";
        gameOverPanel.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }
}
