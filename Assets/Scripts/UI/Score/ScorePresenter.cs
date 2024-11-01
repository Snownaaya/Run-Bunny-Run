public class ScorePresenter
{
    private ScoreCounter _scoreCounter;
    private IScoreView _scoreView;

    public ScorePresenter(ScoreCounter scoreCounter, IScoreView scoreView)
    {
        _scoreCounter = scoreCounter;
        _scoreView = scoreView;
    }

    public void Enable() =>
        _scoreCounter.ScoreChanged += OnScoreChanged;

    public void Disable() =>
        _scoreCounter.ScoreChanged -= OnScoreChanged;

    private void OnScoreChanged()
    {
        _scoreView.UpdateScore(_scoreCounter.Score);
    }
}