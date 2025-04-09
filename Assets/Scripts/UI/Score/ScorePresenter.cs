public class ScorePresenter
{
    private ScoreCounter _scoreCounter;
    private IView _scoreView;

    public ScorePresenter(ScoreCounter scoreCounter, IView scoreView)
    {
        _scoreCounter = scoreCounter;
        _scoreView = scoreView;
    }

    public void Enable() =>
        _scoreCounter.ScoreChanged += OnScoreChanged;

    public void Disable() =>
        _scoreCounter.ScoreChanged -= OnScoreChanged;

    private void OnScoreChanged(int amount) =>
        _scoreView.UpdateDisplay(amount);
}