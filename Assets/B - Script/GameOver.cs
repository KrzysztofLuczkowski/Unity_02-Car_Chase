using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    // Panel wyœwietlany przy Game Over (z przyciskiem Main Menu i tekstem wyniku)
    public GameObject gameOverPanel;
    // Tekst wyœwietlaj¹cy wynik (Your Points:)
    public TextMeshProUGUI yourPointsText;
    // Panel UI gry (np. licznik punktów), który chcemy ukryæ przy Game Over
    public GameObject gameUIPanel;
    // Referencja do skryptu filtru grayscale (umieszczonego na g³ównej kamerze)
    public GrayscaleEffect grayscaleEffect;

    private bool gameOverTriggered = false;

    void Start()
    {
        // Na starcie ukrywamy panel Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!gameOverTriggered && other.gameObject.CompareTag("Obstacle"))
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        gameOverTriggered = true;
        Time.timeScale = 0f;

        // Ukrywamy UI gry (np. punkty, czas itp.)
        if (gameUIPanel != null)
        {
            gameUIPanel.SetActive(false);
        }

        // Pobieramy wynik z systemu punktacji
        int points = Score_Manager.instance != null ? Score_Manager.instance.score_Count : 0;
        if (yourPointsText != null)
        {
            yourPointsText.text = "Your Points: " + points;
            SaveHighScore(points);
        }

        // Pokazujemy panel Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // W³¹czamy filtr czarno-bia³y (grayscale)
        if (grayscaleEffect != null)
        {
            grayscaleEffect.SetEffect(true);
        }
    }

    // Wywo³ywane po naciœniêciu przycisku "Main Menu" na panelu Game Over
    public void OnMainMenuButtonPressed()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void SaveHighScore(int score)
    {
        int bestScore = PlayerPrefs.GetInt("HighScore", 0); // Pobiera zapisany wynik, domyœlnie 0
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save(); // Zapisujemy wynik na sta³e
        }
    }
}
