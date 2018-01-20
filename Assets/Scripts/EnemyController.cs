using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed = 3f;
    public int collisionDamage = 100;
    public GameObject projectile;
    public float shotsPerSecond = 0.2f;
    public float projectileSpeed = 5f;

    private Health health;
    private Transform gun;
    private GameObject projectilesParrent;
    private GameObject player;
	// Use this for initialization
	void Start () {
        health = gameObject.GetComponent<Health>();
        gun = gameObject.transform.Find("Gun");

        if (!GameObject.Find("Projectiles"))
        {
            projectilesParrent = new GameObject("Projectiles");
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        //Move to Player
        if (player)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, step);
        } else
        {
            //Plain movement down
            transform.position += Vector3.down * step;
        }

        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability)
        {
            Fire();
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
        Vector3 startPos = gun.position;
        GameObject shot = Instantiate(projectile, startPos, projectile.transform.rotation);
        //shot.transform.parent = projectilesParrent.transform;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

}
