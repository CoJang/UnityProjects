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
            SceneManager.LoadScene("2D Project");
    }

    void OnClickPlayCarRace()
    {
        if (isPassed)
            SceneManager.LoadScene("Race");
    }

    void OnClickPlayFlappyBird()
    {
        if (isPassed)
            SceneManager.LoadScene("FlappyBird");
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
