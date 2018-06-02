using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float enginePower = 0.5f;
    public int collisionDamage = 100;
    public GameObject projectile;
    public float shotsPerSecond = 0.2f;
    public float firingDistance = 12f;
    public float maxSpeed = 5f;
    public float projectileSpeed = 1000f;

    private Health health;
    private Transform gun;
    private GameObject projectilesParrent;
    private GameObject player;
    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        health = gameObject.GetComponent<Health>();
        gun = gameObject.transform.Find("Gun");

        if (!GameObject.Find("Projectiles"))
        {
            projectilesParrent = new GameObject("Projectiles");
        }
        projectilesParrent = GameObject.Find("Projectiles");
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //Very rough algorithm for leading player pos
        Vector2 target = player.transform.position;
        Vector2 enemy = gameObject.transform.position;
        float distance = Vector2.Distance(enemy, target);//distance in between in meters
        float travelTime = distance / maxSpeed / 2f; //time in seconds the shot would need to arrive at the target
        Vector3 aimPoint = target + player.GetComponent<Rigidbody2D>().velocity * travelTime;

        Vector3 dir = transform.position - aimPoint;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        rb.AddForce(-transform.up * enginePower);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
	// Update is called once per frame
	void Update () {
        if (isTimeToFire(shotsPerSecond))
        {
            if (Vector2.Distance(gameObject.transform.position, player.transform.position) < firingDistance)
            {
                Fire();
            }
        }
    }

    bool isTimeToFire(float shotsPerSecond)
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability)
        {
            return true;
        } else
        {
            return false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile projectile = col.gameObject.GetComponent<Projectile>();

        if (projectile)
        {
            health.GetDamage(projectile.GetDamage());
            projectile.Hit();
        }
    }

    public int GetCollisionDamage()
    {
        return collisionDamage;
    }

    void Fire()
    {
        Vector2 startPos = gun.position;
        GameObject shot = Instantiate(projectile, startPos, gun.rotation);
        shot.transform.parent = projectilesParrent.transform;
        Rigidbody2D shotRB = shot.GetComponent<Rigidbody2D>();
        shotRB.velocity = rb.velocity;
        shotRB.AddForce(-shot.transform.up * projectileSpeed);
    }

}
