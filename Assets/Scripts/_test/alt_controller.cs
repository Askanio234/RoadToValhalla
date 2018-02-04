using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alt_controller : MonoBehaviour {
    public float rotationSpeed = 100f;
    public float maxSpeed = 40;
    public Text speedText;

    float currentSpeed = 0;
    float movingAccel = 2;
    float brakingAccel = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(0, 0, -h * rotationSpeed * Time.deltaTime));

        if (v == 0)
        {
            currentSpeed -= brakingAccel * Time.deltaTime;
        } else
        {
            currentSpeed += v * movingAccel * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); 

        transform.Translate(new Vector3(0, currentSpeed * Time.deltaTime, 0));

        speedText.text = "Speed: " + currentSpeed.ToString("#.0");
	}
}
