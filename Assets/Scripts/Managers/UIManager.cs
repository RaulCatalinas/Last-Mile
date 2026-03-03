using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] private GameObject characterSelectionPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartsContainer;

    public static UIManager Instance { get; private set; }

    private List<HeartController> hearts = new List<HeartController>();
    private int previousLives;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void IncreaseScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        GameManager.Instance.RestartGame();
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void OpenCharacterSelection()
    {
        mainMenuPanel.SetActive(false);
        characterSelectionPanel.SetActive(true);
    }

    public void UpdateLives(int currentLives)
    {
        Debug.Log($"Updating lives: {currentLives}, Previous: {previousLives}");

        if (currentLives < previousLives)
        {
            for (int i = previousLives - 1; i >= currentLives; i--)
            {
                hearts[i].PlayLoseHeartAnimation();
            }
        }
        else
        {
            for (int i = previousLives; i < currentLives; i++)
            {
                hearts[i].PlayGainHealthAnimation();
            }
        }

        previousLives = currentLives;
    }

    public void InitializeLives(int totalLives)
    {
        for (int i = 0; i < totalLives; i++)
        {
            var heartObj = Instantiate(heartPrefab, heartsContainer);
            var heart = heartObj.GetComponent<HeartController>();

            hearts.Add(heart);
        }

        previousLives = totalLives;
    }
}
