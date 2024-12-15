using System;

public class ScoreCounter : IResetteble
{
    public event Action<int> ScoreChanged;

    public int Score { get; private set; }

    public void IncrementScore()
    {
        ScoreChanged?.Invoke(Score++);
    }

    public void Reset()
    {
        Score = 0;
        ScoreChanged?.Invoke(0);
    }
}