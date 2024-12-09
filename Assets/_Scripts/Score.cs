using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    public int score;
    public Score(int score)
    {
        this.score = score;
    }

    public void AddScore(int addScore)
    {
        score += addScore;
    }
    public void RemoveScore(int removeScore)
    {
        score -= removeScore;
    }

    public void Save(string name)
    {
        PlayerPrefs.SetInt(name, score);
        PlayerPrefs.Save();
    }
}
