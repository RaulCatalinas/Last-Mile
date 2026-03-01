using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float scoreInterval = 1f;

    public static ScoreManager Instance { get; private set; }

    public int score { get; private set; }

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

            score += (int)GameManager.selectedPlayer.scoreMultiplier;
            uiManager.IncreaseScore(score);
        }
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