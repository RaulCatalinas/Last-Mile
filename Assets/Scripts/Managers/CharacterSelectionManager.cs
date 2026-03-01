using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject characterSelectionPanel;
    [SerializeField] private GameObject mapSelectionPanel;

    [Header("Characters")]
    [SerializeField] private List<PlayerStats> characters;

    [Header("UI")]
    [SerializeField] private Image characterPreview;
    [SerializeField] private TMP_Text lives;
    [SerializeField] private TMP_Text invincibleTime;
    [SerializeField] private TMP_Text scoreMultiplier;

    private int currentIndex = 0;

    void Start()
    {
        UpdatePreview();
    }

    public void PreviousCharacter()
    {
        currentIndex--;

        if (currentIndex < 0) currentIndex = characters.Count - 1;

        UpdatePreview();
    }

    public void NextCharacter()
    {
        currentIndex++;

        if (currentIndex >= characters.Count) currentIndex = 0;

        UpdatePreview();
    }

    public void ConfirmSelection()
    {
        GameManager.Instance.SelectPlayer(characters[currentIndex]);
        characterSelectionPanel.SetActive(false);
        //mapSelectionPanel.SetActive(true);
        GameManager.Instance.StartGame();
    }

    private void UpdatePreview()
    {
        var currentCharacter = characters[currentIndex];

        characterPreview.sprite = currentCharacter.sprite;
        lives.text = currentCharacter.lives.ToString();
        invincibleTime.text = currentCharacter.invincibilityTime.ToString();
        scoreMultiplier.text = currentCharacter.scoreMultiplier.ToString();
    }
}