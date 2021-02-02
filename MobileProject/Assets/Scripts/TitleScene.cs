using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    public Image IDCheckImage;
    public Sprite[] IDSprites = new Sprite[2];
    public InputField IDInputField;

    private bool isPassed = false;

    void Start()
    {
        if(IDSprites[0] != null)
        {
            IDCheckImage.sprite = IDSprites[0];
        }
        else
        {
            Debug.LogError("IDSprites[0] is NULL!");
        }
    }

    void OnClickPlayDragonFlight()
    {
        if(isPassed)
        {
            GlobalGameManager.Instance.NextSceneName = "2D Project";
            SceneManager.LoadScene("LoadingScene");
        }
    }

    void OnClickPlayCarRace()
    {
        if (isPassed)
        {
            GlobalGameManager.Instance.NextSceneName = "Race";
            SceneManager.LoadScene("LoadingScene");
        }
    }

    void OnClickPlayFlappyBird()
    {
        if (isPassed)
        {
            GlobalGameManager.Instance.NextSceneName = "FlappyBird";
            SceneManager.LoadScene("LoadingScene");
        }
    }

    void OnClickPlayDefence()
    {
        if (isPassed)
        {
            GlobalGameManager.Instance.NextSceneName = "Defence";
            SceneManager.LoadScene("LoadingScene");
        }
    }

    void OnClickPlayMazeRun()
    {
        if (isPassed)
        {
            GlobalGameManager.Instance.NextSceneName = "NavMesh";
            SceneManager.LoadScene("LoadingScene");
        }
    }

    void OnIDInputFieldChange()
    {
        if(IDInputField != null)
        {
            if(IDInputField.text.Length > 2)
            {
                if (IDSprites[1] != null)
                {
                    IDCheckImage.sprite = IDSprites[1];
                    isPassed = true;

                    GlobalGameManager.Instance.OnIDEditFinish(IDInputField.text);
                }
                else
                {
                    Debug.LogError("IDSprites[1] is NULL!");
                }
            }
        }
        else
        {
            Debug.LogError("IDInputField is NULL!");
        }
    }

    void OnIDInputFieldEditEnd()
    {
        if (IDInputField != null)
        {
            if (IDInputField.text.Length > 2)
            {
                if (IDSprites[1] != null)
                {
                    IDCheckImage.sprite = IDSprites[1];
                    isPassed = true;

                    GlobalGameManager.Instance.OnIDEditFinish(IDInputField.text);
                }
                else
                {
                    Debug.LogError("IDSprites[1] is NULL!");
                }
            }
        }
        else
        {
            Debug.LogError("IDInputField is NULL!");
        }
    }
}
