using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool isGameOver { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
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
        isGameOver = true;
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
