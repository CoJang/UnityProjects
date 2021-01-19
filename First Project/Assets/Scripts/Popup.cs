using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public GameObject CheckImage;
    public AudioSource BGMSource;

    void OnClickBGMCheckBox()
    {
        CheckImage.SetActive(!CheckImage.activeSelf);
        if(CheckImage.activeSelf)
        {
            if (BGMSource != null)
                BGMSource.Play();
            else
                Debug.LogError("BGMSource is null!");
        }
        else
        {
            if (BGMSource != null)
                BGMSource.Stop();
            else
                Debug.LogError("BGMSource is null!");
        }
    }
}
