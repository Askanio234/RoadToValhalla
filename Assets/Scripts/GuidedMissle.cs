using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissle : Projectile {
    public float timeFuel = 10f;
    public float acceleration = 25f;
    public float turnOnAfrerSecs = 1f;
    public float rotateSpeed = 50f;
    public float maxSpeed = 600f;

    private float instantiatedAt;
    private GameObject target;
    private Rigidbody2D rb;
    private ParticleSystem Engine;
    private ParticleSystem Explosion;
    public GameObject explosionObj; 

	// Use this for initialization
	void Start () {
        instantiatedAt = Time.timeSinceLevelLoad;
        rb = gameObject.GetComponent<Rigidbody2D>();
        Engine = gameObject.GetComponentInChildren<ParticleSystem>();
        Explosion = explosionObj.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.timeSinceLevelLoad >= instantiatedAt + turnOnAfrerSecs) {
            Engine.Play();
            Engage();
        }
	}

    void LockOnTarget()
    {
        var targetList = GameObject.FindGameObjectsWithTag("Enemy");
        //print("target list length is " + targetList.Length);
        int randomIndex = Random.Range(0, targetList.Length);
        //print("random index is " + randomIndex);
        try {
            target = targetList[randomIndex];
        } catch (System.IndexOutOfRangeException)
        {
            print("No target");
        }
        
    }

    void Engage()
    {
        if (!target)
        {
            LockOnTarget();
        } else
        {
            FlyTowards(target);
        }
    }

    void FlyTowards(GameObject target)
    {
        if (Time.timeSinceLevelLoad < instantiatedAt + timeFuel)
        {
            Vector2 direction = (Vector2)target.transform.position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.AddForce(transform.up * acceleration);
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
            }
        } else
        {
            Explosion.Play();
            Destroy(gameObject);
        }
    }
}
