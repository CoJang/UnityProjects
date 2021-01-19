using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioClips[0] = Resources.Load(string.Format("Resource/Sound/{0}", "title")) as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            StopAndPlay(audioClips[0]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StopAndPlay(audioClips[1]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            StopAndPlay(audioClips[2]);
        }

    }

    void StopAndPlay(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = clip;
        audioSource.Play();
    }
}
