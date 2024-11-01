using System;

public class ScoreCounter : IResetteble
{
    public event Action ScoreChanged;

    public int Score { get; private set; }

    public void IncrementScore(int amount)
    {
        Score += amount;
        ScoreChanged?.Invoke();
    }

    public void Reset()
    {
        Score = 0;
    }
}
