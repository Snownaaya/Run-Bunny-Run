using UnityEngine;
using System;

public class ScoreCounter : MonoBehaviour, IResetteble
{
    public event Action<int> ScoreChanged;

    private int _score = 0;

    public int Score => _score;

    public void IncrementScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    public void Reset()
    {
        _score = 0;
        ScoreChanged.Invoke(_score);
    }
}
