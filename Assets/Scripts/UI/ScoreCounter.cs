using UnityEngine;
using System;

public class ScoreCounter : MonoBehaviour, IResetteble
{
    public event Action<int> OnScoreChanged;

    private int _score = 0;

    public int Score => _score;

    public void IncrementScore()
    {
        _score++;
        OnScoreChanged?.Invoke(_score);
    }

    public void Reset()
    {
        _score = 0;
        OnScoreChanged.Invoke(_score);
    }
}
