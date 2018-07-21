using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour {
    public float timeToDestruction = 1.5f;

    public Vector3 RandomizeIntensity = new Vector3(0.7f, 0, 0);
	// Use this for initialization
	void Start () {
        Destroy(gameObject, timeToDestruction);

        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
        Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z)); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
