﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

    const string MASTER_VOLUME_KEY = "master_volume";

	public static void SetMasterVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        } else
        {
            Debug.LogWarning("Cant set Volume, wrong value");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    //TODO locked and unlocked levels
}
