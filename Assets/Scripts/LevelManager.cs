using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public float autoLoadInSecs;

	// Use this for initialization
	void Start () {
		if(autoLoadInSecs <= 0)
        {
            Debug.Log("auto load disabled use positive num");
        } else
        {
            Invoke("LoadNextLevel", autoLoadInSecs);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
