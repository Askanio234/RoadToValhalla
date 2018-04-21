using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour {

    public Button[] levelButtons;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i > GameKeeper.gameKeeper.maxLelvelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
