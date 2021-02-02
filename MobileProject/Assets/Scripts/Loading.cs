using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    AsyncOperation async;
    float delay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingNextScene(GlobalGameManager.Instance.NextSceneName));
    }

    // Update is called once per frame
    void Update()
    {
        DelayTime(2.0f);
    }

    void DelayTime(float time)
    {
        if (async.progress >= 0.9f)
        {
            delay += Time.deltaTime;

            if(delay >= time)
            {
                async.allowSceneActivation = true;
            }
        }
    }

    IEnumerator LoadingNextScene(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false; // 로딩이 완료되어도 씬을 활성 시키지 않음

        // 90%가 완료될 때 까지 아무것도 하지 않음.
        // [async.isDone]도 있으나 버그가 있었음.
        while(async.progress < 0.9f)
        {
            // 여기에서 뭔갈 처리하면 된다.
            yield return true;
        }

        // 딜레이를 걸려면 아랫줄을 주석처리
        //async.allowSceneActivation = true;
        //yield return true;
    }
}
