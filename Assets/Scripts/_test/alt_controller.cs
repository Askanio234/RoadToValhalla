using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alt_controller : MonoBehaviour {
    public float rotationSpeed = 100f;
    public float enginePower = 1f;
    public Text speedText;

    private Rigidbody2D rb;
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(0, 0, -h * rotationSpeed * Time.deltaTime));


        if (v>0)
        {
            rb.AddForce(transform.up * enginePower);
        
        //For now allow to stop without turning ship around
        } else if (v < 0)
        {
            rb.AddForce(-transform.up * enginePower);
        }
        

        speedText.text = "Velocity: " + rb.velocity.ToString("#.0");
	}
}
