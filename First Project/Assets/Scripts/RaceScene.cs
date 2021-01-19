using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceScene : MonoBehaviour
{
    public bool IsGameEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 부하가 크므로 디버깅용으로만 쓸 것!
    private void OnGUI()
    {
        //GUI.TextArea(new Rect(200, 50, 100, 30), "Text test");
        //GUI.TextField(new Rect(200, 100, 100, 30), "Text test2");
        //GUI.Box(new Rect(200, 150, 100, 30), "ew");
        if(IsGameEnd)
        {
            if (GUI.Button(new Rect(250, 200, 250, 30), "다시 시작"))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}
