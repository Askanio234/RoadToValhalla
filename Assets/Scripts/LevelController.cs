using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    public int levelNum = 1;
    public LevelManager levelManager;
    public GameObject winUI;
    public bool isTimedLevel;
    [Header("Time to survive:")]
    public float timeForLevel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isTimedLevel)
        {
            if (Time.timeSinceLevelLoad >= timeForLevel)
            {
                Win();
            }
        }
	}

    private void Win()
    {
        winUI.SetActive(true);
        Invoke("LoadShop", 3f);
        GameKeeper.gameKeeper.maxLelvelReached = levelNum;
    }

    private void LoadShop()
    {
        levelManager.LoadLevel("012 shop");
    }

    public void Lose()
    {
        winUI.GetComponentInChildren<Text>().text = "Mission Failed";
        winUI.SetActive(true);
        Invoke("LoadShop", 3f);
    }
}
