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
            /*Vector2 direction = (Vector2)target.transform.position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.AddForce(transform.up * acceleration);
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
            }*/
            Vector2 targetPos = target.transform.position;
            Vector2 misslePos = gameObject.transform.position;
            float distance = Vector2.Distance(misslePos, targetPos);//distance in between in meters
            float travelTime = distance / maxSpeed / 1.5f; //time in seconds the missle would need to arrive at the target
            Vector3 aimPoint = targetPos + target.GetComponent<Rigidbody2D>().velocity * travelTime;
            Vector3 dir = transform.position - aimPoint;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            rb.AddForce(transform.up * acceleration);
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
            }
        } else
        {
            //Explosion.Play();
            Destroy(gameObject);
        }
    }
}
