using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // Panel zawieraj¹cy menu (wraz z mg³¹, która przykrywa grê)
    public GameObject mainMenuPanel;

    // Panel UI gry (np. punkty, licznik czasu), który ma byæ widoczny dopiero w trakcie gry
    public GameObject gameUIPanel;

    // Tekst wyœwietlaj¹cy najlepszy wynik
    public TextMeshProUGUI topScoreText;

    void Start()
    {
        // Zatrzymujemy grê, ¿eby auto nie jecha³o podczas menu
        Time.timeScale = 0f;

        // Ukrywamy UI gry, ¿eby nie by³o widoczne w tle
        if (gameUIPanel != null)
        {
            gameUIPanel.SetActive(false);
        }

        // Uaktualniamy tekst z najlepszym wynikiem
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (topScoreText != null)
        {
            topScoreText.text = "Top Score: " + bestScore;
        }
    }

    // Wywo³ywane po klikniêciu przycisku Play
    public void OnPlayButtonPressed()
    {
        // Wznawiamy grê
        Time.timeScale = 1f;

        // Ukrywamy panel menu (mg³a + elementy menu)
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }

        // Pokazujemy UI gry (punkty, czas itd.)
        if (gameUIPanel != null)
        {
            gameUIPanel.SetActive(true);
        }
    }

    // Wywo³ywane po klikniêciu przycisku Hardcore
    public void OnHardcoreButtonPressed()
    {
        GameManager.instance.SetHardcoreMode(true);

        // Przenosimy gracza na wspó³rzêdne: (-10.1, 0, 2)
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerObj.transform.position = new Vector3(-10.1f, 0f, 2f);
            // Ustawiamy tryb Hardcore (zmiana granic ruchu)
            PlayerController pc = playerObj.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.SetHardcoreMode(true);
            }
        }

        // Przenosimy g³ówn¹ kamerê na wspó³rzêdne: (-10.1, 3, -3)
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            mainCam.transform.position = new Vector3(-10.1f, 3f, -3f);
        }

        // Wznawiamy grê
        Time.timeScale = 1f;

        // Ukrywamy panel menu
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }

        // Pokazujemy panel UI gry
        if (gameUIPanel != null)
        {
            gameUIPanel.SetActive(true);
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
        Application.LoadLevel(Application.loadedLevel);
    }

}