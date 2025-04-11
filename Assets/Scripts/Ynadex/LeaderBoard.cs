using UnityEngine;
using YG;

public class LeaderBoard : MonoBehaviour
{
    private const string Score = "Score";
    private const string LastScoreKey = "LastScore";
    private const string AuthPromptKey = "AuthPromptShown";

    private ScoreCounter _scoreCounter;

    public void Initialize(ScoreCounter scoreCounter) =>
        _scoreCounter = scoreCounter;

    public void AdLeaderBoard()
    {
        int currentScore = _scoreCounter.Score;

        if (YandexGame.auth)
        {
            YandexGame.NewLeaderboardScores(Score, currentScore);
            YandexGame.SaveProgress();
        }
        else
        {
            PlayerPrefs.SetInt(LastScoreKey, currentScore);
            PlayerPrefs.Save();

            if (PlayerPrefs.GetInt(AuthPromptKey, 0) == 0)
            {
                YandexGame.AuthDialog();
                PlayerPrefs.SetInt(AuthPromptKey, 1);
                PlayerPrefs.Save();
            }
        }
    }
}