using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreItemTextPrefab;
    [SerializeField] private GameObject canvasPanel;
    private HighScoreController highScoreController;

    public void BackToMenu()
    {
        SceneManager.LoadScene("Scenes/start");
    }

    void Start()
    {
        highScoreController = GameObject.Find("HighScore").GetComponent<HighScoreController>();
        DisplayScoreBoard();
    }

    private void DisplayScoreBoard()
    {
        List<HighScore> highScores = highScoreController.GetAllHighScores();
        
        if (highScores.Count > 0)
        {
            int index = 0;
            foreach (HighScore item in highScores)
            {
                TextMeshProUGUI itemText = Instantiate(highScoreItemTextPrefab);
                itemText.text = $"{index + 1}. {item.playerName} : {item.score}";
                itemText.fontSize -= index * 3;
                itemText.transform.SetParent(canvasPanel.transform);
                itemText.transform.localPosition = new Vector2(0, 90 - index * 40);

                index++;
            }
        }
        else
        {
            TextMeshProUGUI noHighScoresText = Instantiate(highScoreItemTextPrefab);
            noHighScoresText.text = "No high scores yet - be the first to score!";
            noHighScoresText.transform.SetParent(canvasPanel.transform);
            noHighScoresText.transform.localPosition = new Vector2(0, 90);
        }
    }
}
