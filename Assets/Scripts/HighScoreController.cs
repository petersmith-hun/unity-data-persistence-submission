using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

[Serializable]
public class HighScore
{
    public string playerName;
    public int score;

    public HighScore()
    {
        // default constructor for deserialization
    }

    public HighScore(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}

public class HighScoreBoard
{
    public List<HighScore> highScores;

    public HighScoreBoard()
    {
        this.highScores = new List<HighScore>();
    }
}

public class HighScoreController : MonoBehaviour
{
    private static bool isInitialized = false;
    
    private int numberOfStoredHighScores = 5;
    private string highScoresFilePath;
    private HighScoreBoard highScoreBoard;

    public void UpdateHighScores(string playerName, int score)
    {
        RecalculateHighScores(new HighScore(playerName, score));
        SaveHighScores();
    }

    public HighScore GetTopHighScore()
    {
        return highScoreBoard.highScores
            .FirstOrDefault<HighScore>();
    }

    public List<HighScore> GetAllHighScores()
    {
        return highScoreBoard.highScores;
    }

    private void RecalculateHighScores(HighScore highScoreCandidate)
    {
        highScoreBoard.highScores.Add(highScoreCandidate);
        highScoreBoard.highScores = highScoreBoard.highScores
            .OrderByDescending(item => item.score)
            .Take(numberOfStoredHighScores)
            .ToList<HighScore>();
    }

    private void Awake() 
    {
        if (isInitialized)
        {
            Destroy(gameObject);
            return;
        }

        SetHighScoresFilePath();
        LoadHighScores();

        DontDestroyOnLoad(gameObject);
        isInitialized = true;
    }

    private void SetHighScoresFilePath()
    {
        highScoresFilePath = $"{Application.persistentDataPath}/high_scores.json";
    }

    private void LoadHighScores()
    {
        if (File.Exists(highScoresFilePath))
        {
            string highScoresFileContent = File.ReadAllText(highScoresFilePath);
            highScoreBoard = JsonUtility.FromJson<HighScoreBoard>(highScoresFileContent);
        }
        else
        {
            highScoreBoard = new HighScoreBoard();
        }
    }

    private void SaveHighScores()
    {
        string scoreBoardAsJSON = JsonUtility.ToJson(highScoreBoard);
        File.WriteAllText(highScoresFilePath, scoreBoardAsJSON);
    }
}
