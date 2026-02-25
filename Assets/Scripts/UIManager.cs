using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    public void IncreaseScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
