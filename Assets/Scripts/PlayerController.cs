using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Sprite damagedSprite;
    public ParticleSystem leftEngine, rightEngine;
    public ParticleSystem shieldEffect;
    public float speed = 10f;
    public float rotationSpeed = 1000f;
    public float padding = 2f;
    public GameObject projectile;
    public float projectileSpeed;

    public float firingRate;
    private Transform gunPos;
    private GameObject projectilesParrent;
    private Health health;
    private Shield shield;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float lastHitTime;
    private float oldEmissionRate;
    private float newEmissionRate;
    private float oldStartSpeed;
    private float newStartSpeed;
    float xmin;
    float xmax;
    float ymin;
    float ymax;
	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        xmin = leftMost.x + padding;
        xmax = rightMost.x - padding;
        ymin = leftMost.y + padding;
        ymax = topMost.y - padding;

        gunPos = gameObject.transform.Find("Gun");
        if (!GameObject.Find("Projectiles"))
        {
            projectilesParrent = new GameObject("Projectiles");
        }
        oldEmissionRate = leftEngine.emission.rateOverTimeMultiplier;
        oldStartSpeed = 0.2f;
        newEmissionRate = 60f;
        newStartSpeed = 0.4f;

        health = gameObject.GetComponent<Health>();
        shield = gameObject.GetComponent<Shield>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();


    }
	
	// Update is called once per frame
	void Update ()
    {
        //HandlePlayerInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        HandleShieldRegen();
    }

    void FixedUpdate()
    {
        HandlePlayerInput1();
    }

    private void HandlePlayerInput1()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0) {
            EngageAfterBurners();
        } else {
            DisengageAfterBurners();
        }

        Vector3 movement = new Vector3(horizontal, vertical, 0.0f);

        rb.velocity = movement * speed;

        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        float newY = Mathf.Clamp(transform.position.y, ymin, ymax);

        rb.position = new Vector3(newX, newY, transform.position.z);
    }

    /*private void HandlePlayerInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            EngageAfterBurners();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            DisengageAfterBurners();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        float newY = Mathf.Clamp(transform.position.y, ymin, ymax);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }*/

    private void DisengageAfterBurners()
    {
        var mainRight = rightEngine.main;
        var mainLeft = leftEngine.main;
        mainRight.startSpeed = oldStartSpeed;
        mainLeft.startSpeed = oldStartSpeed;
        var emissionRight = rightEngine.emission;
        var emissionLeft = leftEngine.emission;
        emissionRight.rateOverTimeMultiplier = oldEmissionRate;
        emissionLeft.rateOverTimeMultiplier = oldEmissionRate;
    }

    private void EngageAfterBurners()
    {
        var mainRight = rightEngine.main;
        var mainLeft = leftEngine.main;
        mainRight.startSpeed = newStartSpeed;
        mainLeft.startSpeed = newStartSpeed;
        var emmisionRight = rightEngine.emission;
        var emissionLeft = leftEngine.emission;
        emmisionRight.rateOverTimeMultiplier = newEmissionRate;
        emissionLeft.rateOverTimeMultiplier = newEmissionRate;
    }

    private void Fire()
    {
        Vector3 startPos = gunPos.position;
        GameObject shot = Instantiate(projectile, startPos, Quaternion.identity);
        shot.transform.parent = projectilesParrent.transform;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        //TODO play sound
    }

    /*private void Fire()
    {
        courseWeapon.Fire();
    }*/

    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile projectile = col.gameObject.GetComponent<Projectile>();
        EnemyController enemy = col.gameObject.GetComponent<EnemyController>();
        if (projectile)
        {
            float damage = projectile.GetDamage();
            float absorbedDamage = shield.AbsorbDamage(damage);
            hadleShieldEffect(absorbedDamage, damage);
            health.GetDamage(absorbedDamage);
            projectile.Hit();
            lastHitTime = Time.time;
        }
        //Collision damage is not absorbed by shield
        else if(enemy){
            health.GetDamage(enemy.GetCollisionDamage());
            Destroy(enemy.gameObject);
        }
        //Handle sprite change
        if (health.currentHealth/health.maxHealth <= 0.5)
        {
            spriteRenderer.sprite = damagedSprite;
        }
    }

    void HandleShieldRegen()
    {
        if (Time.time >= lastHitTime + shield.regenDelay)
        {
            shield.RegenShield();
        }
    }
    //Shield effect will pop up if any damage was absorbed
    void hadleShieldEffect(float absorbedDamage, float damage)
    {
        if (absorbedDamage != damage)
        {
            shieldEffect.Play();
        }
    }
}
