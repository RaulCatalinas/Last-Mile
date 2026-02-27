using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    const string MAX_SCORE_KEY = "MaxScore";

    public static PlayerPrefsManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void SaveMaxScore(int score)
    {
        PlayerPrefs.SetInt(MAX_SCORE_KEY, score);
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt(MAX_SCORE_KEY);
    }
}
