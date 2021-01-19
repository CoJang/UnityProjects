using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    private static GlobalGameManager instance;
    private string UserID = "Unknown";

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
}
