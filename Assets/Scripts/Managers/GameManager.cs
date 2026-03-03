using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool isGameOver { get; private set; }
    public static PlayerStats selectedPlayer { get; private set; }
    public static int playerLives { get; private set; }
    public static PowerUpData activePowerUp { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SelectPlayer(PlayerStats stats)
    {
        selectedPlayer = stats;
    }

    public void StartGame()
    {
        playerLives = selectedPlayer.lives;
        isGameOver = false;
        LoadScene("City");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public void GameOver()
    {
        UIManager.Instance.ShowGameOverPanel();
        ScoreManager.Instance.SaveMaxScore();

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        playerLives = selectedPlayer.lives;
        isGameOver = false;
        Time.timeScale = 1f;

        LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void LoseLife()
    {
        playerLives--;

        if (playerLives <= 0) isGameOver = true;
    }

    public void ActivatePowerUp(PowerUpData powerUp)
    {
        activePowerUp = powerUp;
    }
}