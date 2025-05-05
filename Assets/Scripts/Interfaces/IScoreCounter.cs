using System;

public interface IScoreCounter
{
    event Action<int> ScoreChanged;
    int Score { get; }
    void IncrementScore();
    void Reset();
}
