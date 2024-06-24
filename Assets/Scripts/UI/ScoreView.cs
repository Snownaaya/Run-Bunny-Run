using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void OnEnable() => _scoreCounter.OnScoreChanged += OnScoreChanged;

    private void OnDisable() =>  _scoreCounter.OnScoreChanged -= OnScoreChanged;

    private void OnScoreChanged(int score) => _text.text = $"Score: {score}";
}