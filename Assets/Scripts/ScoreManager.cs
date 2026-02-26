using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private int scoreRate = 1;

    private float floatScore = 0f;
    public int score { get; private set; }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameOver) return;

        floatScore += scoreRate * Time.deltaTime;
        score = (int)Math.Round(floatScore, MidpointRounding.AwayFromZero);

        uiManager.IncreaseScore(score);
    }
}
