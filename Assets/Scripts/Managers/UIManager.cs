using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] private GameObject characterSelectionPanel;
    [SerializeField] private GameObject mainMenuPanel;

    public static UIManager Instance { get; private set; }

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
}
