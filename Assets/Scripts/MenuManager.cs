using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputField playerNameInputField;
    [SerializeField] private Text validationErrorMessageText;
    [SerializeField] private TextMeshProUGUI highestScoreText;
    private HighScoreController highScoreController;
    
    public void StartGame()
    {
        if (SetPlayerName())
        {
            StartMainScene();
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    void Start()
    {
        highScoreController = GameObject.Find("HighScore").GetComponent<HighScoreController>();

        DisplayCurrentHighestScore();
    }

    private bool SetPlayerName()
    {
        if (playerNameInputField.text.Length > 0)
        {
            GameSession.instance.playerName = playerNameInputField.text;
            return true;
        }
        else 
        {
            validationErrorMessageText.gameObject.SetActive(true);
        }

        return false;
    }

    private void StartMainScene()
    {
        validationErrorMessageText.gameObject.SetActive(false);
        SceneManager.LoadScene("Scenes/main");
    }

    private void DisplayCurrentHighestScore()
    {
        HighScore currentHighestScore = highScoreController.GetTopHighScore();
        highestScoreText.text = currentHighestScore == null 
            ? "No best score yet - be the first!" 
            : $"Best score : {currentHighestScore.playerName} : {currentHighestScore.score}";
    }
}
