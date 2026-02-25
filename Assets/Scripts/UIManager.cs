using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    public void IncreaseScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
