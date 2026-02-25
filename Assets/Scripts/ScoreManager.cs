using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private int scoreRate = 1;

    private float floatScore = 0f;
    public int score { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameOver) return;

        floatScore += scoreRate * Time.deltaTime;
        score = (int)Math.Round(floatScore, MidpointRounding.AwayFromZero);

        uiManager.IncreaseScore(score);
    }
}
