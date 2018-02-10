using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartVolume : MonoBehaviour {
    private MusicPlayer musicPlayer;


    // Use this for initialization
    void Start()
    {
        musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
        if (musicPlayer)
        {
            float volume = PlayerPrefsManager.GetMasterVolume();
            musicPlayer.ChangeVolume(volume);
        }
        else
        {
            Debug.LogWarning("Can not find music manager");
        }
    }
}
