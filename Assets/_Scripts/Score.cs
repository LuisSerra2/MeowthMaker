using UnityEngine;

public class Score
{
    public int score;
    public int highScore;

    public Score(int score, int highScore)
    {
        this.score = score;
        this.highScore = highScore;
    }

    public void AddScore(int addScore)
    {
        score += addScore;
    }

    public void RemoveScore(int removeScore)
    {
        score -= removeScore;
    }

    public void Save(string scoreKey, string highScoreKey)
    {
        PlayerPrefs.SetInt(scoreKey, score);
        PlayerPrefs.SetInt(highScoreKey, highScore);
        PlayerPrefs.Save();
    }

    public void HighScored(string highScoreKey)
    {
        if (highScore <= score)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
        }
    }
}
