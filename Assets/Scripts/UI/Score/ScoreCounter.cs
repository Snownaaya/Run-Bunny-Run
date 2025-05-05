using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreCounter", menuName = "SO/ScoreCounter", order = 1)]
public class ScoreCounter : ScriptableObject, IScoreCounter
{
    private const string BestScoreKey = "BestScore";
    private const string CurrentScoreKey = "CurrentScore";

    public event Action<int> ScoreChanged;

    public int BestScore => PlayerPrefs.GetInt(BestScoreKey, 0);
    public int Score { get; private set; }

    public void IncrementScore()
    {
        Score++;
        SaveCurrentScore();
        CheckBestScore();
        ScoreChanged?.Invoke(Score);
    }

    private void SaveCurrentScore()
    {
        if (Score > 0)
        {
            PlayerPrefs.SetInt(CurrentScoreKey, Score);
            PlayerPrefs.Save();
        }
    }

    private void CheckBestScore()
    {
        if (Score > BestScore)
        {
            PlayerPrefs.SetInt(BestScoreKey, Score);
            PlayerPrefs.Save();
        }
    }

    public void Reset()
    {
        Score = 0;
        SaveCurrentScore();
    }
}