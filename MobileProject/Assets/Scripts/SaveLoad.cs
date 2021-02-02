using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 한번에 저장하는 데이터의 크기가
 * 1메가가 넘는 경우 에러!
 */

public class SaveLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            if(PlayerPrefs.HasKey("ID"))
            {
                string getID = PlayerPrefs.GetString("ID");
                Debug.Log(string.Format("ID : {0}", getID));
            }
            else
            {
                Debug.Log("ID 없음");
            }
        }

        if(Input.GetKey(KeyCode.B))
        {
            string setID = "PlayerID";
            PlayerPrefs.SetString("ID", setID);
            Debug.Log("ID Saved! " + setID);
        }

        if(Input.GetKey(KeyCode.C))
        {
            PlayerPrefs.SetInt("INT", 33);
            PlayerPrefs.SetFloat("FLOAT", 44.44f);

            int getInt = PlayerPrefs.GetInt("INT");
            float getFloat = PlayerPrefs.GetFloat("FLOAT");

        }

        if(Input.GetKey(KeyCode.D))
        {
            // key에 저장된 값이 없다면 우항에 설정된 기본 값을 로드
            int getInt = PlayerPrefs.GetInt("INT2", 66);
            float getFloat = PlayerPrefs.GetFloat("FLOAT2", 77.77f);
        }

        if (Input.GetKey(KeyCode.F))
        {
            PlayerPrefs.DeleteKey("ID");
            PlayerPrefs.DeleteKey("INT");
            PlayerPrefs.DeleteKey("FLOAT");

            PlayerPrefs.DeleteAll();
        }

        if(Input.GetKey(KeyCode.Z))
        {
            PlayerPrefs.Save();
        }
    }
}
