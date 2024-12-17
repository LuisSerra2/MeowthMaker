using UnityEngine;

public class Score
{
    public int CurrentScore { get; private set; }
    public int HighScore { get; private set; }

    public Score(int currentScore, int highScore)
    {
        CurrentScore = currentScore;
        HighScore = highScore;
    }

    public void AddScore(int points)
    {
        CurrentScore += points;
    }

    public bool CheckAndUpdateHighScore()
    {
        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            Save("Score", "HighScore");
            return true;
        }
        return false;
    }

    public void Save(string scoreKey, string highScoreKey)
    {
        PlayerPrefs.SetInt(scoreKey, CurrentScore);
        PlayerPrefs.SetInt(highScoreKey, HighScore);
        PlayerPrefs.Save();
    }

    public void Load(string scoreKey, string highScoreKey)
    {
        CurrentScore = PlayerPrefs.GetInt(scoreKey, 0);
        HighScore = PlayerPrefs.GetInt(highScoreKey, 0);
    }
}
