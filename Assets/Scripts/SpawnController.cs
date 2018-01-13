using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    public float minTime = 2f;
    public float maxTime = 6f;
    public GameObject enemyPrefab;

    private float delayTime;
    private bool isTimeSet = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isTimeSet)
        {
            delayTime = Random.Range(minTime, maxTime);
            isTimeSet = true;
        }
        delayTime -= Time.deltaTime;
        if(delayTime <= 0)
        {
            Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
            isTimeSet = false;
        }
        
	}
    
}
