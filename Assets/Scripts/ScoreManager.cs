using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static readonly string HIGH_SCORE_KEY = "HighScore";
    public static ScoreManager instance;

    public int score;
    public int highScore;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        LoadHighScore();
    }
    
    private void Start()
    { 
        score = 0;
    }

    public void SaveHighScore()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
            highScore = score;
        }
    }

    public void LoadHighScore()
    {
        if (PlayerPrefs.HasKey(HIGH_SCORE_KEY))
        {
            highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY);
        }
        else
        {
            highScore = 0;
        }
    }

    public void IncrementScore() => score++;
}
