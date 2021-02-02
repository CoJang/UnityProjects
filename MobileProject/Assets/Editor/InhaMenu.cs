using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class InhaMenu : MonoBehaviour
{
    [MenuItem("InhaMenu/Clear PlayerPrefs")]

    private static void Clear()
    {
        Debug.Log("Clear");
    }

    [MenuItem("InhaMenu/SubMenu/Select")]

    private static void SubSelect()
    {
        Debug.Log("Select");
    }

    [MenuItem("InhaMenu/SubMenu/HotKey1 %#v")]
    private static void SubHotKey1()
    {
        Debug.Log("컨트롤 + 쉬프트 + v");
    }

    [MenuItem("InhaMenu/SubMenu/HotKey2 &g")]
    private static void SubHotKey2()
    {
        Debug.Log("알트 + g");
    }

    [MenuItem("Assets/Load Selected Scene")]
    private static void LoadSelectScene()
    {
        var Selected = Selection.activeObject;
        if(EditorApplication.isPlaying)
        {
            EditorSceneManager.LoadScene(AssetDatabase.GetAssetPath(Selected));
        }
        else
        {
            EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(Selected));
        }
    }
}
