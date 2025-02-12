using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        UpdateScoreUI();
    }

    
    private void UpdateScoreUI()
    {
        if (Score_Manager.instance != null)
        {
            scoreText.text = "Points: " + Score_Manager.instance.score_Count;
        }
    }
}
