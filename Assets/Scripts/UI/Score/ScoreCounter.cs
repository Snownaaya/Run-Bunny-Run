using System;

[Serializable]
public class ScoreCounter
{
    public event Action<int> ScoreChanged;

    public int Score { get; private set; }

    public void IncrementScore()
    {
        ScoreChanged?.Invoke(Score++);
    }
}