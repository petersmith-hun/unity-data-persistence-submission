using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputField playerNameInputField;
    [SerializeField] private Text validationErrorMessageText;
    
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
}
