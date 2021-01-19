using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text IDText;
    public Text ScoreText;

    public Text GameOverIDText;
    public Text GameOverScoreText;

    void Start()
    {
        IDText.text = GlobalGameManager.Instance.GetUserID();
    }

    void OnClickOKBtn()
    {
        transform.Find("popup").gameObject.SetActive(false);
    }

    void OnClickOptionBtn()
    {
        transform.Find("popup").gameObject.SetActive(true);
    }

    public void OnGetScore(int score)
    {
        ScoreText.text = score.ToString();
    }

    public void OnGameOver()
    {
        GameOverIDText.text = GlobalGameManager.Instance.GetUserID();
        GameOverScoreText.text = ScoreText.text;
        transform.Find("GameOver").gameObject.SetActive(true);
    }
}
