using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {
    public Slider volumeSlider;
    public LevelManager levelManager;

    private MusicPlayer musicPlayer;

    // Use this for initialization
    void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
    }

    // Update is called once per frame
    void Update()
    {
        musicPlayer.ChangeVolume(volumeSlider.value);
    }

    public void SaveAndExit()
    {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        levelManager.LoadLevel("01 Start");
    }

    public void SetDefaults()
    {
        volumeSlider.value = 0.5f;
    }
}
