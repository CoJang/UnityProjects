using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text IDText;
    public Text ScoreText;
    public Text HPText;

    public Text GameOverIDText;
    public Text GameOverScoreText;
    public Text[] GameOverRankIDText = new Text[3];
    public Text[] GameOverRankScoreText = new Text[3];


    void Start()
    {
        IDText.text = GlobalGameManager.Instance.GetUserID();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return))
        {
            PlayerPrefs.SetInt("Rank" + 1, 200);
            PlayerPrefs.SetInt("Rank" + 2, 100);
            PlayerPrefs.SetInt("Rank" + 3, 50);
            ScoreText.text = "300";
            Save();
        }
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

    public void OnHit(int hp)
    {
        HPText.text = hp.ToString();
    }

    public void OnGameOver()
    {
        GameOverIDText.text = GlobalGameManager.Instance.GetUserID();
        GameOverScoreText.text = ScoreText.text;
        Save();
        transform.Find("GameOver").gameObject.SetActive(true);
    }

    public void Save()
    {
        const int TopListNum = 3;
        int currentScore = int.Parse(ScoreText.text);
        int[] Rank = new int[3];
        string[] RankID = new string[3];

        for(int i = 0; i < TopListNum; i++)
        {
            Rank[i] = PlayerPrefs.GetInt("Rank" + (i + 1), 0);
            RankID[i] = PlayerPrefs.GetString("Rank" + (i + 1) + "ID", "Unknown");
        }

        bool isSaved = false;

        for (int i = 0; i < TopListNum; i++)
        {
            if (currentScore > Rank[i])
            {
                if (!isSaved)
                {
                    PlayerPrefs.SetInt("Rank" + (i + 1), currentScore);
                    PlayerPrefs.SetString("Rank" + (i + 1) + "ID", GlobalGameManager.Instance.GetUserID());
                    isSaved = true;
                }

                if (i != TopListNum - 1)
                {
                    PlayerPrefs.SetInt("Rank" + (i + 2), Rank[i]);
                    PlayerPrefs.SetString("Rank" + (i + 2) + "ID", RankID[i]);
                }
            }
        }

        PlayerPrefs.Save();

        for(int i = 0; i < TopListNum; i++)
        {
            GameOverRankIDText[i].text = PlayerPrefs.GetString("Rank" + (i + 1) + "ID");
            GameOverRankScoreText[i].text = PlayerPrefs.GetInt("Rank" + (i + 1)).ToString();
        }
    }
}
