using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private string _playerName;
    public static GameSession instance;
    
    public string playerName
    {
        get => _playerName;
        set
        {
            if (_playerName == null)
            {
                _playerName = value;
            }
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
