using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private int scoreRate = 1;

    public static ScoreManager Instance { get; private set; }

    private float floatScore = 0f;
    public int score { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameOver) return;

        floatScore += scoreRate * Time.deltaTime;
        score = (int)Math.Round(floatScore, MidpointRounding.AwayFromZero);

        uiManager.IncreaseScore(score);
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
