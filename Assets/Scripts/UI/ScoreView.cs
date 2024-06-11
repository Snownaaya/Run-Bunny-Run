using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private List<Roader> _roaders;

    private void OnEnable()
    {
        for (int i = 0; i < _roaders.Count; i++)
        {
            _roaders[i].RoadMoving += OnScoreChanged;
        }
    }

    private void OnScoreChanged(int roadIndex)
    {
        int score = _roaders[roadIndex].ScoreChange;
        _text.text = $"Score: {score}";
    }
}