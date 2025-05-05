using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class LeaderBoardPanel : MonoBehaviour
{
    private const string Counter = "Counter";
    private const string LastScoreKey = "LastScoreKey";
    private const string AuthPromptKey = "AuthPromptKey";

    [SerializeField] private AuthorizationPanel _authorizationPanel;
    [SerializeField] private LeaderBoard _leaderboard;
    [SerializeField] private ScoreCounter _scoreCounter;

    private Button _leaderboardButton;
    private int _minScore = 0;

    private void Awake() =>
        _leaderboardButton = GetComponent<Button>();

    private void OnEnable() =>
        _leaderboardButton.onClick.AddListener(OnLeaderboardClick);

    private void OnDisable() =>
        _leaderboardButton.onClick.RemoveListener(OnLeaderboardClick);

    private void OnLeaderboardClick()
    {
        int bestScore = _scoreCounter.BestScore;

        if (YandexGame.auth)
        {
            _leaderboard.gameObject.SetActive(true);

            if (bestScore > _minScore)
            {
                YandexGame.NewLeaderboardScores(Counter, bestScore);
                YandexGame.SaveProgress();
            }
        }
        else
        {
            _authorizationPanel.gameObject.SetActive(true);
        }
    }

}