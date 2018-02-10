using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    public AudioClip audioClip;

    private AudioSource audioSource;
	
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void OnLevelWasLoaded()
    {
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void ChangeVolume(float value)
    {
        audioSource.volume = value;
    }
}
