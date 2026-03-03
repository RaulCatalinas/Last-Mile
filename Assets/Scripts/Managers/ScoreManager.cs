using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float scoreInterval = 1f;

    public static ScoreManager Instance { get; private set; }

    public int score { get; private set; }

    private float temporaryMultiplier = -1f;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        StartCoroutine(ScoreRoutine());
    }

    IEnumerator ScoreRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(scoreInterval);

            if (GameManager.isGameOver) yield break;

            var multiplier = temporaryMultiplier >= 0f
                ? temporaryMultiplier
                : GameManager.selectedPlayer.scoreMultiplier;

            score += (int)multiplier;

            UIManager.Instance.IncreaseScore(score);
        }
    }

    public void AddMultiplier(float value)
    {
        temporaryMultiplier = GameManager.selectedPlayer.scoreMultiplier + value;
    }

    public void RemoveMultiplier()
    {
        temporaryMultiplier = -1f;
    }

    public void SetTemporaryMultiplier(float value)
    {
        temporaryMultiplier = value;
    }

    public void RestoreMultiplier()
    {
        temporaryMultiplier = -1f;
    }

    public void SaveMaxScore()
    {
        PlayerPrefsManager.Instance.SaveMaxScore(score);
    }

    public int GetMaxScore()
    {
        return PlayerPrefsManager.Instance.GetMaxScore();
    }
}
