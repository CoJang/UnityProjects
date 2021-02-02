using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    static GlobalGameManager instance;
    string UserID = "Unknown";
    int score = 0;

    public string NextSceneName = "Unknown";

    public static GlobalGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("GlobalGameManager");
                instance = newGameObject.AddComponent<GlobalGameManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnIDEditFinish(string id)
    {
        UserID = id;
    }

    public string GetUserID()
    {
        return UserID;
    }

    public void SetScore(int inputScore)
    {
        score = inputScore;
    }

    public int GetScore()
    {
        return score;
    }
}
